using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;
using Infrastructure.Interfaces;
using Domain.Entities;
using WebUI.Pages.Turnos.DTOs;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;

namespace WebUI.Pages.Turnos
{
    public class TurnosPartialModel : PageModel
    {
        private readonly ITurnosRepository _turnosRepository;
        private readonly IMedicosRepository _medicosRepository;
        private readonly IPacientesRepository _pacientesRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IDocumentosRepository _documentosRepository;

        public TurnosPartialModel(ITurnosRepository turnosRepository, IMedicosRepository medicosRepository, IPacientesRepository pacientesRepository, IUsuariosRepository usuariosRepository, IDocumentosRepository documentosRepository)
        {
            _turnosRepository = turnosRepository;
            _medicosRepository = medicosRepository;
            _pacientesRepository = pacientesRepository;
            _usuariosRepository = usuariosRepository;
            _documentosRepository = documentosRepository;
        }

        public List<Medicos> Medicos { get; set; } = new List<Medicos>();
        public List<Pacientes> Pacientes { get; set; } = new List<Pacientes>();
        public List<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
        public List<Documentos> Documentos { get; set; } = new List<Documentos>();

        [BindProperty]
        public TurnoDto TurnoDto { get; set; } = new TurnoDto();


        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int turnoId))
            {
                var turno = await _turnosRepository.GetByIdAsync(turnoId);
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
    }
}
