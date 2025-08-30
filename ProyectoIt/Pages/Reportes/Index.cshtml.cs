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

        public async Task<IActionResult> OnGetReportesMedicosAsync()
        {
            var turnos = await _turnosRepository.GetAllAsync();

            var statsPorMedico = turnos.GroupBy(t => new { t.MedicoId, t.Medico?.Nombre })
                .Select(g => new
                {
                    MedicoId = g.Key.MedicoId,
                    MedicoNombre = g.Key.Nombre,
                    Total = g.Count(),
                }).OrderByDescending(x => x.Total).ToList();
            return new JsonResult(statsPorMedico);
        }

        public async Task<IActionResult> OnGetReportesMensualAsync()
        {
            var turnos = await _turnosRepository.GetAllAsync();

            var statsMensual = turnos.Where(t => t.FechaHora.Year == DateTime.Today.Year).GroupBy(t => t.FechaHora.Month)
                .Select(g => new
                {
                    Mes = g.Key,
                    Total = g.Count()
                }).ToList();

            var result = Enumerable.Range(1, 12).Select(mes => new
            {
                Mes = mes,
                Total = statsMensual.FirstOrDefault(x=> x.Mes == mes)?.Total ?? 0
            });

            return new JsonResult(result);
        }


    }
}
