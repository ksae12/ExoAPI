using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exo.WebApi.Contexts;
using Exo.WebApi.Models;

namespace Exo.WebApi.Repositories;

public class UsuarioRepository
{
    private readonly ExoContext _context;

    public UsuarioRepository(ExoContext context)
    {
        _context = context;
    }

    public UsuarioModel Login(string email, string senha)
    {
        return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
    }

    public List<UsuarioModel> Listar()
    {
        return _context.Usuarios.ToList();
    }

    public void Cadastrar(UsuarioModel usuario)
    {
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public UsuarioModel BuscarPorId(int id)
    {
        return _context.Usuarios.Find(id);
    }

    public void Atualizar(int id, UsuarioModel usuario)
    {
        UsuarioModel usuarioBuscado = _context.Usuarios.Find(id);

        if (usuarioBuscado != null)
        {
            usuarioBuscado.Email = usuario.Email;
            usuarioBuscado.Senha = usuario.Senha;
        }
        _context.Usuarios.Update(usuarioBuscado);
        _context.SaveChanges();
    }

    public void Deletar(int id)
    {
        UsuarioModel usuarioBuscado = _context.Usuarios.Find(id);
        _context.Usuarios.Remove(usuarioBuscado);
        _context.SaveChanges();
    }
}
