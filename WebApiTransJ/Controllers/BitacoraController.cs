using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;  
using DataLayer;
using DataLayer.EntityModel;
using logicLayer.BitacoraViaje;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WebApiTransJ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BitacoraController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarBitacora")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> CrearBitacora([FromBody] DataLayer.EntityModel.BitacoraViajeEntity bitacora)

        {
            Bitacora oBitacora = new Bitacora();


            if (oBitacora.CrearBitacora(ref bitacora))
            {
                return Ok(new
                {
                    ok = true,
                    bitacora.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    bitacora.pTransaccionMensaje

                });
            }
        }
        [HttpPut]
        [Route("ActualizarBitacora")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> actualizarBitacora([FromBody] DataLayer.EntityModel.BitacoraViajeEntity bitacora)
        {
            logicLayer.BitacoraViaje.Bitacora o = new logicLayer.BitacoraViaje.Bitacora();
            if (o.ActualizarBitacora(ref bitacora))
            {
                return Ok(new
                {
                    ok = true,  
                    bitacora.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    bitacora.pTransaccionMensaje

                });
            }
        }
        [HttpPut]
        [Route("CambiarEstadoBitacora")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> cambiarEstado(int IdBitacora, string IdUsuario)
        {
            DataLayer.EntityModel.BitacoraViajeEntity bitacora = new DataLayer.EntityModel.BitacoraViajeEntity();
            logicLayer.BitacoraViaje.Bitacora o = new logicLayer.BitacoraViaje.Bitacora(IdBitacora, IdUsuario);


            if (o.CambiarEstadoBitacora(ref bitacora))
            {
                return Ok(new
                {
                    ok = true,
                    bitacora.pIdBitacora,
                    bitacora.pTransaccionMensaje
             
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    bitacora.pTransaccionMensaje

                });
            }
        }
        [HttpGet]
        [Route("listarBitacoraViajes")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo, Secretaria")]
        public ActionResult<object> listarBitacoraVia()
        {
            logicLayer.BitacoraViaje.Bitacora d = new logicLayer.BitacoraViaje.Bitacora();

            List<DataLayer.EntityModel.CatalogoBitacoraVia> bitacoraVias = new List<DataLayer.EntityModel.CatalogoBitacoraVia>();

            if (d.listarBitacoraViaje(ref bitacoraVias))
            {
                return Ok(new
                {
                    ok = true,
                    response = bitacoraVias
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = bitacoraVias
                });
            }
        }
        [HttpGet]
        [Route("ListarBitacoraViajeInac")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> ListarBitacoraInac()
        {
            logicLayer.BitacoraViaje.Bitacora d = new logicLayer.BitacoraViaje.Bitacora();

            List<DataLayer.EntityModel.CatalogoBitacoraVia> bitacoraVias = new List<DataLayer.EntityModel.CatalogoBitacoraVia>();

            if (d.listarBitacoraInact(ref bitacoraVias))
            {
                return Ok(new
                {
                    ok = true,
                    response = bitacoraVias
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = bitacoraVias
                });
            }
        }
        [HttpGet]
        [Route("ListarBitacoraViajeXID")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo, Secretaria")]
        public ActionResult<object> listaBitacoraID(int IdBItacora)
        {
            List<DataLayer.EntityModel.CatalogoBitacoraID> Bitacora = new List<DataLayer.EntityModel.CatalogoBitacoraID>();

            logicLayer.BitacoraViaje.Bitacora obitacora = new logicLayer.BitacoraViaje.Bitacora(IdBItacora);


            if (obitacora.bitacoraXID(ref Bitacora, IdBItacora))
            {
                return Ok(new
                {
                    ok = true,
                    response = Bitacora
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = Bitacora
                });
            }
        }
    }
}
