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
using logicLayer.Usuarios;
using System.Security.Claims;

namespace WebApiTransJ.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        [HttpPost]
        [Route("RegistrarUsuario")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> CrearUsuario([FromBody] DataLayer.EntityModel.UsuarioEntity usuario)

        {
            AdminUsuarios oadminUsuarios = new AdminUsuarios();


            if (oadminUsuarios.CrearUsuario(ref usuario))
            {
                return Ok(new
                {
                    ok = true,
                    usuario.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    usuario.pTransaccionMensaje

                });
            }
        }

        [HttpPut]
        [Route("ActualizarUsuario")]
        [Authorize(Roles = "Encargado Transporte, Monitoreo, Secretaria")]
        public ActionResult<object> AcutalizarUsuario([FromBody] DataLayer.EntityModel.UsuarioEntity usuario)

        {
            AdminUsuarios oadminUsuarios = new AdminUsuarios();


            if (oadminUsuarios.ActualizarUsuario(ref usuario))
            {
                return Ok(new
                {
                    ok = true,
                    usuario.pTransaccionMensaje
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    usuario.pTransaccionMensaje

                });
            }
        }
        [HttpGet]
        [Route("listaUsuarioActivo")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> listaUsuarioActivo()
        {
            logicLayer.Usuarios.AdminUsuarios d= new logicLayer.Usuarios.AdminUsuarios();

            List<DataLayer.EntityModel.listaUsuario> usuarios = new List<DataLayer.EntityModel.listaUsuario>();

            if (d.listarUsuariosAct(ref usuarios))
            {
                return Ok(new
                {
                    ok = true,
                    response = usuarios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = usuarios
                });
            }
        }
        [HttpPut]
        [Route("CambiarEstadoUsuario")]
        [Authorize(Roles = "Encargado Transporte")]
        public ActionResult<object> cambiarEstado(string IdUsuario, string UsuarioModificacion)
        {
            DataLayer.EntityModel.UsuarioEntity usuario = new DataLayer.EntityModel.UsuarioEntity();
            logicLayer.Usuarios.AdminUsuarios o = new logicLayer.Usuarios.AdminUsuarios(IdUsuario, UsuarioModificacion);

            if (o.CambiarEstadoUsuario(ref usuario))
            {
                return Ok(new
                {
                    ok = true,
                    usuario.pIdUsuario,
                    usuario.pTransaccionMensaje


                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    usuario.pTransaccionMensaje

                });
            }
        }


        [HttpGet]
        [Route("ListarUsuariosXId")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> ListarUsuario(string IdUsuario)
        {
            List<DataLayer.EntityModel.UsuarioEntity> usuario = new List<DataLayer.EntityModel.UsuarioEntity>();

            logicLayer.Usuarios.AdminUsuarios ousuario = new logicLayer.Usuarios.AdminUsuarios();


            if (ousuario.UsuarioId(ref usuario, IdUsuario))
            {
                return Ok(new
                {
                    ok = true,
                    response = usuario
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = usuario
                });
            }
        }

        
    }
}
