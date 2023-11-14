using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services.Interface;

namespace UsuariosApi.Services;

public class CadastroService: ICadastroService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;

    public CadastroService(IMapper mapper, UserManager<Usuario> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Cadastra(CreateUsuarioDto createUsuarioDto)
    {
        var usuario = _mapper.Map<CreateUsuarioDto, Usuario>(createUsuarioDto);

        var resultado = await _userManager.CreateAsync(usuario);

        if (!resultado.Succeeded) 
            throw new Exception("Falha ao cadastrar usuário!", new Exception(resultado.Errors.First().Description));
    }
}