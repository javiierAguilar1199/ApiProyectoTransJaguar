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
using System.Formats.Tar;

namespace logicLayer.Tarifa


{
    public class Tarifa
    {
        private int __estado;
        private int ___estado;
        private int __IdTarifa;
        private int __TipMes;
        private string __TipoOperacion;
        private int estado;
        private string __IdUsuario;
        public Tarifa(int estado)
        {
            ___estado = estado;
        }
        public Tarifa()
        {

        }

        public Tarifa(int IdTarifa, string IdUsuario)
        {
            __IdTarifa= IdTarifa;
            __IdUsuario = IdUsuario;
        }

        public bool crearTarifa(ref DataLayer.EntityModel.TarifaEntity tarifa)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Tarifa", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "i");
                objStoreProc.Add_Par_Int_Input("@i_IdTarifa ", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", tarifa.pIdDepartamento);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", tarifa.pIdMunicipio);
                objStoreProc.Add_Par_Decimal_Input("@i_ValorPago", tarifa.pValor);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", tarifa.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion ", null);

                objStoreProc.Add_Par_Int_Output("@o_ret_value");
                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    tarifa.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    tarifa.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (tarifa.pTransaccionEstado == 0)
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

                tarifa.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
        public bool ActualizarTarifa(ref DataLayer.EntityModel.TarifaEntity tarifa)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                //Validar usuario en BD


                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Tarifa", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "u");
                objStoreProc.Add_Par_Int_Input("@i_IdTarifa ", tarifa.pIdTarifa);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", tarifa.pIdDepartamento);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", tarifa.pIdMunicipio);
                objStoreProc.Add_Par_Decimal_Input("@i_ValorPago", tarifa.pValor);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion ", tarifa.pIdUsuario);

                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    tarifa.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    tarifa.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (tarifa.pTransaccionEstado == 0)
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

                tarifa.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }
        public bool CambiarEstadoTarifa(ref DataLayer.EntityModel.TarifaEntity tarifa)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Tarifa", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "c");
                objStoreProc.Add_Par_Int_Input("@i_IdTarifa ", __IdTarifa);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", 0);
                objStoreProc.Add_Par_Decimal_Input("@i_ValorPago", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion ", __IdUsuario);

                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);
                int valor = 0;

                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    tarifa.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    tarifa.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");


                    if (tarifa.pTransaccionEstado == 0)
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
                tarifa.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }
        public bool ListarTarifas(ref List<ListaTarifa> tarifas)
        {
            DataLayer.EntityModel.ListaTarifa tarifa = new DataLayer.EntityModel.ListaTarifa();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("sp_TJ_Tarifa", "", "");

                objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "s");
                objStoreProc.Add_Par_Int_Input("@i_IdTarifa ", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdDepartamento", 0);
                objStoreProc.Add_Par_Int_Input("@i_IdMunicipio", 0);
                objStoreProc.Add_Par_Decimal_Input("@i_ValorPago", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Usuario_Modificacion ", null);

                objStoreProc.Add_Par_Int_Output("@o_ret_value");




                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {
                    tarifa.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    tarifa.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (tarifa.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            tarifa = new DataLayer.EntityModel.ListaTarifa();
                            tarifa.pIdTarifa = Convert.ToInt32(row["IdTarifa"]);
                            tarifa.pNombreDepto = row["Descripcion"].ToString();
                            tarifa.pNombreMuni = row["Nombre"].ToString();
                            tarifa.pValor = "Q." + Convert.ToDecimal(row["ValorMonetario"]).ToString("N2");


                            tarifas.Add(tarifa);

                        }

                        res = true;

                    }
                    else
                    {

                        tarifas.Add(tarifa);
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
                /*arifa.pIdTarifa = e.Message;*/
                tarifas.Add(tarifa);
                res = false;
            }
            return res;
        }


    }
}
