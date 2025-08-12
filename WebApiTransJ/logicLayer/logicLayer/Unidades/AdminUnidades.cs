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
using DataLayer.EntityModel;

namespace logicLayer.Unidaes


{
    public class AdminUnidades
    {
        private int __estado;
        private int ___estado;
        private int __IdUnidad;
        private int __TipMes;
        private string __TipoOperacion;
        private int estado;
        private string __IdUsuario;
        public AdminUnidades(int estado)
        {
            ___estado = estado;
        }
        public AdminUnidades()
        {

        }
        public AdminUnidades(int IdUnidad, int tipMes)
        {
            __IdUnidad = IdUnidad;
            __TipMes = tipMes;

        }
        public AdminUnidades(int IdUnidad, string IdUsuario)
        {
            __IdUnidad = IdUnidad;
            __IdUsuario = IdUsuario;
        }
  
        public bool crearUnidad(ref DataLayer.EntityModel.UnidadEntity unidad)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "i");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ", unidad.pCodigoUnidad);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", unidad.pColorUnidad);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", unidad.pNoPlaca);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", unidad.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Modelo", unidad.PModelo);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", unidad.PIdSocio);
                objStoreProc.Add_Par_Int_Input("@i_IdMarca", unidad.pIdMarca);

                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");
                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    unidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    unidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (unidad.pTransaccionEstado == 0)
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

                unidad.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }

        public bool ActualizarUnidad(ref DataLayer.EntityModel.UnidadEntity unidad)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //Validar usuario en BD


                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");


                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "a");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ", unidad.pCodigoUnidad);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", unidad.pColorUnidad);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", unidad.pNoPlaca);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion",unidad.pIdUsuario);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", unidad.pIdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdMarca", unidad.pIdMarca);
                objStoreProc.Add_Par_VarChar_Input("@i_Modelo", unidad.PModelo);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", unidad.PIdSocio);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    unidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    unidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (unidad.pTransaccionEstado == 0)
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

                unidad.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
        public bool CambiarEstadoUnidad(ref DataLayer.EntityModel.UnidadEntity unidad)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "c");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ",null);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion",__IdUsuario);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", __IdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdMarca", 0);
                   objStoreProc.Add_Par_VarChar_Input("@i_Modelo",null);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                int valor = 0;

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    unidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    unidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (unidad.pTransaccionEstado == 0)
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
                unidad.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }
        public bool listarUnidades(ref List<CatalogoUnidades> unidades,int IdSocio)
        {
            DataLayer.EntityModel.CatalogoUnidades unidad = new DataLayer.EntityModel.CatalogoUnidades();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "g");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad",0 );
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", IdSocio);
                objStoreProc.Add_Par_Int_Input("@i_IdMarca", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Modelo", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (unidad.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            unidad = new DataLayer.EntityModel.CatalogoUnidades();
                            unidad.IdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            unidad.pCodigoUnidad = row["CodigoUnidad"].ToString();
                            unidad.pNoPlaca = row["No_PlacaUnidad"].ToString();
                            unidades.Add(unidad);

                        }

                        res = true;

                    }
                    else
                    {
                        unidad.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        unidades.Add(unidad);
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
                unidad.pTransaccionMensaje = e.Message;
                unidades.Add(unidad);
                res = false;
            }
            return res;
        }

        public bool listarUnidadesActivas(ref List<CatalogoUnidadesAct> unidades)
        {
            DataLayer.EntityModel.CatalogoUnidadesAct unidad = new DataLayer.EntityModel.CatalogoUnidadesAct();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "s");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMarca", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Modelo", null);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    unidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    unidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (unidad.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            unidad = new DataLayer.EntityModel.CatalogoUnidadesAct();
                            unidad.pIdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            unidad.PNombreSocio = row["NombreSocio"].ToString();
                            unidad.pCodigoUnidad = row["CodigoUnidad"].ToString();
                            unidad.pColor = row["ColorUnidad"].ToString();
                            unidad.pNoPlaca = row["No_PlacaUnidad"].ToString();
               
                            unidades.Add(unidad);

                        }

                        res = true;

                    }
                    else
                    {
                       
                        unidades.Add(unidad);
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
                unidad.pCodigoUnidad = e.Message;
                unidades.Add(unidad);
                res = false;
            }
            return res;
        }
        public bool listarUnidadesInactivas(ref List<CatalogoUnidadesInac> unidades)
        {
            DataLayer.EntityModel.CatalogoUnidadesInac unidad = new DataLayer.EntityModel.CatalogoUnidadesInac();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "e");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ",null);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    unidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    unidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                    if (unidad.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            unidad = new DataLayer.EntityModel.CatalogoUnidadesInac();
                            unidad.pIdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            unidad.PNombreSocio = row["NombreSocio"].ToString();
                            unidad.pCodigoUnidad = row["CodigoUnidad"].ToString();
                            unidad.pColor = row["ColorUnidad"].ToString();
                            unidad.pNoPlaca = row["No_PlacaUnidad"].ToString();

                            unidades.Add(unidad);
                        }

                        res = true;

                    }
                    else
                    {
                       
                        unidades.Add(unidad);
                        res = false;
                        unidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                        unidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                    }

                }
                else
                {
                    res = false;
                }
            }
            catch (Exception e)
            {
                unidad.pTransaccionMensaje = e.Message;
                unidades.Add(unidad);
                res = false;
            }
            return res;
        }

        public bool UnidadesXId(ref List<CatalogoUnidadesAct> unidadesActs, int IdUnidad)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Unidades_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "o");
                objStoreProc.Add_Par_VarChar_Input("@i_CodigoUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ColorUnidad ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_PlacaUnidad", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", IdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMarca", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Modelo", null);

                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            DataLayer.EntityModel.CatalogoUnidadesAct ounidad = new DataLayer.EntityModel.CatalogoUnidadesAct();
                            ounidad = new DataLayer.EntityModel.CatalogoUnidadesAct();
                            ounidad.pIdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            ounidad.PNombreSocio = row["NombreSocio"].ToString();
                            ounidad.pCodigoUnidad = row["CodigoUnidad"].ToString();
                            ounidad.pColor = row["ColorUnidad"].ToString();
                            ounidad.pNoPlaca = row["No_PlacaUnidad"].ToString();
                            ounidad.pIdSocio = Convert.ToInt32(row["IdSocio"]);
                            ounidad.pIdMarca = Convert.ToInt32(row["IdMarca"]);
                            ounidad.pModelo = row["Modelo"].ToString();


                            if (ounidad.pTransaccionEstado == 0)
                            {
                                ounidad.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                                ounidad.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                                unidadesActs.Add(ounidad);
                                res = true;
                            }
                            else
                            {
                                ounidad.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                                unidadesActs.Add(ounidad);
                                res = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoUnidadesAct ounidad = new DataLayer.EntityModel.CatalogoUnidadesAct();
                ounidad.pTransaccionMensaje = e.Message;
                unidadesActs.Add(ounidad);
                res = false;
            }
            return res;
        }

        public bool ViajesXUnidad(ref List<catalogoViajeUnidad> UnidadVia )
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_ObtenerViajesPorUnidad", "", "");

                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", __IdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_Meses", __TipMes);


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.catalogoViajeUnidad opiloto = new DataLayer.EntityModel.catalogoViajeUnidad();

                        if (opiloto.pTransaccionEstado == 0)
                        {
                            opiloto.IdUnidad = Convert.ToInt32(row["IdUnidad"]);
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
                            UnidadVia.Add(opiloto);
                            res = true;
                        }
                        else
                        {
                            opiloto.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            UnidadVia.Add(opiloto);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.catalogoViajeUnidad opiloto = new DataLayer.EntityModel.catalogoViajeUnidad();
                opiloto.pTransaccionMensaje = e.Message;
                UnidadVia.Add(opiloto);
                res = false;
            }
            return res;
        }
        public bool CantidadViajesUnidad(ref List<CantidadViajesUnidades> pilotos)
        {
            DataLayer.EntityModel.CantidadViajesUnidades piloto = new DataLayer.EntityModel.CantidadViajesUnidades();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_GetViajesPorUnidad", "", "");



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
                            piloto = new DataLayer.EntityModel.CantidadViajesUnidades();
                            piloto.IdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            piloto.CodigoUnidad = row["CodigoUnidad"].ToString();
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
