using Alura.Usuarios.API.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Alura.Usuarios.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        public IActionResult CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {

            throw new NotImplementedException();
        }
    }
}
