using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Domain.Entities;
using WebUI.Pages.Turnos.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using Application.Repositorys;
using WebUI.Pages.Turnos.Request;

namespace WebUI.Pages.Turnos
{
    public class TurnosPartialModel(ITurnosRepository turnosRepository, IMedicosRepository medicosRepository, IPacientesRepository pacientesRepository, IUsuariosRepository usuariosRepository, IDocumentosRepository documentosRepository, IEstudiosRepository estudiosRepository, INotificationService notificationService) : PageModel
    {

        private readonly ITurnosRepository _turnosRepository = turnosRepository;
        private readonly IMedicosRepository _medicosRepository = medicosRepository;
        private readonly IPacientesRepository _pacientesRepository = pacientesRepository;
        private readonly IUsuariosRepository _usuariosRepository = usuariosRepository;
        private readonly IDocumentosRepository _documentosRepository = documentosRepository;
        private readonly IEstudiosRepository _estudiosRepository = estudiosRepository;
        private readonly INotificationService _notificationService = notificationService;

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
                        Dni = turno.Paciente.Dni,
                        Nombre = turno.Paciente.Nombre,
                        Apellido = turno.Paciente.Apellido,
                        FechaNacimiento = turno.Paciente.FechaNacimiento,
                        Email = turno.Paciente.Email,
                        Telefono = turno.Paciente.Telefono,
                        FechaHora = turno.FechaHora,
                        EstadoId = turno.EstadoId,
                        EstudioId = turno.EstudioId,
                        MedicoId = turno.MedicoId,
                        PacienteId = turno.PacienteId,
                        UsuarioId = turno.UsuarioId,
                        Observaciones = turno.Observaciones
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
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "adjuntos");
                    Directory.CreateDirectory(uploadPath);

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
                            Ruta = $"adjuntos/{fileName}",
                            TurnosId = turno.Id,
                            FechaSubida = DateTime.Now
                        };
                        await _documentosRepository.AddAsync(documento);
                    }
                }
                var medico = await _medicosRepository.GetByIdAsync(turno.MedicoId);
                await _notificationService.NotifyTurnoCreated(turno.Id, turno.Paciente?.Nombre + " " + turno.Paciente?.Apellido, medico?.Nombre + " " + medico?.Apellido); 
                return new JsonResult(new { success = true, message = "Turno Creado" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        public async Task<IActionResult> OnPutAsync()
        {
            try
            {
                var turno = await _turnosRepository.GetByIdAsync(TurnoDto.Id);
                if (turno == null) return NotFound();

                var paciente = await _pacientesRepository.GetByDniAsync(TurnoDto.Dni);

                paciente = new Pacientes
                {
                    Nombre = TurnoDto.Nombre,
                    Apellido = TurnoDto.Apellido,
                    Dni = TurnoDto.Dni,
                    FechaNacimiento = TurnoDto.FechaNacimiento,
                    Telefono = TurnoDto.Telefono,
                    Email = TurnoDto.Email
                };

                var result = await _pacientesRepository.UpdateAsync(paciente);

                if (!result)
                    return new JsonResult(new { sucess = true, message = "Error al actualizar el turno" });

                turno.FechaModificacion = DateTime.Now;
                turno.FechaHora = TurnoDto.FechaHora;
                turno.EstadoId = TurnoDto.EstadoId;
                turno.MedicoId = TurnoDto.MedicoId;
                turno.EstudioId = TurnoDto.EstudioId;
                turno.FechaHora = TurnoDto.FechaHora;
                turno.Observaciones = TurnoDto.Observaciones;

                result = await _turnosRepository.UpdateAsync(turno);

                if (TurnoDto.Documentos != null && TurnoDto.Documentos.Any())
                {
                    var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "adjuntos");
                    Directory.CreateDirectory(uploadPath);

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
                            Ruta = $"adjuntos/{fileName}",
                            TurnosId = turno.Id,
                            FechaSubida = DateTime.Now
                        };
                        await _documentosRepository.AddAsync(documento);
                    }
                }
                var estado = (EstadoTurno)TurnoDto.EstadoId;
                await _notificationService.NotifyTurnoUpdated(turno.Id, estado.ToString());
                if (result)
                    return new JsonResult(new { success = true, message = "Turno actualizado correctamente" });
                else
                    return new JsonResult(new { success = false, message = "Error al actualizar el turno" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<IActionResult> OnDeleteAsync([FromBody] CancelTurnoRequest cancelTurnoRequest)
        {
            var turno = await _turnosRepository.GetByIdAsync(cancelTurnoRequest.Id);

            var result = await _turnosRepository.DeleteAsync(turno);
            await _notificationService.NotifyTurnoCanceled(turno.Id, turno.Observaciones);

            if (result) 
                return new JsonResult(new { success = true, message = "Turno Cancelado correctamente" });
            else
                return new JsonResult(new { success = false, message = "Error al cancelar el turno" });
        }

        private async Task LoadDataASync()
        {
            Medicos = [.. (await _medicosRepository.GetAllAsync())];
            Pacientes = [.. (await _pacientesRepository.GetAllAsync())];
            Estudios = [.. (await _estudiosRepository.GetAllAsync())];
        }
    }
}
