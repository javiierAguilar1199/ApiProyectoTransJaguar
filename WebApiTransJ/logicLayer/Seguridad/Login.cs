using Datalayer.EntityModel;
using DataLayer.ConexionBD;
using LogicLayer.Helper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace logicLayer.Seguridad
{
    public class Login
    {
        private string pId_usuario;
        private string pContrasenia;

        public Login(string pId_usuario, string pContrasenia)
        {
            this.pId_usuario = pId_usuario;
            this.pContrasenia = pContrasenia;
        }

        public bool validaUsuario(ref LoginEntity login)
        {
            //validaciones
            if (login.pContrasenia.Trim() == "")
            {
                login.pMsg = "La contraseña no puede estar en blanco";
                return false;
            }
            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                var root = builder.Build();
                string token = "";
                var secret = root.GetValue<string>("AppConfig:MySecret");
                if (secret != null)
                {
                    /*Validar usuario en BD*/
                    EjecProcAlm objStoreProc = new EjecProcAlm("InicioSesion", "", "");

                    objStoreProc.Add_Par_VarChar_Input("@i_tipo_transaccion", "l");
                    objStoreProc.Add_Par_VarChar_Input("@i_id_usuario", pId_usuario);
                    objStoreProc.Add_Par_VarChar_Input("@i_contrasenia", pContrasenia);

                    objStoreProc.Add_Par_VarChar_Output("@o_rol", 200);
                    objStoreProc.Add_Par_VarChar_Output("@o_id_organizacion", 200);
                    objStoreProc.Add_Par_VarChar_Output("@o_organizacion", 200);
                    objStoreProc.Add_Par_VarChar_Output("@o_correo", 100);
                    objStoreProc.Add_Par_VarChar_Output("@o_telefono", 8);
                    objStoreProc.Add_Par_VarChar_Output("@o_Direccion", 50);
                    objStoreProc.Add_Par_VarChar_Output("@o_nombre", 150);
                            

                    objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                    objStoreProc.Add_Par_Int_Output("@o_ret_value");


                    string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_parametros();
                    string o_msgError = "";
                    int o_ret_value;

                    if (string.IsNullOrEmpty(msgResEjecucion))
                    {
                        o_msgError = (string)objStoreProc.obtenerValorParametroOutput("@o_msgError");
                        o_ret_value = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                        if (o_ret_value == 0)
                        {
                            string o_rol = objStoreProc.obtenerValorParametroOutput("@o_rol").ToString();
              
                            string o_correo = (string)objStoreProc.obtenerValorParametroOutput("@o_correo").ToString();
                            string o_direccion = (string)objStoreProc.obtenerValorParametroOutput("@o_direccion").ToString();

                            string o_nombre = objStoreProc.obtenerValorParametroOutput("@o_nombre").ToString();


                            var jwtHelper = new JWTHelper();
                            token = jwtHelper.CreateToken(pId_usuario, o_rol, o_correo,o_direccion, o_nombre, secret);
                            login.pMsg = o_msgError;
                    
                            /*Distintos roles*/
                            List<string> R = new List<string>();
                            string[] rols_asignados = o_rol.ToString().Split(';');
                            /*Recorrer los roles del usuario para crear un arreglo en el Json*/
                            for (int i = 0; i < rols_asignados.Length; i++)
                            {
                                R.Add(rols_asignados[i]);
                            }
                            login.pDesRoles = R;
                            login.pCorreo = o_correo;
                            login.pDireccion = o_direccion;
                            login.pToken = token;
                            login.pNombre = o_nombre;
                          

                            return true;
                        }
                        else
                        {
                            login.pMsg = o_ret_value + " " + o_msgError;
                            return false;
                        }


                    }
                    else
                    {
                        login.pMsg = msgResEjecucion;
                        return false;
                    }


                }

            }
            catch (Exception e)
            {
                login.pMsg = e.Message;
                return false;

            }

            return res;
        }
    }
}
