using UsuariosApi.Data.Dtos;

namespace UsuariosApi.Services.Interface;

public interface ICadastroService
{
    Task Cadastra(CreateUsuarioDto createUsuarioDto);
}