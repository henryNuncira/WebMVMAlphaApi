using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPruebaAlpha.Models
{
    public class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
