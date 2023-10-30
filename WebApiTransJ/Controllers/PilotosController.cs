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
using logicLayer.Pilotos;

namespace WebApiTransJ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PilotosController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarPiloto")]
        [Authorize(Roles = "Encargado Transporte, Secretaria" )]
        public ActionResult<object> crearPiloto([FromBody] DataLayer.EntityModel.PilotoEntity piloto)

        {
            AdminPilotos oAdminPilotos = new AdminPilotos();


            if (oAdminPilotos.crearPiloto(ref piloto))
            {
                return Ok(new
                {
                    ok = true,
                    piloto.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    piloto.pTransaccionMensaje

                });
            }
        }

        [HttpPut]
        [Route("ActualizarPiloto")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> ActualizarPiloto([FromBody] DataLayer.EntityModel.PilotoEntity piloto)
        {
            logicLayer.Pilotos.AdminPilotos o = new logicLayer.Pilotos.AdminPilotos();

            if (o.ActualizarPiloto(ref piloto))
            {
                return Ok(new
                {
                    ok = true,
                    // socio.pIdAntecedente,
                    piloto.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    piloto.pTransaccionMensaje

                });
            }
        }
        [HttpPut]
        [Route("CambiarEstadoPiloto")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> cambiarEstado(int IdPiloto, string IdUsuario)
        {
            DataLayer.EntityModel.PilotoEntity piloto = new DataLayer.EntityModel.PilotoEntity();
            logicLayer.Pilotos.AdminPilotos o = new logicLayer.Pilotos.AdminPilotos(IdPiloto, IdUsuario);
      

            if (o.CambiarEstadoPiloto(ref piloto))
            {
                return Ok(new
                {
                    ok = true,
                    //piloto.pIdPiloto,
                    piloto.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    piloto.pTransaccionMensaje

                });
            }
        }

        [HttpGet]
        [Route("listaPilotos")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo, Secretaria")]
        public ActionResult<object> listaPilotos()
        {
            logicLayer.Pilotos.AdminPilotos d = new logicLayer.Pilotos.AdminPilotos();

            List<DataLayer.EntityModel.CatalogoEntityPiloto> pilotos = new List<DataLayer.EntityModel.CatalogoEntityPiloto>();

            if (d.listaPilotos(ref pilotos))
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
        [Route("listaPilotosActivos")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> PilotosActivos()
        {
            logicLayer.Pilotos.AdminPilotos d = new logicLayer.Pilotos.AdminPilotos();

            List<DataLayer.EntityModel.CatalogoEntityPilotosTJ> pilotos = new List<DataLayer.EntityModel.CatalogoEntityPilotosTJ>();

            if (d.listarPilotosActivos(ref pilotos))
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
        [Route("ListaPilotosViajes")]
        //[Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> CantidadViajesPiloto()
        {
            logicLayer.Pilotos.AdminPilotos d = new logicLayer.Pilotos.AdminPilotos();

            List<DataLayer.EntityModel.CantidadViajesPiloto> pilotos = new List<DataLayer.EntityModel.CantidadViajesPiloto>();

            if (d.cantidadViajesPiloto(ref pilotos))
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
        [Route("listaPilotosInactivos")]
        [Authorize(Roles = "Encargado Transporte, Secretaria")]
        public ActionResult<object> pilotosInactivos()
        {
            logicLayer.Pilotos.AdminPilotos d = new logicLayer.Pilotos.AdminPilotos();

            List<DataLayer.EntityModel.CatalogoEntityPilotosTJ> pilotos = new List<DataLayer.EntityModel.CatalogoEntityPilotosTJ>();

            if (d.listarPilotosInactivos(ref pilotos))
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
        [Route("listarPilotXId")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listarPilostID(int IdPiloto)
        {
            List<DataLayer.EntityModel.CatalogoEntityPilotosTJ> pilotosTJs = new List<DataLayer.EntityModel.CatalogoEntityPilotosTJ>();

            logicLayer.Pilotos.AdminPilotos opiloto = new logicLayer.Pilotos.AdminPilotos(IdPiloto);


            if (opiloto.PilotosXID(ref pilotosTJs, IdPiloto))
            {
                return Ok(new
                {
                    ok = true,
                    response = pilotosTJs
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = pilotosTJs
                });
            }
        }
        [HttpGet]
        [Route("ListaViajesXPiloto")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> ViajesPiloto(int IdPiloto,int tipMes)
        {
            List<DataLayer.EntityModel.CatalogoViajesPiloto> pilotosTJs = new List<DataLayer.EntityModel.CatalogoViajesPiloto>();

            logicLayer.Pilotos.AdminPilotos opiloto = new logicLayer.Pilotos.AdminPilotos(IdPiloto, tipMes);


            if (opiloto.ViajesXPiloto(ref pilotosTJs))
            {
                return Ok(new
                {
                    ok = true,
                    response = pilotosTJs
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = pilotosTJs
                });
            }
        }
        

    }
}
