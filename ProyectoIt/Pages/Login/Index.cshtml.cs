using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositorys;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Formats.Asn1;
using System.Security.Claims;
using WebUI.Pages.Login.DTOs;

namespace WebUI.Pages.Login
{
    public class IndexModel : PageModel
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPasswordService _passwordService;
        private readonly IEmailService _emailService;
        private const string message = "Usuario o contraseña incorrectos";
        public IndexModel(IUsuariosRepository usuariosRepository, IPasswordService passwordService, IEmailService emailService)
        {
            _usuariosRepository = usuariosRepository;
            _passwordService = passwordService;
            _emailService = emailService;
        }

        [BindProperty]
        public LoginDto Login { get; set; } = new LoginDto();
        public string ErrorMessage { get; set; } = string.Empty;
        [BindProperty]
        public ForgotPasswordRequest ForgotPasswordInput { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _usuariosRepository.GetByEmailAsync(Login.Email);

            if (user == null)
            {
                ErrorMessage = message;
                return Page();
            }

            var verify = _passwordService.VerifyPassword(user.HashPassword, Login.Password);

            if (!verify)
            {
                ErrorMessage = message;
                return Page();
            }

            return await SignIn(user); ;
        }

        public async Task<IActionResult> OnGetGoogleResponseAsync()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync();
            if (!authenticateResult.Succeeded)
            {
                return RedirectToPage("/Login/Index");
            }

            var claimsGoogle = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;

            var user = await _usuariosRepository.GetByEmailAsync(claimsGoogle.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value);
            return await SignIn(user);
        }

        public async Task<IActionResult> OnPostGoogleLoginAsync()
        {
            var redirectURL = Url.Page("/Login/Index", pageHandler: "GoogleResponse");
            var properties = new AuthenticationProperties { RedirectUri = redirectURL };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        public async Task<IActionResult> SignIn(Usuarios user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Rol.Nombre)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostForgotPasswordAsync()
        {
            var user = await _usuariosRepository.GetByEmailAsync(ForgotPasswordInput.Email);
            if (user == null || !user.Estado)
            {
                return Page();
            }

            await _emailService.SendEmailAsync(user.Email, "Restablecer contraseña", "Su codigo es 1234");

            ErrorMessage = "Se ha enviado un enlace a su mail para restablecer su contraseña.";
            return Page();
        }
    }
}