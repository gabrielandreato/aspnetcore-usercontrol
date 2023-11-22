using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dtos;
using UsuariosApi.Models;
using UsuariosApi.Services.Interface;

namespace UsuariosApi.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IMapper _mapper;
    private readonly UserManager<Usuario> _userManager;
    private readonly SignInManager<Usuario> _signInManager;
    private readonly ITokenService _tokenService;

    public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> signInManager,
        ITokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    public async Task Cadastra(CreateUsuarioDto createUsuarioDto)
    {
        var usuario = _mapper.Map<CreateUsuarioDto, Usuario>(createUsuarioDto);

        var resultado = await _userManager.CreateAsync(usuario);

        if (!resultado.Succeeded)
            throw new Exception("Falha ao cadastrar usuário!", new Exception(resultado.Errors.First().Description));
    }

    public async Task<string> Login(LoginUsuarioDto dto)
    {
        //TODO: corrigir falha no retorno do usuario.
        var resultado = await _signInManager.PasswordSignInAsync(
            dto.Username,
            dto.Password,
            false,
            false
        );

        if (!resultado.Succeeded)
        {
            throw new ApplicationException("Usuario não autenticado");
        }

        var usuario = _signInManager
            .UserManager
            .Users
            .FirstOrDefault(x => x.NormalizedUserName == dto.Username);

        string token =  _tokenService.GenerateToken(usuario);
        
        return token;
    }
}