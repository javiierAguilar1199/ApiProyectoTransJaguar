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
using DataLayer.EntityModel;

namespace logicLayer.BitacoraViaje
{
    public class Bitacora
    {

        private int __IdBitacora;
        private string __IdUsuarioModi;
        private int __estado;
        private int ___estado;
        private string __Valor;
        private string __TipoOperacion;
        private int estado;
        public Bitacora(int estado)
        {
            ___estado = estado;
        }
        public Bitacora()
        {
      
        }

        public Bitacora(int IdBitacora, string IdUsuario)
        {
            __IdBitacora = IdBitacora;
            __IdUsuarioModi = IdUsuario;

        }
        public Bitacora(string dato, string v)
        {
            __Valor = dato;
            __TipoOperacion = v;
        }
        public Bitacora(string dato, string v, int estado)
        {
            __Valor = dato;
            __TipoOperacion = v;
            __estado = estado;
        }
        public bool CrearBitacora(ref DataLayer.EntityModel.BitacoraViajeEntity bitacora)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Bitacora_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "i");
                objStoreProc.Add_Par_VarChar_Input("@i_NoEntrega", bitacora.pIdNumeroEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Entrega", bitacora.pFechaEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Entrega", bitacora.pHoraEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Dia_Entrega", bitacora.pDiaEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Lugar_Entrega", bitacora.pLugarEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_ProducEntrega", bitacora.pProductoEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_ObservEntreg", bitacora.pObservaciones);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", bitacora.pIdPiloto);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", bitacora.pIdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", bitacora.pIdSocio);
                objStoreProc.Add_Par_Int_Input("@i_IdBitacora", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", bitacora.pIdDepartamento);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", bitacora.pIdMunicipio);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", bitacora.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
               string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                    if (string.IsNullOrEmpty(msgResEjecucion))

                {

                    bitacora.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    bitacora.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
               
                    if (bitacora.pTransaccionEstado == 0)
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

                bitacora.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }

