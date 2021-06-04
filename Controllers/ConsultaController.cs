using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPruebaAlpha.Models;

namespace WebApiPruebaAlpha.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsultaController : Controller
    {
        private readonly ILogger _logger;
        public AlphaMVMContext context = new AlphaMVMContext();

        public ConsultaController(ILogger<ConsultaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{nit}")]
        public RadicadoCorrespondecium GetNitRadicado(string nit)
        {
            var Persona = context.ContactoPersonas.Where(x => x.Nit == nit).ToList();
            var IdNitPer = Persona.Select(x => x.IdPersona).ToList()[0];
            RadicadoCorrespondecium file;
            try
            {

                file = context.RadicadoCorrespondecia.FirstOrDefault(item => item.IdRadicado == IdNitPer ); 
                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
