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

        var result = await _userManager.CreateAsync(usuario, createUsuarioDto.Password);

        if (!result.Succeeded)
            throw new Exception("Falha ao cadastrar usuário!", new Exception(result.Errors.First().Description));
    }

    public async Task<string> Login(LoginUsuarioDto dto)
    {
        try
        {
            var result = await _signInManager.PasswordSignInAsync(
                dto.Username,
                dto.Password,
                false,
                false
            );
            if (!result.Succeeded) throw new ApplicationException("Usuario não autenticado");

            var user = _signInManager
                .UserManager
                .Users
                .First(x => x.NormalizedUserName == dto.Username);

            var token = _tokenService.GenerateToken(user);

            return token;
        } catch (Exception e)
        {
            throw new Exception("Falha de autenticação", e.InnerException);
        }
    }
}