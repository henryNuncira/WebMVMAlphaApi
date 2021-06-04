using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPruebaAlpha.Models
{
    public class ContactoPersona
    {
        public ContactoPersona()
        {
            RadicadoCorrespondecia = new HashSet<RadicadoCorrespondecium>();
        }

        public int IdPersona { get; set; }
        public int? TipoContacto { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public ICollection<RadicadoCorrespondecium> RadicadoCorrespondecia { get; set; }
    }
}
