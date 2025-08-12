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

namespace logicLayer.Horarios


{
    public class AdminHorarios
    {
        private int __estado;
        private int ___estado;
        private string __Valor;
        private int __IdHorario;
        private string __IdUsuario;
        private string __TipoOperacion;
        private int estado;
        public AdminHorarios(int estado)
        {
            ___estado = estado;
        }
        public AdminHorarios()
        {

        }
        public AdminHorarios(string dato, string v)
        {
            __Valor = dato;
            __TipoOperacion = v;
        }
        public AdminHorarios(string dato, string v, int estado)
        {
            __Valor = dato;
            __TipoOperacion = v;
            __estado = estado;
        }

        public AdminHorarios(int IdHorario, string IdUsuario)
        {
            __IdHorario = IdHorario;
            __IdUsuario = IdUsuario;
          
        }
        public bool CrearHorario(ref DataLayer.EntityModel.HorarioEntity horario)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Horario_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "i");

                objStoreProc.Add_Par_Int_Input("@i_IdHorario",0 );
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Carga", horario.pHoraEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Carga", horario.pFechaCarga);
                objStoreProc.Add_Par_VarChar_Input("@i_Planta_Carga", horario.pPlanta);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", horario.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion",null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", horario.pIdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", horario.PIdSocio);
                objStoreProc.Add_Par_Int_Input("@i_IdPIloto", horario.pIdPiloto);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");
                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    horario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    horario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (horario.pTransaccionEstado == 0)
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

                horario.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }

        public bool ActualizarHorario(ref DataLayer.EntityModel.HorarioEntity horario)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //Validar usuario en BD


                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Horario_CRUD", "", "");


                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "a");
                objStoreProc.Add_Par_Int_Input("@i_IdHorario", horario.pIdHorario);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Carga", horario.pHoraEntrega);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Carga", horario.pFechaCarga);
                objStoreProc.Add_Par_VarChar_Input("@i_Planta_Carga", horario.pPlanta);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion",null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", horario.pIdUsuario);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", horario.pIdUnidad);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", horario.PIdSocio);
                objStoreProc.Add_Par_Int_Input("@i_IdPIloto", horario.pIdPiloto);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    horario.pTransaccionEstado  = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    horario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                

                    if (horario.pTransaccionEstado == 0)
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

                horario.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
        public bool CambiarEstadoHorario(ref DataLayer.EntityModel.HorarioEntity horario)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Horario_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "c");
                objStoreProc.Add_Par_Int_Input("@i_IdHorario", __IdHorario);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Planta_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion",__IdUsuario);
     
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad",0 );
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdPIloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                int valor = 0;

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    horario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    horario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (horario.pTransaccionEstado == 0)
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
                horario.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }
        public bool listarHorarios(ref List<CatalogHorarios> horarios)
        {
            DataLayer.EntityModel.CatalogHorarios horario = new DataLayer.EntityModel.CatalogHorarios();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Horario_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "s");
                objStoreProc.Add_Par_Int_Input("@i_IdHorario", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Planta_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdPIloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (horario.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            horario = new DataLayer.EntityModel.CatalogHorarios();
                            horario.IdHorario = Convert.ToInt32(row["IdHorario"]);
                            horario.pHoraCarga = row["Hora_Carga"].ToString();
                            horario.pFechaCarga = ((DateTime)row["Fecha_Carga"]).ToString("yyyy-MM-dd");
                            horario.pPlantaCarga = row["Planta_Carga"].ToString();
                            horario.pNOmbreSocio = row["NombreSocio"].ToString();
                            horario.PNOmbrePiloto = row["NombrePiloto"].ToString();
                            horario.PCodigoUnidad = row["NombreUnidad"].ToString();
                          //  horario.pUsuario = row["Usuario_Creacion"].ToString();
                            horarios.Add(horario);

                        }

                        res = true;

                    }
                    else
                    {
                        horario.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        horarios.Add(horario);
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
                horario.pTransaccionMensaje = e.Message;
                horarios.Add(horario);
                res = false;
            }
            return res;
        }
        public bool listarHorariosInac(ref List<CatalogHorarios> horarios)
        {
            DataLayer.EntityModel.CatalogHorarios horario = new DataLayer.EntityModel.CatalogHorarios();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Horario_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "e");
                objStoreProc.Add_Par_Int_Input("@i_IdHorario", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Planta_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdPIloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (horario.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            horario = new DataLayer.EntityModel.CatalogHorarios();
                            horario.IdHorario = Convert.ToInt32(row["IdHorario"]);
                            horario.pHoraCarga = row["Hora_Carga"].ToString();
                            horario.pFechaCarga = row["Fecha_Carga"].ToString();
                            horario.pPlantaCarga = row["Planta_Carga"].ToString();
                            horario.pNOmbreSocio = row["NombreSocio"].ToString();
                            horario.PNOmbrePiloto = row["NombrePiloto"].ToString();
                            horario.PCodigoUnidad = row["NombreUnidad"].ToString();
                            horarios.Add(horario);

                        }

                        res = true;

                    }
                    else
                    {
                        horario.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        horarios.Add(horario);
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
                horario.pTransaccionMensaje = e.Message;
                horarios.Add(horario);
                res = false;
            }
            return res;
        }
        public bool horariosXID(ref List<CatalogoHorarioId> horarios, int IdHorario)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Horario_CRUD", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "o");
                objStoreProc.Add_Par_Int_Input("@i_IdHorario", IdHorario);
                objStoreProc.Add_Par_VarChar_Input("@i_Hora_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Fecha_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Planta_Carga", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_IdUnidad", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdSocio", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdPIloto", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            DataLayer.EntityModel.CatalogoHorarioId ohorario = new DataLayer.EntityModel.CatalogoHorarioId();
                            ohorario = new DataLayer.EntityModel.CatalogoHorarioId();
                            ohorario.IdHorario = Convert.ToInt32(row["IdHorario"]);
                            ohorario.pIdSocio = Convert.ToInt32(row["IdSocio"]);
                            ohorario.PIdUnidad = Convert.ToInt32(row["IdUnidad"]);
                            ohorario.pHoraCarga = row["Hora_Carga"].ToString();
                            ohorario.pFechaCarga = row["Fecha_Carga"].ToString();
                            ohorario.pIdPiloto  = Convert.ToInt32(row["IdPiloto"]);
                        
                             ohorario.pFechaCarga = ((DateTime)row["Fecha_Carga"]).ToString("yyyy-MM-dd");
                            ohorario.pPlantaCarga = row["Planta_Carga"].ToString();

                            if (ohorario.pTransaccionEstado == 0)
                            {
                                ohorario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                                ohorario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                                horarios.Add(ohorario);
                                res = true;
                            }
                            else
                            {
                                ohorario.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                                horarios.Add(ohorario);
                                res = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.CatalogoHorarioId ohorario = new DataLayer.EntityModel.CatalogoHorarioId();
                ohorario.pTransaccionMensaje = e.Message;
                horarios.Add(ohorario);
                res = false;
            }
            return res;
        }
    }
}
