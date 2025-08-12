using Datalayer.EntityModel;
using DataLayer.ConexionBD;
using LogicLayer.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using logicLayer.BitacoraViaje;
using Microsoft.Data.SqlClient;
using DataLayer.EntityModel;
using Microsoft.Identity.Client;

namespace logicLayer.Socios
{
    public class AdminSocios
    {
        private int __estado;
        private int ___estado;
        private string __Valor;
        private string __TipoOperacion;
        private int estado;
        private int __IdSocio;
        private int __TipMes;
        private string __IdSoci;
        private string __IdUsuario;


       
        public AdminSocios(string Nombre)
        {
            ___estado = estado;
        }
        public AdminSocios(int IdSocio)

        {

            
        }
        public AdminSocios(int IdSocio, int tipMes)
        {
            __IdSocio = IdSocio;
            __TipMes = tipMes;

        }
        public AdminSocios(int IdSocio,
                           string IdUsuario)
        {
            __IdSocio = IdSocio;
            __IdUsuario = IdUsuario;
        }

        public AdminSocios()
        {

        }
        public AdminSocios(string dato, string v, int estado)
        {
            __Valor = dato;
            __TipoOperacion = v;
            __estado = estado;
        }
        public bool CrearSocio(ref DataLayer.EntityModel.SocioEntity socio)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "i");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", socio.pNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", socio.pSNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", socio.pPApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", socio.pSApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", socio.pNoTelefono);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", socio.pNoDPI);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion", socio.pDireccion);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", socio.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio",0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");
                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    socio.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    socio.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    //DataRow rows = data.Rows[0];
                    //socio.pTransaccionEstado = Convert.ToInt32(rows["@o_ret_value"]);

                    if (socio.pTransaccionEstado == 0)
                    {

                        //DataRow row = data.Rows[0];
                        //socio.pTransaccionMensaje = row["@o_msgerror"].ToString();
                     //   socio.pIdSocio = row["Id Socio"].ToString();
                        res = true;
                    }
                    else
                    {
                    //    DataRow rowl = data.Rows[0];
                    //    socio.pTransaccionMensaje = rowl["@o_ret_value"].ToString();
                        res = false;
                    }


                }
            }
            catch (Exception e)
            {

                socio.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }

        public bool ActualizarSocio(ref DataLayer.EntityModel.SocioEntity socio)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //Validar usuario en BD
              

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");


                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "u");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", socio.pNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", socio.pSNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", socio.pPApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", socio.pSApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", socio.pNoTelefono);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", socio.pNoDPI);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion", socio.pDireccion);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", socio.pIdUsuario);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", socio.pIdSocio);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    socio.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    socio.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");



                    if (socio.pTransaccionEstado == 0)
                    {

                        res = true;
                    }
                    else
                    {
                     
                        res = false;
                    }


                }
            }
            catch (Exception e)
            {

                socio.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
        public bool CambiarEstadoSocio(ref DataLayer.EntityModel.SocioEntity socio)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "c");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono",null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion",__IdUsuario);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", __IdSocio);    
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
         

                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    socio.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    socio.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (socio.pTransaccionEstado == 0)
                    {
               
                        res = true;
                    }
                    else
                    {
         
                        res = false;
                    }
                }
            }
            catch (Exception e)
            {
                socio.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }

        public bool listarSocios(ref List<CatalogoEntitySocio> socios)
        {
            DataLayer.EntityModel.CatalogoEntitySocio socio = new DataLayer.EntityModel.CatalogoEntitySocio();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "g");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (socio.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            socio = new DataLayer.EntityModel.CatalogoEntitySocio();
                            socio.IdSocio = Convert.ToInt32(row["IdSocio"]);
                            socio.pNombre = row["NombreCompleto"].ToString();
                            //  departamento.pEstado = Convert.ToInt32(row["Estado"]);

                            socios.Add(socio);

                        }

                        res = true;

                    }
                    else
                    {
                        socio.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        socios.Add(socio);
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
                socio.pTransaccionMensaje = e.Message;
                socios.Add(socio);
                res = false;
            }
            return res;
        }

        public bool listarSociosActivos(ref List<CatalogoEntitySocioAct> socios)
        {
            DataLayer.EntityModel.CatalogoEntitySocioAct socio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "a");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion",null);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (socio.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            socio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
                            socio.IdSocio = Convert.ToInt32(row["IdSocio"]);
                            socio.pNombre = row["pNombre"].ToString();
                            socio.pSNombre = row["SNOmbre"].ToString();
                            socio.pPApellido = row["PApellido"].ToString();
                            socio.pSApellido = row["SApellido"].ToString();
                            socio.pNoTelefono = row["No_Telefono"].ToString();
                            socio.pNoDPI = row["No_DPI"].ToString();
                          
                            socios.Add(socio);

                        }

                        res = true;

                    }
                    else
                    {
                        socio.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        socios.Add(socio);
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
                socio.pTransaccionMensaje = e.Message;
                socios.Add(socio);
                res = false;
            }
            return res;
        }
        public bool listarSociosInactivos(ref List<CatalogoEntitySocioAct> socios)
        {
            DataLayer.EntityModel.CatalogoEntitySocioAct socio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "e");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (socio.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            socio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
                            socio.IdSocio = Convert.ToInt32(row["IdSocio"]);
                            socio.pNombre = row["pNombre"].ToString();
                            socio.pSNombre = row["SNOmbre"].ToString();
                            socio.pPApellido = row["PApellido"].ToString();
                            socio.pSApellido = row["SApellido"].ToString();
                            socio.pNoTelefono = row["No_Telefono"].ToString();
                            socio.pNoDPI = row["No_DPI"].ToString();

                            //  departamento.pEstado = Convert.ToInt32(row["Estado"]);

                            socios.Add(socio);

                        }

                        res = true;

                    }
                    else
                    {
                        socio.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        socios.Add(socio);
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
                socio.pTransaccionMensaje = e.Message;
                socios.Add(socio);
                res = false;
            }
            return res;
        }
        public bool SocioXID(ref List<CatalogoEntitySocioAct> socioActs, int IdSocio)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");

                objStoreProc.Add_Par_Int_Input("@i_IdSocio", IdSocio);
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "o");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.CatalogoEntitySocioAct oSocio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
                        // oMunicipio.pidDepartamento = Convert.ToInt32(row["IdDepartamento"]); ;
                        oSocio.IdSocio = Convert.ToInt32(row["IdSocio"]);
                        oSocio.pNombre = row["pNombre"].ToString();
                        oSocio.pSNombre = row["SNOmbre"].ToString();
                        oSocio.pPApellido = row["PApellido"].ToString();
                        oSocio.pSApellido = row["SApellido"].ToString();
                        oSocio.pNoTelefono = row["No_Telefono"].ToString();
                        oSocio.pDireccion = row["Direccion"].ToString();
                        oSocio.pNoDPI = row["No_DPI"].ToString();

                        if (oSocio.pTransaccionEstado == 0)
                        {
                            socioActs.Add(oSocio);
                            res = true;
                        }
                        else
                        {
                            oSocio.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            socioActs.Add(oSocio);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoEntitySocioAct oSocio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
                oSocio.pTransaccionMensaje = e.Message;
                socioActs.Add(oSocio);
                res = false;
            }
            return res;
        }
        public bool SocioXNombre(ref List<CatalogoEntitySocioAct> socioActs, string Nombre)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Socios_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", Nombre);

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "o");
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
           
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.CatalogoEntitySocioAct oSocio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
                        oSocio.IdSocio = Convert.ToInt32(row["IdSocio"]);
                        oSocio.pNombre = row["pNombre"].ToString();
                        oSocio.pSNombre = row["SNOmbre"].ToString();
                        oSocio.pPApellido = row["PApellido"].ToString();
                        oSocio.pSApellido = row["SApellido"].ToString();
                        oSocio.pNoTelefono = row["No_Telefono"].ToString();
                        oSocio.pNoDPI = row["No_DPI"].ToString();

                        if (oSocio.pTransaccionEstado == 0)
                        {
                            socioActs.Add(oSocio);
                            res = true;
                        }
                        else
                        {
                            oSocio.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            socioActs.Add(oSocio);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoEntitySocioAct oSocio = new DataLayer.EntityModel.CatalogoEntitySocioAct();
                oSocio.pTransaccionMensaje = e.Message;
                socioActs.Add(oSocio);
                res = false;
            }
            return res;
        }

        public bool ViajesXSocio(ref List<catalogoViajesSocios> SocioViaje)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_ObtenerViajesPorSocio", "", "");

                objStoreProc.Add_Par_Int_Input("@i_IdSocio  ", __IdSocio);
                objStoreProc.Add_Par_Int_Input("@i_Meses", __TipMes);


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.catalogoViajesSocios opiloto = new DataLayer.EntityModel.catalogoViajesSocios();

                        if (opiloto.pTransaccionEstado == 0)
                        {
                            opiloto.IdSocio = Convert.ToInt32(row["IdSocio"]);
                            opiloto.pNoEntrega = row["NoEntrega"].ToString();
                            opiloto.pDiaEntrega = row["Dia_Entrega"].ToString();
                            opiloto.pLugarEntrega = row["Lugar_Entrega"].ToString();
                            opiloto.pCodigoUnidad = row["CodigoUnidad"].ToString();
                            opiloto.pNombrePiloto = row["NombrePiloto"].ToString();
                            opiloto.pMunicipio = row["NombreMunicipio"].ToString();
                            opiloto.pDepartamento = row["NombreDepartamento"].ToString();
                            opiloto.pObservaciones = row["ObservEntreg"].ToString();
                            opiloto.pFechaEntrego = ((DateTime)row["Fecha_Entrega"]).ToString("yyyy-MM-dd");
                            opiloto.pUsuario = row["Usuario_Creacion"].ToString();
                            SocioViaje.Add(opiloto);
                            res = true;
                        }
                        else
                        {
                            opiloto.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            SocioViaje.Add(opiloto);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.catalogoViajesSocios opiloto = new DataLayer.EntityModel.catalogoViajesSocios();
                opiloto.pTransaccionMensaje = e.Message;
                SocioViaje.Add(opiloto);
                res = false;
            }
            return res;
        }
        public bool CantidadViajesSocios(ref List<cantidadViajesSocios> pilotos)
        {
            DataLayer.EntityModel.cantidadViajesSocios piloto = new DataLayer.EntityModel.cantidadViajesSocios();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_GetViajesPorSocio", "", "");



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
                            piloto = new DataLayer.EntityModel.cantidadViajesSocios();
                            piloto.IdSocio = Convert.ToInt32(row["IdSocio"]);
                            piloto.pNombre = row["NombreSocio"].ToString();
                            piloto.pCantidad = Convert.ToInt32(row["CantidadViajes"]);


                            pilotos.Add(piloto);

                        }

                        res = true;

                    }
                    else
                    {

                        pilotos.Add(piloto);
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
                pilotos.Add(piloto);
                res = false;
            }
            return res;
        }

    }

}
