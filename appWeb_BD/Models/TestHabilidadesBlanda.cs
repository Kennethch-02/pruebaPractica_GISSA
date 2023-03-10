using System;
using System.Collections.Generic;

namespace appWeb_BD.Models;

public partial class TestHabilidadesBlanda
{
    public int Id { get; set; }

    public string Habilidad { get; set; } = null!;

    public int UsuarioId { get; set; }

    public virtual TestUsuario Usuario { get; set; } = null!;
}
