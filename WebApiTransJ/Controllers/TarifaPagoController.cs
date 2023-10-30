

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
using logicLayer.Tarifa;

namespace WebApiTransJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifaPagoController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarTarifa")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> CrearTarifa([FromBody] DataLayer.EntityModel.TarifaEntity tarifa)

        {
            logicLayer.Tarifa.Tarifa o = new logicLayer.Tarifa.Tarifa();


            if (o.crearTarifa(ref tarifa))
            {
                return Ok(new
                {
                    ok = true,

                    tarifa.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    tarifa.pTransaccionMensaje

                });
            }

        }

        [HttpPut]
        [Route("ActualizarTarifa")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> actualizarTarifa([FromBody] DataLayer.EntityModel.TarifaEntity tarifa)

        {
            logicLayer.Tarifa.Tarifa o = new logicLayer.Tarifa.Tarifa();


            if (o.ActualizarTarifa(ref tarifa))
            {
                return Ok(new
                {
                    ok = true,

                    tarifa.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    tarifa.pTransaccionMensaje

                });
            }

        }
        [HttpPut]
        [Route("CambiarEstadoTarifa")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> CambiarEstado(int IdTarifa, string IdUsuario)
        {
            DataLayer.EntityModel.TarifaEntity tarifa = new DataLayer.EntityModel.TarifaEntity();
            logicLayer.Tarifa.Tarifa o = new logicLayer.Tarifa.Tarifa(IdTarifa, IdUsuario);


            if (o.CambiarEstadoTarifa(ref tarifa))
            {
                return Ok(new
                {
                    ok = true,
                    tarifa.pIdTarifa,
                    tarifa.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    tarifa.pTransaccionMensaje

                });
            }
        }
        [HttpGet]
        [Route("ListaTarifa")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo, Secretaria")]
        public ActionResult<object> ListaTarifa()
        {
            logicLayer.Tarifa.Tarifa d = new logicLayer.Tarifa.Tarifa();

            List<DataLayer.EntityModel.ListaTarifa> tarifa = new List<DataLayer.EntityModel.ListaTarifa>();

            if (d.ListarTarifas(ref tarifa))
            {
                return Ok(new
                {
                    ok = true,
                    response = tarifa
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = tarifa
                });
            }
        }
    }
}
