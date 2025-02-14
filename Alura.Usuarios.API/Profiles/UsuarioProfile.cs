using Alura.Usuarios.API.Data.Dtos;
using Alura.Usuarios.API.Models;
using AutoMapper;

namespace Alura.Usuarios.API.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<CreateUsuarioDto, Usuario>();
        }
    }
}
