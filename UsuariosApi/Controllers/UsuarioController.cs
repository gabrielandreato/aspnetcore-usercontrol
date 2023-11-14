using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services.Interface;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController : ControllerBase
{
    private ICadastroService _cadastroService;

    public UsuarioController(ICadastroService cadastroService)
    {
        _cadastroService = cadastroService;

    }

    [HttpPost]
    public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createUsuarioDto)
    {
        await _cadastroService.Cadastra(createUsuarioDto);
        return Ok("Usuário cadastrado!");
    }
}