using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

    [HttpPost]
    public IActionResult Cadastrar(UsuarioModel usuario)
    {
        _usuarioRepository.Cadastrar(usuario);
        return StatusCode(201, "Usuário criado com sucesso!");
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