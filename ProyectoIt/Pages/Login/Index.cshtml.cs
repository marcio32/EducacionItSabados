using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Repositorys;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity.Data;
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
        [BindProperty]
        public string Codigo { get; set; } = string.Empty;
        [BindProperty]
        public string Password { get; set; } = string.Empty;
        [BindProperty]
        public ForgotPasswordRequest ForgotPasswordInput { get; set; }
        [TempData]
        public string Email { get; set; }
        public int Code { get; set; }
      

        public async Task<IActionResult> OnPostAsync()
        {
            TempData["ErrorMessage"] = null;
            TempData["SuccessMessage"] = null;
            var user = await _usuariosRepository.GetByEmailAsync(Login.Email);

            if (user == null)
            {
                TempData["ErrorMessage"] = message;
                return Page();
            }

            var verify = _passwordService.VerifyPassword(user.HashPassword, Login.Password);

            if (!verify)
            {
                TempData["ErrorMessage"] = message;
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
            Email = ForgotPasswordInput.Email;
            var user = await _usuariosRepository.GetByEmailAsync(ForgotPasswordInput.Email);
            if (user == null || !user.Estado)
            {
                Code = 1;
                return null;
            }

            var guid = Guid.NewGuid();
            var numeros = new String(guid.ToString().Where(Char.IsDigit).ToArray());
            var seed = int.Parse(numeros.Substring(0, 6));
            var random = new Random(seed);
            var code = random.Next(000000, 999999);
            user.Codigo = code;
            await _usuariosRepository.UpdateAsync(user);

            await _emailService.SendEmailAsync(user.Email, "Restablecer contraseña", $"Su codigo de restablecimiento es: {code}");
            Code = code;
            return null;
        }


        public async Task<IActionResult> OnPostRecoverAccountAsync()
        {
            TempData["ErrorMessage"] = null;
            TempData["SuccessMessage"] = null;
            var user = await _usuariosRepository.GetByEmailAsync(Email);
            if (user == null || !user.Estado)
            {
                TempData["ErrorMessage"] = "Error al cambiar el codigo";
                return RedirectToPage("/Login/Index");
            }

            if (user.Codigo.ToString() == Codigo)
            {
                user.HashPassword = _passwordService.HashPassword(Password);
                user.Codigo = null;
                await _usuariosRepository.UpdateAsync(user);
                
                TempData["SuccessMessage"] = "Contraseña cambiada con exito";
                return RedirectToPage("/Login/Index");
            }

            TempData["ErrorMessage"] = "El codigo ingresado es incorrecto por favor intente nuevamente";


            return Page();
        }
    }
}