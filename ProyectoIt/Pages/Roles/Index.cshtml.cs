using Infrastructure.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebUI.Pages.Roles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public readonly IRolesRepository _rolesRepository;
        public IEnumerable<Domain.Entities.Roles> Roles { get; set; }

        public IndexModel(IRolesRepository rolesRepository) => _rolesRepository  = rolesRepository;

        public async Task OnGetAsync() => Roles = await _rolesRepository.GetAllAsync();
    }
}
