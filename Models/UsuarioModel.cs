using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exo.WebApi.Models;

public class UsuarioModel
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
}
