using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPruebaAlpha.Dto
{
    public class BodyPersona
    {
        public int? TipoContacto { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
