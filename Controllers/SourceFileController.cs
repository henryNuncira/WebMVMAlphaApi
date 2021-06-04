using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using WebApiPruebaAlpha.Models;
using WebApiPruebaAlpha.Dto;

namespace WebApiPruebaAlpha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SourceFileController : ControllerBase
    {
        private readonly ILogger _logger;
        public AlphaMVMContext context = new AlphaMVMContext();

        public SourceFileController(ILogger<SourceFileController> logger)
        {
            _logger = logger;
        }
        //-----------------------********************------------------------------   

        [HttpGet]
        public IEnumerable<RadicadoCorrespondecium> GetRadicados()
        {
            List<RadicadoCorrespondecium> files;
            try
            {

                files = context.RadicadoCorrespondecia.ToList();
                return files;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

        [HttpGet("{idRadicado}")]
        public RadicadoCorrespondecium GetRadicadoId(int idRadicado)
        {
            RadicadoCorrespondecium file;
            try
            {

                file = context.RadicadoCorrespondecia.FirstOrDefault(item => item.IdRadicado == idRadicado); ;
                return file;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

       
        [HttpPost]
        public RadicadoCorrespondecium PostNewRadicadod(BodyRadicadoCorrespondencia bodyRadicado)
        {   
            RadicadoCorrespondecium newRadicado = new();


            try
            {
                if (bodyRadicado != null)
                {
                   newRadicado = new RadicadoCorrespondecium()
                    {
                        Nombre = bodyRadicado.Nombre,
                        RespaldoCorrespondencia = bodyRadicado.RespaldoCorrespondencia,
                        Estado = bodyRadicado.Estado,

                    };
                    context.RadicadoCorrespondecia.Add(newRadicado);
                    context.SaveChanges();
                    return newRadicado;
                }
                return newRadicado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }


        [HttpPut("{idRadicado}")]
        public RadicadoCorrespondecium PutUpdateRadicado(int idRadicado, RadicadoCorrespondecium Radicado)
        {
            

            RadicadoCorrespondecium entity = new();
            try
            {
                if (Radicado != null)
                {
                    entity = context.RadicadoCorrespondecia.FirstOrDefault(item => item.IdRadicado == idRadicado);

                    if (entity != null)
                    {
                        entity.IdRadicado = Radicado.IdRadicado;
                        entity.Nombre = Radicado.Nombre;
                        entity.RespaldoCorrespondencia = Radicado.RespaldoCorrespondencia;
                        entity.Estado = Radicado.Estado;

                        context.SaveChanges();
                        return entity;
                    }
                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        [HttpPost("{idRadicado}")]
        public ClasificadoRadicado PostGenerarSerial(string idRadicado)
        {
            string Ci = "CI";
            string Ce = "CE";
            int semillaInterna = 00000000;
            int semillaExterna = 00000000;
            int semilla = semillaInterna + 1;
            var newSemillai = new ClasificadoRadicado() {
                TipoRadicado = 1,
                SerialRadicado =  Ci+ Convert.ToString(semilla),
            };
            //context.ClasificadoRadicados.Add(newSemillai);
            
            ClasificadoRadicado newClasificadoSerial = new();
            var radicado = context.RadicadoCorrespondecia.Where(x => x.IdRadicado == Convert.ToInt32(idRadicado)).ToList();
            var estado = radicado.Select(x => x.Estado).ToList()[0];

            try
            {
                if (estado == 1)
                {
                    newClasificadoSerial = new ClasificadoRadicado()
                    {
                        TipoRadicado = estado,
                       


                    };
                    context.ClasificadoRadicados.Add(newClasificadoSerial);
                    context.SaveChanges();
                    return newClasificadoSerial;
                }
                else
                {

                }
                return newClasificadoSerial;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

        }
}
