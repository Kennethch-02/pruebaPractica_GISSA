using System;
using System.Collections.Generic;

namespace appWeb_BD.Models;

public partial class TestTipoIdentificacion
{
    public int TipoIdentificacionId { get; set; }

    public string NombreTipoIdentificacion { get; set; } = null!;

    public virtual ICollection<TestUsuario> TestUsuarios { get; } = new List<TestUsuario>();
}
