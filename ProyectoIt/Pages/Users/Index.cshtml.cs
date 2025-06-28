using Infrastructure.Repositorys;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

namespace WebUI.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public readonly IUsuariosRepository _usuariosRepository;
        public IEnumerable<Usuarios> Users { get; set; }

        public IndexModel(IUsuariosRepository usuariosRepository) => _usuariosRepository = usuariosRepository;

        public async Task OnGetAsync() => Users = await _usuariosRepository.GetAllAsync();
    }
}
