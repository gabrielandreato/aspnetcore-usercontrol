using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Services.Interface;

public interface IUsuarioService
{
    Task Cadastra(CreateUsuarioDto createUsuarioDto);
    Task<string> Login(LoginUsuarioDto dto);
}