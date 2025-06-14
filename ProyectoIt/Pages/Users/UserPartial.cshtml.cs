using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Repositorys;
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Users.Request;

namespace WebUI.Pages.Users
{
    public class UserPartialModel : PageModel
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UserPartialModel(IUsuariosRepository usuariosRepository) => _usuariosRepository = usuariosRepository;

        public async Task<IActionResult> OnGetAsync (string id)
        {
            return Page();
        }

        public async Task<IActionResult> OnDeleteUserAsync([FromBody] DeleteUserRequest deleteUserRequest)
        {
            var user = await _usuariosRepository.GetByIdAsync(deleteUserRequest.Id);

            if(user == null)
            {
                return NotFound();
            }
            user.Estado = false;
            await _usuariosRepository.DeleteAsync(user);

            return new JsonResult(new { success = true });
        }
    }
}
