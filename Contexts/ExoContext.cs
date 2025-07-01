using Exo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace Exo.WebApi.Contexts;

public class ExoContext : DbContext
{
    public ExoContext()
    {
    }

    public ExoContext(DbContextOptions<ExoContext> options) : base(options)
    {
    }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<UsuarioModel> Usuarios { get; set; }


}
