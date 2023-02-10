using System;
using System.Collections.Generic;

namespace appWeb_BD.Models;

public partial class TestUsuario
{
    public int UsuarioId { get; set; }

    public int TipoUsuarioId { get; set; }

    public int TipoIdentificacionId { get; set; }

    public string NumeroIdentificacion { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public int HabilidadBlandaId { get; set; }

    public virtual TestHabilidadesBlanda HabilidadBlanda { get; set; } = null!;

    public virtual TestTipoIdentificacion TipoIdentificacion { get; set; } = null!;

    public virtual TestTipoUsuario TipoUsuario { get; set; } = null!;
}
