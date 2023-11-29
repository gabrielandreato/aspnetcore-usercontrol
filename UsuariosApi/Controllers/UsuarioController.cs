using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Services.Interface;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;

    }

    [HttpPost("Cadastrar")]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createUsuarioDto)
    {
        await _usuarioService.Cadastra(createUsuarioDto);
        return Ok("Usuário cadastrado!");
    }
    
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUsuarioDto loginUsuarioDto)
    {
        try
        {
            var token = await _usuarioService.Login(loginUsuarioDto);
            return Ok(token);

        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
