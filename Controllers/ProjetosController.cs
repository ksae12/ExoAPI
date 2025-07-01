using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Exo.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjetosController : ControllerBase
{
    private readonly ProjetoRepository _projetoRepository;
    public ProjetosController(ProjetoRepository projetoRepository)
    {
        _projetoRepository = projetoRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_projetoRepository.Listar());
    }

    [HttpPost]
    public IActionResult Cadastrar(Projeto projeto)
    {
        _projetoRepository.Cadastrar(projeto);
        return StatusCode(201, "Cadastro feito com sucesso!");
    }

    [HttpGet("{id}")]
    public IActionResult BucarPorId(int id)
    {
        Projeto projeto = _projetoRepository.BuscarPorId(id);

        if (projeto == null)
        {
            return NotFound("Projeto não encontrado!");
        }
        return Ok(projeto);
    }
}
