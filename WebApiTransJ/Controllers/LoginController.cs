using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using logicLayer.Seguridad;
namespace ProyectoJavierTesis.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        [Route("InicioSesion")]
        [AllowAnonymous]
        public ActionResult<object> InicioSesion([FromBody] Datalayer.EntityModel.LoginEntity login)
        {
            Login u = new Login(login.pId_usuario, login.pContrasenia);
            if (u.validaUsuario(ref login))
            {
                return Ok(new
                {
                    ok = true,
                    msg = login.pMsg,
                    token = login.pToken,
                    Id_Usuario = login.pId_usuario,
                    Nommbre = login.pNombre,
                    Direccion = login.pDireccion,
                    correo = login.pCorreo,
                    Roles = login.pDesRoles,
         
                });
            }
            else
            {

                return Ok(new
                {
                    ok = false,
                    msg = login.pMsg
                });
            }
        }

    }
}