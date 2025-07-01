using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddControllers();
builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();


var connectionString = builder.Configuration.GetConnectionString("ConnectionDefault");

builder.Services.AddDbContext<ExoContext>(options =>
    options.UseSqlServer(connectionString)
);

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
