using Microsoft.AspNetCore.Mvc.RazorPages;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebUI.Pages.Users.Request;
using Domain.Entities;
using WebUI.Pages.Users.DTOs;

namespace WebUI.Pages.Users
{
    public class UsersPartialModel : PageModel
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IPasswordService _passwordService;

        public UsersPartialModel(IUsuariosRepository usuariosRepository, IRolesRepository rolesRepository, IPasswordService passwordService)
        {
            _usuariosRepository = usuariosRepository;
            _rolesRepository = rolesRepository;
            _passwordService = passwordService;
        }

        public IEnumerable<Domain.Entities.Roles> Roles { get; set; } = new List<Domain.Entities.Roles>();

        [BindProperty]
        public UserDto UserDto { get; set; } = new UserDto();


        public async Task<IActionResult> OnGetAsync(string id)
        {
            Roles =  await _rolesRepository.GetRolesActiveAsync();

            if (!string.IsNullOrEmpty(id))
            {
                var user = await _usuariosRepository.GetByIdAsync(int.Parse(id));

                if (user != null)
                {
                    UserDto.Id = user.Id;
                    UserDto.Name = user.Nombre;
                    UserDto.Email = user.Email;
                    UserDto.Estado = user.Estado;
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var usuario = new Usuarios
            {
                Nombre = UserDto.Name,
                Email = this.UserDto.Email,
                Estado = Request.Form["Estado"] == "on",
                FechaCreacion = DateTime.Now,
                HashPassword = _passwordService.HashPassword(UserDto.Password),
                RolId = this.UserDto.Rol
            };

            var result = await _usuariosRepository.AddAsync(usuario);

            if (result)
            {
                return new JsonResult(new { success = true, message = "Usuario creado correctamente" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al crear el usuario" });
            }
        }

        public async Task<IActionResult> OnPutAsync()
        {
            var user = await _usuariosRepository.GetByIdAsync(UserDto.Id);

            if (user == null)
            {
                return NotFound();
            }


            user.Nombre = UserDto.Name;
            user.Email = UserDto.Email;
            user.HashPassword = _passwordService.HashPassword(UserDto.Password);
            user.RolId = UserDto.Rol;
            user.Estado = Request.Form["Estado"] == "on";
            user.FechaModificacion = DateTime.Now;
            var result =await _usuariosRepository.UpdateAsync(user);

            if (result)
            {
                return new JsonResult(new { success = true, message = "Usuario modificado correctamente" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al modificar el usuario" });
            }

        }

        public async Task<IActionResult> OnDeleteUserAsync([FromBody] DeleteUserRequest deleteUserRequest)
        {
            var user = await _usuariosRepository.GetByIdAsync(deleteUserRequest.Id);

            if (user == null)
            {
                return NotFound();
            }
            user.Estado = false;
            var result = await _usuariosRepository.DeleteAsync(user);

            if (result)
            {
                return new JsonResult(new { success = true, message = "Usuario eliminado correctamente" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al eliminar al usuario" });
            }
        }
    }
}
