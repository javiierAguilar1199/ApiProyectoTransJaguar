using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using DataLayer;
using DataLayer.EntityModel;
using logicLayer.Socios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WebApiTransJ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SociosController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarSocio")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> CrearSocio([FromBody] DataLayer.EntityModel.SocioEntity socio)

        {
            AdminSocios oAdminSocios = new AdminSocios();


            if (oAdminSocios.CrearSocio(ref socio))
            {
                return Ok(new
                {
                    ok = true,
                    socio.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    socio.pTransaccionMensaje

                });
            }
        }

        [HttpPut]
        [Route("ActualizarSocios")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> AcutalizarSocio([FromBody] DataLayer.EntityModel.SocioEntity socio)
        {
            logicLayer.Socios.AdminSocios o = new logicLayer.Socios.AdminSocios();

            if (o.ActualizarSocio(ref socio))
            {
                return Ok(new
                {
                    ok = true,
            
                    socio.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    socio.pTransaccionMensaje

                });
            }
        }
        [HttpPut]
        [Route("CambiarEstadoSocio")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> cambiarEstado(int IdSocio, string IdUsuario)
        {
            DataLayer.EntityModel.SocioEntity socio = new DataLayer.EntityModel.SocioEntity();
            logicLayer.Socios.AdminSocios o = new logicLayer.Socios.AdminSocios(IdSocio,IdUsuario);

            if (o.CambiarEstadoSocio(ref socio))
            {
                return Ok(new
                {
                    ok = true,
                    socio.pIdSocio,
                    socio.pTransaccionMensaje

                    
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    socio.pTransaccionMensaje

                });
            }
        }
       
        [HttpGet]
        [Route("listaSocios")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listaSocios()
        {
            logicLayer.Socios.AdminSocios d = new logicLayer.Socios.AdminSocios();

            List<DataLayer.EntityModel.CatalogoEntitySocio> socios = new List<DataLayer.EntityModel.CatalogoEntitySocio>();

            if (d.listarSocios(ref socios))
            {
                return Ok(new
                {
                    ok = true,
                    response = socios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = socios
                });
            }
        }
        [HttpGet]
        [Route("listaSociosActivos")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> listaSociosActivos()
        {
            logicLayer.Socios.AdminSocios d = new logicLayer.Socios.AdminSocios();

            List<DataLayer.EntityModel.CatalogoEntitySocioAct> socios = new List<DataLayer.EntityModel.CatalogoEntitySocioAct>();

            if (d.listarSociosActivos(ref socios))
            {
                return Ok(new
                {
                    ok = true,
                    response = socios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = socios
                });
            }
        }
        [HttpGet]
        [Route("listaSociosInactivos")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> listaSociosInactivos()
        {
            logicLayer.Socios.AdminSocios d = new logicLayer.Socios.AdminSocios();

            List<DataLayer.EntityModel.CatalogoEntitySocioAct> socios = new List<DataLayer.EntityModel.CatalogoEntitySocioAct>();

            if (d.listarSociosInactivos(ref socios))
            {
                return Ok(new
                {
                    ok = true,
                    response = socios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = socios
                });
            }
        }
        [HttpGet]
        [Route("ListarSocioXId")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listarSocioXID(int IdSocio)
        {
            List<DataLayer.EntityModel.CatalogoEntitySocioAct> socioActs = new List<DataLayer.EntityModel.CatalogoEntitySocioAct>();

            logicLayer.Socios.AdminSocios oSocio = new logicLayer.Socios.AdminSocios();


            if (oSocio.SocioXID(ref socioActs, IdSocio))
            {
                return Ok(new
                {
                    ok = true,
                    response = socioActs
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = socioActs
                });
            }
        }
        [HttpGet]
        [Route("ListarSocioXNombre")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> listarSocioNombre(string Nombre)
        {
            List<DataLayer.EntityModel.CatalogoEntitySocioAct> socioActs = new List<DataLayer.EntityModel.CatalogoEntitySocioAct>();

            logicLayer.Socios.AdminSocios oSocio = new logicLayer.Socios.AdminSocios(Nombre);


            if (oSocio.SocioXNombre(ref socioActs, Nombre))
            {
                return Ok(new
                {
                    ok = true,
                    response = socioActs
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = socioActs
                });
            }
        }
        [HttpGet]
        [Route("CantidadViajeXSocio")]
        //[Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> CantidadViajesSocios()
        {
            logicLayer.Socios.AdminSocios d = new logicLayer.Socios.AdminSocios();

            List<DataLayer.EntityModel.cantidadViajesSocios> pilotos = new List<DataLayer.EntityModel.cantidadViajesSocios>();

            if (d.CantidadViajesSocios(ref pilotos))
            {
                return Ok(new
                {
                    ok = true,
                    response = pilotos
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = pilotos
                });
            }
        }


        [HttpGet]
        [Route("ListaViajesXSocio")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> viajesSocios(int IdSocio, int tipMes)
        {
            List<DataLayer.EntityModel.catalogoViajesSocios> SocioViaje = new List<DataLayer.EntityModel.catalogoViajesSocios>();

            logicLayer.Socios.AdminSocios opiloto = new logicLayer.Socios.AdminSocios(IdSocio, tipMes);

            if (opiloto.ViajesXSocio(ref SocioViaje))
            {
                return Ok(new
                {
                    ok = true,
                    response = SocioViaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = SocioViaje
                });
            }
        }
    }
}
