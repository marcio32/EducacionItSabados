using Infrastructure.Repositorys;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using WebUI.Pages.Login.DTOs;

namespace WebUI.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPasswordService _passwordService;
        public IndexModel(IUsuariosRepository usuariosRepository, IPasswordService passwordService)
        {
            _usuariosRepository = usuariosRepository;
            _passwordService = passwordService;
        }

        [BindProperty]
        public LoginDto Login { get; set; } = new LoginDto();

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _usuariosRepository.GetByEmailAsync(Login.Email);

            if (user == null)
            {
                return Page();
            }

            var verify = _passwordService.VerifyPassword(user.HashPassword, Login.Password);

            if (!verify)
            {
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol.Nombre)
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");

            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync("CookieAuthentication", principal);

            return RedirectToPage("/Index");

        }
    }
}
