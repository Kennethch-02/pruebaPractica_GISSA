using System;
using System.Collections.Generic;

namespace appWeb_BD.Models;

public partial class TestTipoUsuario
{
    public int TipoUsuarioId { get; set; }

    public string NombreTipoUsuario { get; set; } = null!;

    public virtual ICollection<TestUsuario> TestUsuarios { get; } = new List<TestUsuario>();
}
