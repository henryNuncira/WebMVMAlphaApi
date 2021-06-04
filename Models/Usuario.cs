using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPruebaAlpha.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Token { get; set; }
        public string Clave { get; set; }
        public int? IdRadicado { get; set; }

        public RadicadoCorrespondecium IdRadicadoNavigation { get; set; }
        public Rol IdRolNavigation { get; set; }
    }
}
