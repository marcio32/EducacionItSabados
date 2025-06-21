using Microsoft.AspNetCore.Mvc.RazorPages;
using Infrastructure.Repositorys;
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Users.Request;
using Domain.Entities;
using Infrastructure.Application;

namespace WebUI.Pages.Users
{
    public class UserPartialModel : PageModel
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IPasswordService _passwordService;

        public UserPartialModel(IUsuariosRepository usuariosRepository, IRolesRepository rolesRepository, IPasswordService passwordService)
        {
            _usuariosRepository = usuariosRepository;
            _rolesRepository = rolesRepository;
            _passwordService = passwordService;
        }

        public List<Domain.Entities.Roles> Roles { get; set; } = new List<Domain.Entities.Roles>();

        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public int Rol { get; set; }
        [BindProperty]
        public bool Estado { get; set; }


        public async Task<IActionResult> OnGetAsync(string id)
        {
            Roles =  await _rolesRepository.GetRolesActiveAsync();

            if (!string.IsNullOrEmpty(id))
            {
                var user = await _usuariosRepository.GetByIdAsync(int.Parse(id));

                if (user != null)
                {
                    Id = user.Id;
                    Name = user.Nombre;
                    Email = user.Email;
                    Estado = user.Estado;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuario = new Usuarios
            {
                Nombre = Name,
                Email = this.Email,
                Estado = Request.Form["Estado"] == "on",
                FechaCreacion = DateTime.Now,
                HashPassword = _passwordService.HashPassword(Password),
                RolId = this.Rol
            };

            await _usuariosRepository.AddAsync(usuario);
            return new JsonResult(new { success = true });
        }

        public async Task<IActionResult> OnPutAsync()
        {
            var user = await _usuariosRepository.GetByIdAsync(Id);

            if (user == null)
            {
                return NotFound();
            }

            var prueba = _passwordService.VerifyPassword(user.HashPassword, Password);

            user.Nombre = Name;
            user.Email = Email;
            user.HashPassword = _passwordService.HashPassword(Password);
            user.RolId = Rol;
            user.Estado = Request.Form["Estado"] == "on";
            user.FechaModificacion = DateTime.Now;
            await _usuariosRepository.UpdateAsync(user);
            return new JsonResult(new { success = true });

        }

        public async Task<IActionResult> OnDeleteUserAsync([FromBody] DeleteUserRequest deleteUserRequest)
        {
            var user = await _usuariosRepository.GetByIdAsync(deleteUserRequest.Id);

            if (user == null)
            {
                return NotFound();
            }
            user.Estado = false;
            await _usuariosRepository.DeleteAsync(user);

            return new JsonResult(new { success = true });
        }
    }
}
