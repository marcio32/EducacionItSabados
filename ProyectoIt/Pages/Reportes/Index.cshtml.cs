using Application.Interfaces;
using Infrastructure.Repositorys;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Reportes
{
    public class IndexModel : PageModel
    {
        public readonly ITurnosRepository _turnosRepository;
        public IndexModel(ITurnosRepository turnosRepository) => _turnosRepository = turnosRepository;
        
        public async Task<IActionResult> OnGetReportesAsync()
        {
            var turnos = await _turnosRepository.GetAllAsync();

            var stats = new
            {
                Total = turnos.Count(),
                Pendientes = turnos.Count(t => t.EstadoId == 1),
                Confirmados = turnos.Count(t => t.EstadoId == 2),
                Cancelados = turnos.Count(t => t.EstadoId == 3),
                Realizados = turnos.Count(t => t.EstadoId == 4),
                Hoy = turnos.Count(t => t.FechaHora.Date == DateTime.Today)
            };

            return new JsonResult(stats);
        }


    }
}
