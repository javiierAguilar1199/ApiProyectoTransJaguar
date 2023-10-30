using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using DataLayer;
using logicLayer.Catalogo;
using DataLayer.EntityModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Azure;
using System.Data;

namespace WebApiTransJ.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogoController : ControllerBase
    {
        [HttpGet]
        [Route("ListarMunicipio")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> ListarMunicipio(int departamento)
        {
            List<DataLayer.EntityModel.CatalogoEntityMunicipio> municipios = new List<DataLayer.EntityModel.CatalogoEntityMunicipio>();

            logicLayer.Catalogo.AdminCatalogo oMunicipio = new logicLayer.Catalogo.AdminCatalogo(departamento);


            if (oMunicipio.ListarMunicipio(ref municipios, departamento))
            {
                return Ok(new
                {
                    ok = true,
                    response = municipios
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = municipios
                });
            }
        }
        [HttpGet]
        [Route("ListDeptos")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> ListDeptos()
        {
            logicLayer.Catalogo.AdminCatalogo d = new logicLayer.Catalogo.AdminCatalogo();

            List<DataLayer.EntityModel.CatalogoEntityMunicipio> Departamentos = new List<DataLayer.EntityModel.CatalogoEntityMunicipio>();

            if (d.ListarDepartamentos(ref Departamentos))
            {
                return Ok(new
                {
                    ok = true,
                    response = Departamentos
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = Departamentos
                });
            }
        }

        [HttpGet]
        [Route("listaRoles")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listaRoles()
        {
            logicLayer.Catalogo.AdminCatalogo d = new logicLayer.Catalogo.AdminCatalogo();

            List<DataLayer.EntityModel.cataloRoles> Roles = new List<DataLayer.EntityModel.cataloRoles>();

            if (d.listaRoles(ref Roles))
            {
                return Ok(new
                {
                    ok = true,
                    response = Roles
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = Roles
                });
            }
        }

        [HttpGet]
        [Route("listaMarcas")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> listaMarcas()
        {
            logicLayer.Catalogo.AdminCatalogo d = new logicLayer.Catalogo.AdminCatalogo();

            List<DataLayer.EntityModel.catalogoMarca> Marcas = new List<DataLayer.EntityModel.catalogoMarca>();

            if (d.listarMarcas(ref Marcas))
            {
                return Ok(new
                {
                    ok = true,
                    response = Marcas
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = Marcas
                });
            }
        }
        [HttpGet]
        [Route("UsuarioOrganizacion")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> Organzacion(string IdUsuario)
        {
            List<DataLayer.EntityModel.catalogOrganizacion> UsrOrgani = new List<DataLayer.EntityModel.catalogOrganizacion>();

            logicLayer.Catalogo.AdminCatalogo ousr = new logicLayer.Catalogo.AdminCatalogo(IdUsuario);


            if (ousr.UsrOrganizacion(ref UsrOrgani, IdUsuario))
            {
                return Ok(new
                {
                    ok = true,
                    response = UsrOrgani
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = UsrOrgani
                });
            }
        }

        [HttpGet]
        [Route("UsuarioRoles")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> UsrROles(string IdUsuario)
        {
            List<DataLayer.EntityModel.catalogoUsrRol> ROles = new List<DataLayer.EntityModel.catalogoUsrRol>();

            logicLayer.Catalogo.AdminCatalogo ousr = new logicLayer.Catalogo.AdminCatalogo(IdUsuario);


            if (ousr.usrRoles(ref ROles, IdUsuario))
            {
                return Ok(new
                {
                    ok = true,
                    response = ROles
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = ROles
                });
            }
        }

        [HttpGet]
        [Route("ListaOperacion")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> ListaOperacion()
        {
            logicLayer.Catalogo.AdminCatalogo d = new logicLayer.Catalogo.AdminCatalogo();

            List<DataLayer.EntityModel.catalogoInfogeneral> infogenerals = new List<DataLayer.EntityModel.catalogoInfogeneral>();

            if (d.InfoGeneral(ref infogenerals))
            {
                return Ok(new
                {
                    ok = true,
                    response = infogenerals
                });
            }
            else
            {
                return Ok(new
                {
                    ok = false,
                    response = infogenerals
                });
            }
        }

        [HttpGet]
        [Route("ListaViajesDetpt")]
        [Authorize(Roles = "Encargado Transporte, Secretaria, Monitoreo")]
        public ActionResult<object> CantidadViajesdept()
        {
            logicLayer.Catalogo.AdminCatalogo d = new logicLayer.Catalogo.AdminCatalogo();

            List<DataLayer.EntityModel.cantidadViajeDPto> pilotos = new List<DataLayer.EntityModel.cantidadViajeDPto>();

            if (d.ViajesDepartamento(ref pilotos))
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
    }
}

