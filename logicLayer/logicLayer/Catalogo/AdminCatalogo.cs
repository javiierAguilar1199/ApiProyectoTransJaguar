using DataLayer.ConexionBD;
using DataLayer.EntityModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logicLayer.Catalogo
{
    public class AdminCatalogo
    {

        private int __Estado;


        public AdminCatalogo()
        {
        }

        public AdminCatalogo(int estado)
        {
            __Estado = estado;
        }

        public AdminCatalogo(string departamento)
        {
        }
        public bool listarMarcas(ref List<catalogoMarca> marcas)
        {
            DataLayer.EntityModel.catalogoMarca marca = new DataLayer.EntityModel.catalogoMarca();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("SP_MarcaUnidad", "", "");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (marca.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            marca = new DataLayer.EntityModel.catalogoMarca();
                            marca.pIdMarca = Convert.ToInt32(row["IdMarca"]);
                            marca.pMarca = row["Descripcion"].ToString();
                            //  departamento.pEstado = Convert.ToInt32(row["Estado"]);

                            marcas.Add(marca);

                        }

                        res = true;

                    }
                    else
                    {
                        marca.pTransaccionMensaje = rows["TransaccionMensaje"].ToString();
                        marcas.Add(marca);
                        res = false;
                    }

                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                marca.pTransaccionMensaje = e.Message;
                marcas.Add(marca);
                res = false;
            }
            return res;
        }
        public bool listaRoles(ref List<cataloRoles> roles)
        {
            DataLayer.EntityModel.cataloRoles rol = new DataLayer.EntityModel.cataloRoles();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("SP_Roles", "", "");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (rol.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            rol = new DataLayer.EntityModel.cataloRoles();
                            rol.pIdRol = Convert.ToInt32(row["IdRol"]);
                            rol.pDescripcion = row["Nombre_Rol"].ToString();
                            //  departamento.pEstado = Convert.ToInt32(row["Estado"]);

                            roles.Add(rol);

                        }

                        res = true;

                    }
                    else
                    {
                        rol.pTransaccionMensaje = rows["TransaccionMensaje"].ToString();
                        roles.Add(rol);
                        res = false;
                    }

                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                rol.pTransaccionMensaje = e.Message;
                roles.Add(rol);
                res = false;
            }
            return res;
        }

        public bool ViajesDepartamento(ref List<cantidadViajeDPto> departamento)
        {
            DataLayer.EntityModel.cantidadViajeDPto piloto = new DataLayer.EntityModel.cantidadViajeDPto();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_viajesXDepto", "", "");



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    //piloto.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("  "));
                    //piloto.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (piloto.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            piloto = new DataLayer.EntityModel.cantidadViajeDPto();
                           
                            piloto.pNombre = row["Descripcion"].ToString();
                            piloto.pCantidad = Convert.ToInt32(row["CantidadViajes"]);


                            departamento.Add(piloto);

                        }

                        res = true;

                    }
                    else
                    {

                        departamento.Add(piloto);
                        res = false;
                    }

                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                piloto.pTransaccionMensaje = e.Message;
                departamento.Add(piloto);
                res = false;
            }
            return res;
        }

        public bool ListarDepartamentos(ref List<CatalogoEntityMunicipio> departamentos)
        {
            DataLayer.EntityModel.CatalogoEntityMunicipio departamento = new DataLayer.EntityModel.CatalogoEntityMunicipio();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("SP_Departamentos", "", "");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (departamento.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            departamento = new DataLayer.EntityModel.CatalogoEntityMunicipio();
                            departamento.pidDepartamento = Convert.ToInt32(row["Id_Departamento"]);
                            departamento.pNombre = row["Descripcion"].ToString();
                            //  departamento.pEstado = Convert.ToInt32(row["Estado"]);

                            departamentos.Add(departamento);

                        }

                        res = true;

                    }
                    else
                    {
                        departamento.pTransaccionMensaje = rows["TransaccionMensaje"].ToString();
                        departamentos.Add(departamento);
                        res = false;
                    }

                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                departamento.pTransaccionMensaje = e.Message;
                departamentos.Add(departamento);
                res = false;
            }
            return res;
        }
        public bool ListarMunicipio(ref List<CatalogoEntityMunicipio> municipios, int departamento)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_MunicipiosXDep", "", "");

                objStoreProc.Add_Par_Int_Input("@DepartamentoId", departamento);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.CatalogoEntityMunicipio oMunicipio = new DataLayer.EntityModel.CatalogoEntityMunicipio();
                        // oMunicipio.pidDepartamento = Convert.ToInt32(row["IdDepartamento"]); ;
                        oMunicipio.pidMunicipio = Convert.ToInt32(row["IdMunicipio"]);
                        oMunicipio.pNombre = row["Nombre"].ToString();

                        if (oMunicipio.pTransaccionEstado == 0)
                        {
                            municipios.Add(oMunicipio);
                            res = true;
                        }
                        else
                        {
                            oMunicipio.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            municipios.Add(oMunicipio);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoEntityMunicipio oMunicipio = new DataLayer.EntityModel.CatalogoEntityMunicipio();
                oMunicipio.pTransaccionMensaje = e.Message;
                municipios.Add(oMunicipio);
                res = false;
            }
            return res;
        }
    

        public bool UsrOrganizacion(ref List<catalogOrganizacion> organi, string IdUsuario)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_OrgUsuario", "", "");

              
                objStoreProc.Add_Par_VarChar_Input("@IdUsuario", IdUsuario);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.catalogOrganizacion oUsrOrg = new DataLayer.EntityModel.catalogOrganizacion();

                      
                        oUsrOrg.pNombre = row["NombreOrganizacion"].ToString();
                        
                        if (oUsrOrg.pTransaccionEstado == 0)
                        {
                            organi.Add(oUsrOrg);
                            res = true;
                        }
                        else
                        {
                          
                            organi.Add(oUsrOrg);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.catalogOrganizacion oUsrOrg = new DataLayer.EntityModel.catalogOrganizacion();
                oUsrOrg.pTransaccionMensaje = e.Message;
                organi.Add(oUsrOrg);
                res = false;
            }
            return res;
        }


        public bool usrRoles(ref List<catalogoUsrRol> ROles, string IdUsuario)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("Sp_RolesUsr", "", "");


                objStoreProc.Add_Par_VarChar_Input("@IdUsuario", IdUsuario);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.catalogoUsrRol oUsrRol = new DataLayer.EntityModel.catalogoUsrRol();


                        oUsrRol.pDescripcioRol = row["Nombre_Rol"].ToString();
                        oUsrRol.pIdRol = Convert.ToInt32(row["IdRol"]);
                        oUsrRol.pIdUsuario = row["Id_Usuario"].ToString();

                        if (oUsrRol.pTransaccionEstado == 0)
                        {
                            ROles.Add(oUsrRol);
                            res = true;
                        }
                        else
                        {

                            ROles.Add(oUsrRol);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.catalogoUsrRol oUsrRol = new DataLayer.EntityModel.catalogoUsrRol();
                oUsrRol.pTransaccionMensaje = e.Message;
                ROles.Add(oUsrRol);
                res = false;
            }
            return res;
        }


        public bool InfoGeneral(ref List<catalogoInfogeneral> infogenerals)
        {
            DataLayer.EntityModel.catalogoInfogeneral infos = new DataLayer.EntityModel.catalogoInfogeneral();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("SP_infoGeneral", "", "");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (infos.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {

                            infos = new DataLayer.EntityModel.catalogoInfogeneral();
                            infos.pCantidad = Convert.ToInt32(row["ItemCount"]);
                            infos.pDescripcion = row["ItemName"].ToString();
                            infogenerals.Add(infos);

                        }

                        res = true;

                    }
                    else
                    {
                        infos.pTransaccionMensaje = rows["TransaccionMensaje"].ToString();
                        infogenerals.Add(infos);
                        res = false;
                    }

                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                infos.pTransaccionMensaje = e.Message;
                infogenerals.Add(infos);
                res = false;
            }
            return res;
        }

    }
}
