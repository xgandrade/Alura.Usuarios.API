using System.ComponentModel.DataAnnotations;

namespace Alura.Usuarios.API.Data.Dtos
{
    public class LoginUsuarioDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
