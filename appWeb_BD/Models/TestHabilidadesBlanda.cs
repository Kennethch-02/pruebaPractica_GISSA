using System;
using System.Collections.Generic;

namespace appWeb_BD.Models;

public partial class TestHabilidadesBlanda
{
    public int HabilidadBlandaId { get; set; }

    public string NombreHabilidadBlanda { get; set; } = null!;

    public virtual ICollection<TestUsuario> TestUsuarios { get; } = new List<TestUsuario>();
}
