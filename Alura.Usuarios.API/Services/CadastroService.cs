using Alura.Usuarios.API.Data.Dtos;
using Alura.Usuarios.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Alura.Usuarios.API.Services
{
    public class CadastroService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;

        public CadastroService(IMapper mapper, UserManager<Usuario> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task Cadastrar(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityResult result = await _userManager.CreateAsync(usuario, usuarioDto.Password);

            if (!result.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuário.");
        }
    }
}
