using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;

    public UsuarioController(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createUsuarioDto)
    {
        var usuario = _mapper.Map<CreateUsuarioDto, Usuario>(createUsuarioDto);

        var resultado = await _userManager.CreateAsync(usuario);

        if (resultado.Succeeded) return Ok("Usuario cadastrado.");
        throw new ApplicationException("Falha ao cadastrar usuário!");
    }
}