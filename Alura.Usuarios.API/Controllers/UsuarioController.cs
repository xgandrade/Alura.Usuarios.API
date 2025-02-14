using Alura.Usuarios.API.Data.Dtos;
using Alura.Usuarios.API.Models;
using Alura.Usuarios.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Alura.Usuarios.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private CadastroService _cadastroService;

        public UsuarioController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            await _cadastroService.Cadastrar(usuarioDto);
            return Ok(usuarioDto);
        }
    }
}
