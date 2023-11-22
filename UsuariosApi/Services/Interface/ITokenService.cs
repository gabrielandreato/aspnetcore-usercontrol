using UsuariosApi.Models;

namespace UsuariosApi.Services;

public interface ITokenService
{
    string GenerateToken(Usuario usuario);
}