using System;
using System.Collections.Generic;

namespace appWeb_BD.Models;

public partial class TestUsuario
{
    public int Id { get; set; }

    public string TipoUsuario { get; set; } = null!;

    public string TipoIdentificacion { get; set; } = null!;

    public string NumIdentificacion { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;  
    public string Telefono { get; set; } = null!;
    public string HabilidadBlanda { get; set; } = null!;

    public virtual ICollection<TestHabilidadesBlanda> TestHabilidadesBlanda { get; } = new List<TestHabilidadesBlanda>();

    public virtual ICollection<TestTelefono> TestTelefonos { get; } = new List<TestTelefono>();
}
