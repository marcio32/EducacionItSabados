using Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Turnos
{
    public class IndexModel : PageModel
    {
        private readonly ITurnosRepository _turnosRepository;

        public IEnumerable<Domain.Entities.Turnos> Turnos { get; set; } = new List<Domain.Entities.Turnos>();

        public IndexModel(ITurnosRepository turnosRepository) => _turnosRepository = turnosRepository;

        public async Task OnGetAsync() => Turnos = await _turnosRepository.GetAllAsync();
    }
}
