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
        private UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto usuarioDto)
        {
            await _usuarioService.Cadastrar(usuarioDto);
            return Ok(usuarioDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDto loginDto)
        {
            var token = await _usuarioService.Login(loginDto);
            return Ok(token);
        }
    }
}