        public bool ActualizarBitacora(ref DataLayer.EntityModel.BitacoraViajeEntity bitacora)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //Validar usuario en BD


                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Bitacora_CRUD", "", "");


                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "a");
                objStoreProc.Add_Par_VarChar_Input("@i_NoEntrega", bitacora.pIdNumeroEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Entrega", bitacora.pFechaEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Entrega", bitacora.pHoraEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Dia_Entrega", bitacora.pDiaEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Lugar_Entrega", bitacora.pLugarEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_ProducEntrega", bitacora.pProductoEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_ObservEntreg", bitacora.pObservaciones);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", bitacora.pIdPiloto);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", bitacora.pIdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", bitacora.pIdSocio);
                objStoreProc.Add_Par_Int_Input("@i_IdBitacora", bitacora.pIdBitacora);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", bitacora.pIdDepartamento);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", bitacora.pIdMunicipio);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", bitacora.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {

                    bitacora.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    bitacora.pTransaccionMensaje =(string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (bitacora.pTransaccionEstado == 0)
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

                bitacora.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
        public bool CambiarEstadoBitacora(ref DataLayer.EntityModel.BitacoraViajeEntity bitacora)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Bitacora_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "c");
                objStoreProc.Add_Par_VarChar_Input("@i_NoEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Dia_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Lugar_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ProducEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ObservEntreg", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdBitacora", __IdBitacora);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento",0);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio",0);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", __IdUsuarioModi);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");



                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                int valor = 0;

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    bitacora.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    bitacora.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");



                    if (bitacora.pTransaccionEstado == 0)
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
                bitacora.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }


        public bool listarBitacoraViaje(ref List<CatalogoBitacoraVia> bitacoraVias)
        {
            DataLayer.EntityModel.CatalogoBitacoraVia bitacoraVia = new DataLayer.EntityModel.CatalogoBitacoraVia();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Bitacora_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "s");
                objStoreProc.Add_Par_VarChar_Input("@i_NoEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Dia_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Lugar_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ProducEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ObservEntreg", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdBitacora", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    bitacoraVia.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    bitacoraVia.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (bitacoraVia.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            bitacoraVia = new DataLayer.EntityModel.CatalogoBitacoraVia();
                     
                            bitacoraVia.IdBitacora = Convert.ToInt32(row["IdBitacora"]);
                            bitacoraVia.pNoEntrega = row["NoEntrega"].ToString();
                            bitacoraVia.pFechaEntrega = ((DateTime)row["Fecha_Entrega"]).ToString("yyyy-MM-dd");
                            bitacoraVia.pHOraEntrega = row["Hora_Entrega"].ToString();
                            bitacoraVia.pDiaEntrega = row["Dia_Entrega"].ToString();
                            bitacoraVia.pLugarEntrega = row["Lugar_Entrega"].ToString();
                            bitacoraVia.pDepartamento = row["NombreDepartamento"].ToString();
                            bitacoraVia.pMunicipio = row["NombreMunicipio"].ToString();
                            bitacoraVia.pProducto = row["ProducEntrega"].ToString();
                            bitacoraVia.pObservaciones = row["ObservEntreg"].ToString();
                            bitacoraVia.PNOmbrePiloto = row["NombrePiloto"].ToString();
                            bitacoraVia.pNOmbreSocio = row["NombreSocio"].ToString();
                            bitacoraVia.PNOmbrePiloto = row["NombrePiloto"].ToString();
                            bitacoraVia.PCodigoUnidad = row["NombreUnidad"].ToString();
                            bitacoraVias.Add(bitacoraVia);

                        }

                        res = true;

                    }
                    else
                    {

                        bitacoraVias.Add(bitacoraVia);
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
                bitacoraVia.pTransaccionMensaje = e.Message;
                bitacoraVias.Add(bitacoraVia);
                res = false;
            }
            return res;
        }
        public bool listarBitacoraInact(ref List<CatalogoBitacoraVia> bitacoraVias)
        {
            DataLayer.EntityModel.CatalogoBitacoraVia bitacoraVia = new DataLayer.EntityModel.CatalogoBitacoraVia();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Bitacora_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "E");
                objStoreProc.Add_Par_VarChar_Input("@i_NoEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Dia_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Lugar_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ProducEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ObservEntreg", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdBitacora", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    bitacoraVia.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    bitacoraVia.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (bitacoraVia.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            bitacoraVia = new DataLayer.EntityModel.CatalogoBitacoraVia();

                            bitacoraVia.IdBitacora = Convert.ToInt32(row["IdBitacora"]);
                            bitacoraVia.pNoEntrega = row["NoEntrega"].ToString();
                            bitacoraVia.pFechaEntrega = row["Fecha_Entrega"].ToString();
                            bitacoraVia.pHOraEntrega = row["Hora_Entrega"].ToString();
                            bitacoraVia.pDiaEntrega = row["Dia_Entrega"].ToString();
                            bitacoraVia.pLugarEntrega = row["Lugar_Entrega"].ToString();
                            bitacoraVia.pDepartamento = row["NombreDepartamento"].ToString();
                            bitacoraVia.pMunicipio = row["NombreMunicipio"].ToString();
                            bitacoraVia.pProducto = row["ProducEntrega"].ToString();
                            bitacoraVia.pObservaciones = row["ObservEntreg"].ToString();
                            bitacoraVia.PNOmbrePiloto = row["NombrePiloto"].ToString();
                            bitacoraVia.pNOmbreSocio = row["NombreSocio"].ToString();
                            bitacoraVia.PNOmbrePiloto = row["NombrePiloto"].ToString();
                            bitacoraVia.PCodigoUnidad = row["NombreUnidad"].ToString();
                            bitacoraVias.Add(bitacoraVia);

                        }

                        res = true;

                    }
                    else
                    {

                        bitacoraVias.Add(bitacoraVia);
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
                bitacoraVia.pTransaccionMensaje = e.Message;
                bitacoraVias.Add(bitacoraVia);
                res = false;
            }
            return res;
        }
        public bool bitacoraXID(ref List<CatalogoBitacoraID> bitacoraVias, int IdBitacora)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Bitacora_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "o");
                objStoreProc.Add_Par_VarChar_Input("@i_NoEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Entrega", null);

                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Dia_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Lugar_Entrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ProducEntrega", null);
                objStoreProc.Add_Par_VarChar_Input("@i_ObservEntreg", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdBitacora", IdBitacora);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            DataLayer.EntityModel.CatalogoBitacoraID obitacora = new DataLayer.EntityModel.CatalogoBitacoraID();
                            obitacora = new DataLayer.EntityModel.CatalogoBitacoraID();
                            obitacora.pNoEntrega = row["NoEntrega"].ToString();
                            obitacora.pFechaEntrega = ((DateTime)row["Fecha_Entrega"]).ToString("yyyy-MM-dd");
                         
                            obitacora.pHOraEntrega = row["Hora_Entrega"].ToString();
                            obitacora.pDiaEntrega = row["Dia_Entrega"].ToString();
                            obitacora.pLugarEntrega = row["Lugar_Entrega"].ToString();
                            obitacora.pDepartamento = Convert.ToInt32(row["Id_Departamento"]);
                            obitacora.pMunicipio = Convert.ToInt32(row["IdMunicipio"]);
                            obitacora.pProducto = row["ProducEntrega"].ToString();
                            obitacora.pObservaciones = row["ObservEntreg"].ToString();
                            obitacora.pIdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            obitacora.pIdSocio = Convert.ToInt32(row["IdSocio"]);
                            obitacora.pIdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            obitacora.IdBitacora = Convert.ToInt32(row["IdBitacora"]);


                            if (obitacora.pTransaccionEstado == 0)
                            {

                                obitacora.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                                obitacora.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                                bitacoraVias.Add(obitacora);
                                res = true;
                            }
                            else
                            {
                                obitacora.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                                bitacoraVias.Add(obitacora);
                                res = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoBitacoraID obitacora = new DataLayer.EntityModel.CatalogoBitacoraID();
                obitacora.pTransaccionMensaje = e.Message;
                bitacoraVias.Add(obitacora);
                res = false;
            }
            return res;
        }
    }
}
