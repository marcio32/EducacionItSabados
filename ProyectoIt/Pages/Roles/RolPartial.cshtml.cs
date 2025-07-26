using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.InteropServices;
using WebUI.Pages.Roles.DTOs;
using WebUI.Pages.Roles.Request;

namespace WebUI.Pages.Roles
{
    public class RolPartialModel : PageModel
    {
        private readonly IRolesRepository _rolesRepository;
        public RolPartialModel(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [BindProperty]
        public RolDto RolDto { get; set; } = new RolDto();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var rol = await _rolesRepository.GetByIdAsync(int.Parse(id));

            if(rol == null)
            {
                return NotFound();
            }

            RolDto.Id = rol.Id;
            RolDto.Name = rol.Nombre;
            RolDto.Estado = rol.Estado;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var rol = new Domain.Entities.Roles
            {
                Nombre = RolDto.Name,
                Estado = RolDto.Estado
            };

            var result = await _rolesRepository.AddAsync(rol);

            if(result)
            {
                return new JsonResult(new { success = true, message = "Rol creado correctamente" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al crear el rol" });
            }
        }
        public async Task<IActionResult> OnPutAsync()
        {
            var rol = await _rolesRepository.GetByIdAsync(RolDto.Id);

            if (rol == null)
            {
                return NotFound();
            }
            rol.Nombre = RolDto.Name;
            rol.Estado = RolDto.Estado;

            var result = await _rolesRepository.UpdateAsync(rol);

            if (result)
            {
                return new JsonResult(new { success = true, message = "Rol modificado correctamente" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al modificar el rol" });
            }
        }

        public async Task<IActionResult> OnDeleteAsync([FromBody] DeleteRoleRequest deleteRoleRequest)
        {
            var rol = await _rolesRepository.GetByIdAsync(deleteRoleRequest.Id);

            if (rol == null)
            {
                return NotFound();
            }

            var result = await _rolesRepository.DeleteAsync(rol);

            if (result)
            {
                return new JsonResult(new { success = true, message = "Rol eliminado correctamente" });
            }
            else
            {
                return new JsonResult(new { success = false, message = "Error al eliminar el rol" });
            }
        }

    }
}
