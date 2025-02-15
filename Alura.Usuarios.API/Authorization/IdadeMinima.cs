using Microsoft.AspNetCore.Authorization;

namespace Alura.Usuarios.API.Authorization
{
    public class IdadeMinima : IAuthorizationRequirement
    {
        public IdadeMinima(int idade)
        {
            Idade = idade;
        }

        public int Idade { get; set; }
    }
}
