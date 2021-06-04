using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiPruebaAlpha.Dto
{
    public class ResponseUsuario
    {
        public int state { get; set; }

        public string message { get; set; }

        public string rol { get; set; }
        public string token { get; set; }
    }
}
