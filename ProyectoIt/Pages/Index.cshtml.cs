using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace ProyectoIt.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {

        private readonly ITurnosRepository _turnosRepository;

        public IndexModel(ITurnosRepository turnosRepository)
        {
            _turnosRepository = turnosRepository;
        }

        public int TurnosPendientes { get; set; }
        public int TurnosConfirmados { get; set; }
        public int TurnosHoy { get; set; }
        public int TurnosReprogramados { get; set; }
        public List<Domain.Entities.Turnos> TurnosDelDia { get; set; } = new();

        public async Task OnGetAsync()
        {
            var turnos = await _turnosRepository.GetAllAsync();
            TurnosPendientes = turnos.Count(t=>t.EstadoId == 1);
            TurnosConfirmados = turnos.Count(t=>t.EstadoId == 2);
            TurnosReprogramados = turnos.Count(t=>t.EstadoId == 6);
            TurnosHoy = turnos.Count(t=>t.FechaHora.Date == DateTime.Today && t.EstadoId != 3);
            TurnosDelDia = turnos.Where(t => t.FechaHora.Date == DateTime.Today && t.EstadoId != 3).ToList();
        }

        public async Task<IActionResult> OnGetStatsAsync()
        {
            var turnos = await _turnosRepository.GetAllAsync();
            return new JsonResult(new
            {
                TurnosPendientes = turnos.Count(t => t.EstadoId == 1),
                TurnosConfirmados = turnos.Count(t => t.EstadoId == 2),
                TurnosReprogramados = turnos.Count(t => t.EstadoId == 6),
                TurnosHoy = turnos.Count(t => t.FechaHora.Date == DateTime.Today && t.EstadoId != 3),
                TurnosDelDia = turnos.Where(t => t.FechaHora.Date == DateTime.Today && t.EstadoId != 3).ToList()
        });
        }

    }
}
