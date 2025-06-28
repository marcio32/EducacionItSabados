using Microsoft.AspNetCore.Mvc;

namespace WebUI.Pages.Users.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Rol { get; set; }
        public bool Estado { get; set; }
    }
}
