using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using DataLayer;
using DataLayer.EntityModel;
using logicLayer.Unidaes;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using logicLayer;
using logicLayer.Unidaes;

namespace WebApiTransJ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarUnidad")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> CrearUnidad([FromBody] DataLayer.EntityModel.UnidadEntity unidad)

        {
            AdminUnidades oadminUnidades = new AdminUnidades();


            if (oadminUnidades.crearUnidad(ref unidad))
            {
                return Ok(new
                {
                    ok = true,
                    unidad.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    unidad.pTransaccionMensaje

                });
            }
        }

        [HttpPut]
        [Route("ActualizarUnidad")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> ActualizarUnidad([FromBody] DataLayer.EntityModel.UnidadEntity unidad)
        {
            logicLayer.Unidaes.AdminUnidades o = new logicLayer.Unidaes.AdminUnidades();

            if (o.ActualizarUnidad(ref unidad))
            {
                return Ok(new
                {
                    ok = true,
                    // socio.pIdAntecedente,
                    unidad.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    unidad.pTransaccionMensaje

                });
            }
        }
        [HttpPut]
        [Route("CambiarEstadoUnidad")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> cambiarEstado(int IdUnidad, string IdUsuario)
        {
            DataLayer.EntityModel.UnidadEntity unidad = new DataLayer.EntityModel.UnidadEntity();
            logicLayer.Unidaes.AdminUnidades o = new logicLayer.Unidaes.AdminUnidades(IdUnidad, IdUsuario);


            if (o.CambiarEstadoUnidad(ref unidad))
            {
                return Ok(new
                {
                    ok = true,
                    unidad.pIdUnidad,
                   unidad.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    unidad.pTransaccionMensaje

                });
            }
        }
        [HttpGet]
        [Route("listarUnidades")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listaUnidades(int IdSocio)
        {
            logicLayer.Unidaes.AdminUnidades d = new logicLayer.Unidaes.AdminUnidades();

            List<DataLayer.EntityModel.CatalogoUnidades> unidades = new List<DataLayer.EntityModel.CatalogoUnidades>(IdSocio);

            if (d.listarUnidades(ref unidades, IdSocio))
            {
                return Ok(new
                {
                    ok = true,
                    response = unidades
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = unidades
                });
            }
        }

        [HttpGet]
        [Route("listarUnidadesActivas")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> listarUniadesAct()
        {
            logicLayer.Unidaes.AdminUnidades d = new logicLayer.Unidaes.AdminUnidades();

            List<DataLayer.EntityModel.CatalogoUnidadesAct> unidades = new List<DataLayer.EntityModel.CatalogoUnidadesAct>();

            if (d.listarUnidadesActivas(ref unidades))
            {
                return Ok(new
                {
                    ok = true,
                    response = unidades
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = unidades
                });
            }
        }

        [HttpGet]
        [Route("listarUnidadesInactivas")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> listarUnidadesInac()
        {
            logicLayer.Unidaes.AdminUnidades d = new logicLayer.Unidaes.AdminUnidades();

            List<DataLayer.EntityModel.CatalogoUnidadesInac> unidades = new List<DataLayer.EntityModel.CatalogoUnidadesInac>();

            if (d.listarUnidadesInactivas(ref unidades))
            {
                return Ok(new
                {
                    ok = true,
                    response = unidades
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = unidades
                });
            }
        }
        [HttpGet]
        [Route("listarUnidadesXId")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listarUnidadesXID(int IdUnidad)
        {
            List<DataLayer.EntityModel.CatalogoUnidadesAct> unidad = new List<DataLayer.EntityModel.CatalogoUnidadesAct>();

            logicLayer.Unidaes.AdminUnidades ounidad = new logicLayer.Unidaes.AdminUnidades(IdUnidad);


            if (ounidad.UnidadesXId(ref unidad, IdUnidad))
            {
                return Ok(new
                {
                    ok = true,
                    response = unidad
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = unidad
                });
            }
        }
        [HttpGet]
        [Route("ListaCantidadViajeUnidad")]
        //[Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> cantidadvijaesUnidad()
        {
            logicLayer.Unidaes.AdminUnidades d = new logicLayer.Unidaes.AdminUnidades();

            List<DataLayer.EntityModel.CantidadViajesUnidades> pilotos = new List<DataLayer.EntityModel.CantidadViajesUnidades>();

            if (d.CantidadViajesUnidad(ref pilotos))
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
        [Route("ListaViajesUnidad")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> ViajesUnidad(int IdUnidad, int tipMes)
        {
            List<DataLayer.EntityModel.catalogoViajeUnidad> SocioViaje = new List<DataLayer.EntityModel.catalogoViajeUnidad>();

            logicLayer.Unidaes.AdminUnidades opiloto = new logicLayer.Unidaes.AdminUnidades(IdUnidad, tipMes);


            if (opiloto.ViajesXUnidad(ref SocioViaje))
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
