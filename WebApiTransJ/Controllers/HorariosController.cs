using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using DataLayer;
using DataLayer.EntityModel;
using logicLayer.Horarios;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace WebApiTransJ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarHorario")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> CrearHorario([FromBody] DataLayer.EntityModel.HorarioEntity horario)

        {
            AdminHorarios oAdminHorarios = new AdminHorarios();


            if (oAdminHorarios.CrearHorario(ref horario))
            {
                return Ok(new
                {
                    ok = true,
                    horario.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    horario.pTransaccionMensaje

                });
            }
        }

        [HttpPut]
        [Route("ActualizarHorario")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> ActualizarHorario([FromBody] DataLayer.EntityModel.HorarioEntity horario)
        {
            logicLayer.Horarios.AdminHorarios o = new logicLayer.Horarios.AdminHorarios();

            if (o.ActualizarHorario(ref horario))
            {
                return Ok(new
                {
                    ok = true,
              
                    horario.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    horario.pTransaccionMensaje

                });
            }
        }
        [HttpPut]
        [Route("CambiarEstadoHorario")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> cambiarEstado(int IdHorario, string IdUsuario)
        {
            DataLayer.EntityModel.HorarioEntity horario = new DataLayer.EntityModel.HorarioEntity();
            logicLayer.Horarios.AdminHorarios o = new logicLayer.Horarios.AdminHorarios(IdHorario, IdUsuario);
            if (o.CambiarEstadoHorario(ref horario))
            {
                return Ok(new
                {
                    ok = true,
                    horario.pIdHorario,
                    horario.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    horario.pTransaccionMensaje

                });
            }
        }
        [HttpGet]
        [Route("listarHorarios")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> listarHorarios()
        {
            logicLayer.Horarios.AdminHorarios d = new logicLayer.Horarios.AdminHorarios();

            List<DataLayer.EntityModel.CatalogHorarios> horarios = new List<DataLayer.EntityModel.CatalogHorarios>();

            if (d.listarHorarios(ref horarios))
            {
                return Ok(new
                {
                    ok = true,
                    response = horarios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = horarios
                });
            }
        }
        [HttpGet]
        [Route("listarHorarioInactivos")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo")]
        public ActionResult<object> listarHorarioInac()
        {
            logicLayer.Horarios.AdminHorarios d = new logicLayer.Horarios.AdminHorarios();

            List<DataLayer.EntityModel.CatalogHorarios> horarios = new List<DataLayer.EntityModel.CatalogHorarios>();

            if (d.listarHorariosInac(ref horarios))
            {
                return Ok(new
                {
                    ok = true,
                    response = horarios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = horarios
                });
            }
        }
        [HttpGet]
        [Route("listarHorariosXID")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo, Secretaria")]
        public ActionResult<object> listarHorarioID(int IdHorario)
        {
            List<DataLayer.EntityModel.CatalogoHorarioId> horario = new List<DataLayer.EntityModel.CatalogoHorarioId>();

            logicLayer.Horarios.AdminHorarios ohorario = new logicLayer.Horarios.AdminHorarios(IdHorario);


            if (ohorario.horariosXID(ref horario, IdHorario))
            {
                return Ok(new
                {
                    ok = true,
                    response = horario
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = horario
                });
            }
        }
    }
}
