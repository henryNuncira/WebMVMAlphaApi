using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPruebaAlpha.Models
{
    public class RadicadoCorrespondecium
    {
        public RadicadoCorrespondecium()
        {
        }

        public RadicadoCorrespondecium(List<RadicadoCorrespondecium> radicados)
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRadicado { get; set; }
        public string Nombre { get; set; }
        public byte[] RespaldoCorrespondencia { get; set; }
        public int? Estado { get; set; }
        public int? IdPersonaContacto { get; set; }
        public int? IdClasificadoR { get; set; }
        public DateTime? Fecha { get; set; }

        public ClasificadoRadicado IdClasificadoRNavigation { get; set; }
        public ContactoPersona IdPersonaContactoNavigation { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
