using Alura.Usuarios.API.Data.Dtos;
using Alura.Usuarios.API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Alura.Usuarios.API.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Cadastrar(CreateUsuarioDto usuarioDto)
        {
            Usuario usuario = _mapper.Map<Usuario>(usuarioDto);
            IdentityResult result = await _userManager.CreateAsync(usuario, usuarioDto.Password);

            if (!result.Succeeded)
                throw new ApplicationException("Falha ao cadastrar usuário.");
        }

        public async Task<string> Login(LoginUsuarioDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, false);

            if (!result.Succeeded)
                throw new ApplicationException("Usuário não autenticado.");

            var usuario = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == loginDto.UserName.ToUpper());
            var token = _tokenService.GenerateToken(usuario);
            return token;
        }
    }
}
