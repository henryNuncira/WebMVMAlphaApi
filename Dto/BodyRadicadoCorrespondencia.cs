using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPruebaAlpha.Dto
{
    public class BodyRadicadoCorrespondencia
    {
        public string Nombre { get; set; }
        public byte[] RespaldoCorrespondencia { get; set; }
        public int? Estado { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
