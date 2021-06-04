using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPruebaAlpha.Dto
{
    public class BodyUsuario
    {
        public string Nombre { get; set; }
        public string Dni { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Token { get; set; }
        public string Clave { get; set; }
    }
}
