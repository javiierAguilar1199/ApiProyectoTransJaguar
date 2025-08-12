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
using System.Drawing;

namespace logicLayer.Pilotos
{
    public class AdminPilotos
    {
        private int __estado;
        private int __IdPiloto;
        private int __TipMes;
        private string __IdUsuarioModi;
        private string __FechaInicio;
        private string __FechaFin;
        private int ___estado;
        private string __Valor;
        private string __TipoOperacion;
        private int estado;
        public AdminPilotos(int estado)
        {
            ___estado = estado;
        }
        public AdminPilotos()
        {

        }
      
        public AdminPilotos(string dato, string v, int estado)
        {
            __Valor = dato;
            __TipoOperacion = v;
            __estado = estado;
        }

        public AdminPilotos(int IdPiloto, string IdUsuario)
        {
            __IdPiloto = IdPiloto;
            __IdUsuarioModi = IdUsuario;
        
        }
        public AdminPilotos(int IdPiloto, int tipMes)
        {
            __IdPiloto = IdPiloto;
            __TipMes = tipMes;

        }

        public bool crearPiloto(ref DataLayer.EntityModel.PilotoEntity piloto)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "i");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", piloto.pNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", piloto.pSNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", piloto.pPApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", piloto.pSApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", piloto.pNoTelefono);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", piloto.pNoDPI);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", piloto.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", piloto.pDireccion);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");
                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    piloto.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    piloto.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                

                    if (piloto.pTransaccionEstado == 0)
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

                piloto.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }

        public bool ActualizarPiloto(ref DataLayer.EntityModel.PilotoEntity piloto)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
         

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");


                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "a");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", piloto.pNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", piloto.pSNombre);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", piloto.pPApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", piloto.pSApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", piloto.pNoTelefono);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", piloto.pNoDPI);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", piloto.pDireccion);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", piloto.pIdUsuario);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", piloto.pIdPiloto);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    piloto.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    piloto.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


         
                    if (piloto.pTransaccionEstado == 0)
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

                piloto.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
      
        public bool CambiarEstadoPiloto(ref DataLayer.EntityModel.PilotoEntity piloto )
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "c");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", __IdUsuarioModi);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", __IdPiloto);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
            
                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    piloto.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    piloto.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (piloto.pTransaccionEstado == 0) 
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
                piloto.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }

        public bool listaPilotos(ref List<CatalogoEntityPiloto> pilotos)
        {
            DataLayer.EntityModel.CatalogoEntityPiloto piloto = new DataLayer.EntityModel.CatalogoEntityPiloto();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "g");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto",0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (piloto.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            piloto = new DataLayer.EntityModel.CatalogoEntityPiloto();
                            piloto.IdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            piloto.pNombre = row["NombreCompleto"].ToString();
                           

                            pilotos.Add(piloto);

                        }

                        res = true;

                    }
                    else
                    {
                        piloto.pTransaccionMensaje = rows["@o_msgerror"].ToString();
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
       
        public bool listarPilotosActivos(ref List<CatalogoEntityPilotosTJ> pilotos)
        {
            DataLayer.EntityModel.CatalogoEntityPilotosTJ piloto = new DataLayer.EntityModel.CatalogoEntityPilotosTJ();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "s");
                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");



                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    piloto.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    piloto.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (piloto.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            piloto = new DataLayer.EntityModel.CatalogoEntityPilotosTJ();
                            piloto.IdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            piloto.pNombre = row["pNombre"].ToString();
                            piloto.pSNombre = row["SNOmbre"].ToString();
                            piloto.pPApellido = row["PApellido"].ToString();
                            piloto.pSApellido = row["SApellido"].ToString();
                            piloto.pNoTelefono = row["No_Telefono"].ToString();
                            piloto.pNoDPI = row["No_DPI"].ToString();
                            piloto.pDireccion = row["Direccion"].ToString();


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

        public bool cantidadViajesPiloto(ref List<CantidadViajesPiloto> pilotos)
        {
            DataLayer.EntityModel.CantidadViajesPiloto piloto = new DataLayer.EntityModel.CantidadViajesPiloto();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_GetViajesPorPiloto", "", "");
              


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
                            piloto = new DataLayer.EntityModel.CantidadViajesPiloto();
                            piloto.IdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            piloto.pNombre = row["NombrePiloto"].ToString();
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

        public bool listarPilotosInactivos(ref List<CatalogoEntityPilotosTJ> pilotos)
        {
            DataLayer.EntityModel.CatalogoEntityPilotosTJ piloto = new DataLayer.EntityModel.CatalogoEntityPilotosTJ();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "e");

                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    piloto.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    piloto.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                    if (piloto.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            piloto = new DataLayer.EntityModel.CatalogoEntityPilotosTJ();
                            piloto.IdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            piloto.pNombre = row["pNombre"].ToString();
                            piloto.pSNombre = row["SNOmbre"].ToString();
                            piloto.pPApellido = row["PApellido"].ToString();
                            piloto.pSApellido = row["SApellido"].ToString();
                            piloto.pNoTelefono = row["No_Telefono"].ToString();
                            piloto.pNoDPI = row["No_DPI"].ToString();
                            piloto.pDireccion = row["Direccion"].ToString();


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
        public bool PilotosXID (ref List<CatalogoEntityPilotosTJ> pilotosTJs, int IdPiloto)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Pilotos_CRUD", "", "");
                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "o");

                objStoreProc.Add_Par_VarChar_Input("@i_PNombre ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SNOmbre", null);
                objStoreProc.Add_Par_VarChar_Input("@i_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_Telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Direccion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion ", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", IdPiloto);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.CatalogoEntityPilotosTJ opiloto = new DataLayer.EntityModel.CatalogoEntityPilotosTJ();
                       
                        if (opiloto.pTransaccionEstado == 0)
                        {
                            opiloto.IdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            opiloto.pNombre = row["pNombre"].ToString();
                            opiloto.pSNombre = row["SNOmbre"].ToString();
                            opiloto.pPApellido = row["PApellido"].ToString();
                            opiloto.pSApellido = row["SApellido"].ToString();
                            opiloto.pNoTelefono = row["No_Telefono"].ToString();
                            opiloto.pNoDPI = row["No_DPI"].ToString();
                            opiloto.pDireccion = row["Direccion"].ToString();
                            pilotosTJs.Add(opiloto);
                            res = true;
                        }
                        else
                        {
                            opiloto.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            pilotosTJs.Add(opiloto);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoEntityPilotosTJ opiloto = new DataLayer.EntityModel.CatalogoEntityPilotosTJ();
                opiloto.pTransaccionMensaje = e.Message;
                pilotosTJs.Add(opiloto);
                res = false;
            }
            return res;
        }

        public bool ViajesXPiloto(ref List<CatalogoViajesPiloto> pilotosTJs)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("ObtenerViajesPorPiloto", "", "");

                objStoreProc.Add_Par_Int_Input("@i_IdPiloto", __IdPiloto);
                objStoreProc.Add_Par_Int_Input("@i_Meses", __TipMes);

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    foreach (DataRow row in data.Rows)
                    {
                        DataLayer.EntityModel.CatalogoViajesPiloto opiloto = new DataLayer.EntityModel.CatalogoViajesPiloto();

                        if (opiloto.pTransaccionEstado == 0)
                        {
                            opiloto.IdPiloto = Convert.ToInt32(row["IdPiloto"]);
                            opiloto.pNoEntrega = row["NoEntrega"].ToString();
                            opiloto.pDiaEntrega = row["Dia_Entrega"].ToString();
                            opiloto.pLugarEntrega = row["Lugar_Entrega"].ToString();
                            opiloto.pCodigoUnidad = row["CodigoUnidad"].ToString();
                            opiloto.pNombreSocio = row["NombreSocio"].ToString();
                            opiloto.pMunicipio = row["NombreMunicipio"].ToString();
                            opiloto.pDepartamento = row["NombreDepartamento"].ToString();
                            opiloto.pObservaciones = row["ObservEntreg"].ToString();
                            opiloto.pFechaEntrego = ((DateTime)row["Fecha_Entrega"]).ToString("yyyy-MM-dd");
                            opiloto.pUsuario = row["Usuario_Creacion"].ToString();
                            pilotosTJs.Add(opiloto);
                            res = true;
                        }
                        else
                        {
                            opiloto.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                            pilotosTJs.Add(opiloto);
                            res = false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoViajesPiloto opiloto = new DataLayer.EntityModel.CatalogoViajesPiloto();
                opiloto.pTransaccionMensaje = e.Message;
                pilotosTJs.Add(opiloto);
                res = false;
            }
            return res;
        }
    }
}
