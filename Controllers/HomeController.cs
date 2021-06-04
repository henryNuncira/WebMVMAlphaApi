using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPruebaAlpha.Dto;
using WebApiPruebaAlpha.Helper;
using WebApiPruebaAlpha.Models;

namespace WebApiPruebaAlpha.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public AlphaMVMContext context = new AlphaMVMContext();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //-----------------------********************-------------------------------

        //----------------************* login *******------------------
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            List<Usuario> files;
            try
            {

                files = context.Usuarios.ToList();
                return files;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public Usuario PostNuevoUsuario(BodyUsuario usuario)
        {
            HashedPassword Password = HashHelper.Hash(usuario.Clave);
            Usuario nuevoUsuario = new();
            
            try
            {
                if (usuario != null)
                {
                        nuevoUsuario = new Usuario()

                      { 
                        Nombre = usuario.Nombre,
                        Dni = usuario.Dni,
                        Direccion = usuario.Direccion,
                        Telefono = usuario.Telefono,
                        Email = usuario.Email,
                        Clave = Password.Password,
                        Salt = Password.Salt,
                        Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray()),

                    };
                        context.Usuarios.Add(nuevoUsuario);
                        context.SaveChanges();
                    return nuevoUsuario;
                }
                return nuevoUsuario;
            }
            catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }

        }


            // actualizar o modificar uno existente

            [HttpPut("{idUsuario}")]
        public Usuario PutUsuario(int idUsuario, Usuario usuario)
        {
            Usuario entity = new();

            try {
                    if (usuario !=null) 
                    {
                    entity = context.Usuarios.FirstOrDefault(i => i.IdUsuario == idUsuario);
                    if (entity != null)
                    {
                        context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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


            //  eliminar
            [HttpDelete("{idUsuario}")]
        public Usuario DeleteUsuario(int idUsuario)
        {
            Usuario entity = new();

            try
            {
               
                    entity = context.Usuarios.FirstOrDefault(x => x.IdUsuario == idUsuario);
                if (entity != null)
                    {

                    context.Usuarios.Remove(entity);
                    context.SaveChanges();
                    return entity;

                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
           

        }


      
         public HashHelper hashelper = new HashHelper();
        [HttpPost]
        public ResponseUsuario PostLogin(BodyLogin login)
        {
            var result = context.Usuarios.Where(x => x.Nombre == login.NombreUsuario).ToList();

            if (result.Count != 0)
            {
                //traigo la salt y el password de la base de datos segun el usuario que ingresa el login
               
                var salt = result.Select(x => x.Salt).ToList()[0];
                var claveBD = result.Select(x => x.Clave).ToList()[0];
                var id = result.Select(x => x.IdRol).ToList()[0];
                var rol = context.Rols.Where(x => x.IdRol == id).ToList();
                
                // hasheo nuevamente la clave y comparo con la base de datos que sea el mismo y retorna  el hash
                var claveVerificada = hashelper.CheckHash(login.Password, claveBD, salt).ToString();
               

                //comparamos que los datos recibidos cumplan el login
                // var usuario = context.Usuarios.Where(x => (x.NombreUsuario == login.NombreUsuario)&&(x.Password == claveVerificada)).ToList();

                if (claveVerificada == claveBD)
                {

                    var rolNombre = rol.Select(x => x.Nombre).ToList()[0];
                    var token = result.Select(x => x.Token).ToList()[0];
                    return new ResponseUsuario { state = 200, message = "Usuario Activo", rol = (string)rolNombre, token = (string)token };
                    

                }
                else
                {
                    return new ResponseUsuario { state = 400, message = "Clave incorrecta No existe revise sus credenciales", rol = "vacio" };
                }

            }
            else
            {
                return new ResponseUsuario { state = 400, message = "Usuario No existe revise sus credenciales" };
            }
        }
 //----------------************* Persona contacto *******------------------
        [HttpGet]
        public IEnumerable<ContactoPersona> GetPersonaContacto()
        {
            List<ContactoPersona> files;
            try
            {

                files = context.ContactoPersonas.ToList();
                return files;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }
        [HttpPost]
        public ContactoPersona PostNuevoPersona(BodyPersona bodyPersona)
        {
           
            ContactoPersona nuevoPersona = new();

            try
            {
                if (bodyPersona != null)
                {
                    nuevoPersona = new ContactoPersona()

                    {
                        TipoContacto = bodyPersona.TipoContacto,
                        Nombre = bodyPersona.Nombre,
                        Nit = bodyPersona.Nit,
                        Correo = bodyPersona.Correo,
                        Direccion = bodyPersona.Direccion,
                        Telefono = bodyPersona.Telefono
                  
                    };
                    context.ContactoPersonas.Add(nuevoPersona);
                    context.SaveChanges();
                    return nuevoPersona;
                }
                return nuevoPersona;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }


        // actualizar o modificar uno existente

        [HttpPut("{idPersona}")]
        public ContactoPersona PutContactoPersona(int idPersona, BodyPersona bodyPersona )
        {
            ContactoPersona entity = new();

            try
            {
                if (bodyPersona != null)
                {
                    entity = context.ContactoPersonas.FirstOrDefault(i => i.IdPersona == idPersona);
                    if (entity != null)
                    {
                        context.Entry(bodyPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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


        //  eliminar
        [HttpDelete("{idPersona}")]
        public ContactoPersona DeletePersona(int idPersona)
        {
            ContactoPersona entity = new();

            try
            {

                entity = context.ContactoPersonas.FirstOrDefault(x => x.IdPersona == idPersona);
                if (entity != null)
                {

                    context.ContactoPersonas.Remove(entity);
                    context.SaveChanges();
                    return entity;

                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }


        }

    }
}
