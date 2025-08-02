using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Domain.Entities;
using WebUI.Pages.Turnos.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using Application.Repositorys;

namespace WebUI.Pages.Turnos
{
    public class TurnosPartialModel(ITurnosRepository turnosRepository, IMedicosRepository medicosRepository, IPacientesRepository pacientesRepository, IUsuariosRepository usuariosRepository, IDocumentosRepository documentosRepository, IEstudiosRepository estudiosRepository) : PageModel
    {

        private readonly ITurnosRepository _turnosRepository = turnosRepository;
        private readonly IMedicosRepository _medicosRepository = medicosRepository;
        private readonly IPacientesRepository _pacientesRepository = pacientesRepository;
        private readonly IUsuariosRepository _usuariosRepository = usuariosRepository;
        private readonly IDocumentosRepository _documentosRepository = documentosRepository;
        private readonly IEstudiosRepository _estudiosRepository = estudiosRepository;

        public List<Medicos> Medicos { get; set; } = new List<Medicos>();
        public List<Pacientes> Pacientes { get; set; } = new List<Pacientes>();
        public List<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
        public List<Documentos> Documentos { get; set; } = new List<Documentos>();
        public List<Estudios> Estudios { get; set; } = new List<Estudios>();

        [BindProperty]
        public TurnoDto TurnoDto { get; set; } = new TurnoDto();


        public async Task<IActionResult> OnGetAsync(string id)
        {
            await LoadDataASync();
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int turnoId))
            {
                var turno = await turnosRepository.GetByIdAsync(turnoId);
                if (turno != null)
                {
                    TurnoDto = new TurnoDto
                    {
                        Id = turno.Id,
                        FechaHora = turno.FechaHora,
                        Estado = Enum.Parse<EstadoTurno>(turno.Estado.Nombre),
                        MedicoId = turno.MedicoId,
                        PacienteId = turno.PacienteId,
                        UsuarioId = turno.UsuarioId
                    };
                    Documentos = turno.Documentos.ToList();
                }
            }


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                var paciente = await _pacientesRepository.GetByDniAsync(TurnoDto.Dni);
                if (paciente == null)
                {
                    paciente = new Pacientes
                    {
                        Nombre = TurnoDto.Nombre,
                        Apellido = TurnoDto.Apellido,
                        Dni = TurnoDto.Dni,
                        FechaNacimiento = TurnoDto.FechaNacimiento,
                        Telefono = TurnoDto.Telefono,
                        Email = TurnoDto.Email
                    };

                    paciente = await _pacientesRepository.AddAsync(paciente);
                }

                var turno = new Domain.Entities.Turnos
                {
                    UsuarioId = 3,
                    FechaHora = DateTime.Now,
                    EstadoId = TurnoDto.EstadoId,
                    MedicoId = TurnoDto.MedicoId,
                    PacienteId = paciente.Id,
                    EstudioId = TurnoDto.EstudioId,
                    DocumentosId = 0
                };

                turno = await _turnosRepository.AddAsync(turno);

                if (TurnoDto.Documentos != null && TurnoDto.Documentos.Any())
                {
                    var uploadPath = @"c:\temp\";

                    foreach (var archivo in TurnoDto.Documentos)
                    {
                        var fileName = $"{turno.Id}_{archivo.FileName}";
                        var filePath = Path.Combine(uploadPath, fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await archivo.CopyToAsync(stream);
                        }

                        var documento = new Documentos
                        {
                            Nombre = fileName,
                            Ruta = @"c:\temp\" + fileName,
                            TurnosId = turno.Id,
                            FechaSubida = DateTime.Now
                        };
                        await _documentosRepository.CreateAsync(documento);
                    }
                }
                return new JsonResult(new { success = true, message = "Turno Creado" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private async Task LoadDataASync()
        {
            Medicos = [.. (await _medicosRepository.GetAllAsync())];
            Pacientes = [.. (await _pacientesRepository.GetAllAsync())];
            Estudios = [.. (await _estudiosRepository.GetAllAsync())];
        }
    }
}
