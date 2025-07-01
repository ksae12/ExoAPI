using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Exo.WebApi.Controllers;


[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuariosController(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_usuarioRepository.Listar());
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        UsuarioModel usuario = _usuarioRepository.BuscarPorId(id);

        if (usuario == null)
        {
            return NotFound("Usuário não encontrado!");
        }
        return Ok(usuario);
    }

    public IActionResult Post(UsuarioModel usuario)
    {
        UsuarioModel usuarioBuscado = _usuarioRepository.Login(usuario.Email, usuario.Senha);

        if (usuarioBuscado == null)
        {
            return NotFound("E-mail ou senha inválidos!");
        }

        var claims = new[]
        {

            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, usuarioBuscado.Email),

            new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
        };

        var key = new
        SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("exoapi-chave-autenticacao"));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "exoapi.webapi",
            audience: "exoapi.webapi",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Ok(
            new { token = new JwtSecurityTokenHandler().WriteToken(token) }
        );
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, UsuarioModel usuario)
    {
        _usuarioRepository.Atualizar(id, usuario);
        return StatusCode(201, "Usuário atualizado");
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        try
        {
            _usuarioRepository.Deletar(id);
            return StatusCode(204, "Usuário deletado!");
        }
        catch (Exception)
        {

            return BadRequest("Não foi possível deletar o usuário");
        }
    }

}