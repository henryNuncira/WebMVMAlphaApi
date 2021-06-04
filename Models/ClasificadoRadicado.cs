using System;
using System.Collections.Generic;

#nullable disable

namespace WebApiPruebaAlpha.Models
{
    public class ClasificadoRadicado
    {
        public ClasificadoRadicado()
        {
            RadicadoCorrespondecia = new HashSet<RadicadoCorrespondecium>();
        }

        public int IdClasificadoR { get; set; }
        public int? TipoRadicado { get; set; }
        public string SerialRadicado { get; set; }

        public ICollection<RadicadoCorrespondecium> RadicadoCorrespondecia { get; set; }
    }
}
