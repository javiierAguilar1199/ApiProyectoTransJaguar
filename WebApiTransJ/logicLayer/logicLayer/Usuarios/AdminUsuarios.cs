using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LogicLayer.Helper;
using DataLayer.ConexionBD;
using Microsoft.Data.SqlClient;
using DataLayer.EntityModel;
using Microsoft.VisualBasic;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Forms;
using logicLayer.BitacoraViaje;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace logicLayer.Usuarios
{
    public class AdminUsuarios
    {

        private string _username;
        private string _password;
        private string __IdUsuario;
        private string __Usuario_Modificacion;


        
        private HttpContext context;






        public AdminUsuarios(string IdUsuario)
        {
            __IdUsuario = IdUsuario;
         
        }
        public AdminUsuarios(string IdUsuario, string UsuarioModificacion)
        {
            __IdUsuario = IdUsuario;
            __Usuario_Modificacion = UsuarioModificacion;

        }

        public AdminUsuarios()
        {

        }


        public AdminUsuarios(string username, string password, string? pId_usuario_modificacion)
        {
            _username = username;
            _password = password;
        }




        public bool CrearUsuario(ref DataLayer.EntityModel.UsuarioEntity usuario)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("Sp_AdminUsuario", "", "");

                objStoreProc.Add_Par_VarChar_Input("@I_tipo_transaccion", "i");
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario", usuario.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@I_PNombre", usuario.pPNombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SNombre", usuario.SNombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PApellido", usuario.pPApellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SApellido", usuario.pSApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_direccion", usuario.pDireccion);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", usuario.pNoDPI);
                objStoreProc.Add_Par_VarChar_Input("@i_telefono", usuario.pNoTelefono);
                objStoreProc.Add_Par_VarChar_Input("@i_Correo", usuario.pCorreo);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_creacion", usuario.pIdUsuarioCreacion);
                objStoreProc.Add_Par_VarChar_Input("@i_contrasenia", usuario.pContrasenia);
                objStoreProc.Add_Par_Int_Input("@i_id_rol", usuario.pIdRol);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_id_organizacion", usuario.pIdOrganizacion);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {

                    usuario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    usuario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (usuario.pTransaccionEstado == 0)
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

                usuario.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }



        public bool ActualizarUsuario(ref DataLayer.EntityModel.UsuarioEntity usuario)
        {

            bool res = false;
            try
            {

                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("Sp_AdminUsuario", "", "");

                objStoreProc.Add_Par_VarChar_Input("@I_tipo_transaccion", "u");
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario", usuario.pIdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@I_PNombre", usuario.pPNombre);
                objStoreProc.Add_Par_VarChar_Input("@I_SNombre", usuario.SNombre);
                objStoreProc.Add_Par_VarChar_Input("@I_PApellido", usuario.pPApellido);
                objStoreProc.Add_Par_VarChar_Input("@I_SApellido", usuario.pSApellido);
                objStoreProc.Add_Par_VarChar_Input("@i_direccion", usuario.pDireccion);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", usuario.pNoDPI);
                objStoreProc.Add_Par_VarChar_Input("@i_telefono", usuario.pNoTelefono);
                objStoreProc.Add_Par_VarChar_Input("@i_Correo", usuario.pCorreo);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_creacion", usuario.pIdUsuarioCreacion);
                objStoreProc.Add_Par_VarChar_Input("@i_contrasenia", usuario.pContrasenia);
                objStoreProc.Add_Par_Int_Input("@i_id_rol", usuario.pIdRol);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_id_organizacion", usuario.pIdOrganizacion);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {

                    usuario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    usuario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (usuario.pTransaccionEstado == 0)
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

                usuario.pTransaccionMensaje = e.Message;
                res = false;

            }
            return res;
        }

        public bool listarUsuariosAct(ref List<listaUsuario> usuarios)
        {
            DataLayer.EntityModel.listaUsuario usuario = new DataLayer.EntityModel.listaUsuario();
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                /*Validar usuario en BD*/
                EjecProcAlm objStoreProc = new EjecProcAlm("Sp_AdminUsuario", "", "");

                objStoreProc.Add_Par_VarChar_Input("@I_tipo_transaccion", "s");
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PNombre", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SNombre", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PApellido",null);
                objStoreProc.Add_Par_VarChar_Input("@I_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_direccion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Correo", null);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_contrasenia",null);
                objStoreProc.Add_Par_Int_Input("@i_id_rol", 0);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_modificacion", null);
                objStoreProc.Add_Par_Int_Input("@i_id_organizacion",0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                DataRow rows = data.Rows[0];


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    if (usuario.pTransaccionEstado == 0)
                    {

                        foreach (DataRow row in data.Rows)
                        {
                            usuario = new DataLayer.EntityModel.listaUsuario();
                            usuario.pIdUsuario = row["Id_Usuario"].ToString();
                            usuario.pPNombre = row["PNombre"].ToString();
                            usuario.SNombre = row["SNombre"].ToString();
                            usuario.pPApellido = row["PApellido"].ToString();
                            usuario.pSApellido = row["SApellido"].ToString();
                            usuario.pNoTelefono = row["No_Telefono"].ToString();
                            usuario.pNoDPI = row["No_DPI"].ToString();
                            usuario.pCorreo = row["Correo"].ToString();
                            usuario.pDireccion = row["Direccion"].ToString();
                            usuarios.Add(usuario);

                        }

                        res = true;

                    }
                    else
                    {
                        usuario.pTransaccionMensaje = rows["@o_msgerror"].ToString();
                        usuarios.Add(usuario);
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
                usuario.pTransaccionMensaje = e.Message;
                usuarios.Add(usuario);
                res = false;
            }
            return res;
        }

        public bool CambiarEstadoUsuario(ref DataLayer.EntityModel.UsuarioEntity usuario)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                EjecProcAlm objStoreProc = new EjecProcAlm("Sp_AdminUsuario", "", "");

                objStoreProc.Add_Par_VarChar_Input("@I_tipo_transaccion", "c");
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario", __IdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@I_PNombre", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SNombre", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_direccion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Correo", null);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_contrasenia", null);
                objStoreProc.Add_Par_Int_Input("@i_id_rol", 0);
                
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_modificacion", __Usuario_Modificacion);

               objStoreProc.Add_Par_Int_Input("@i_id_organizacion", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");


                //objeto que se llama data y se lo pasamos como ref
                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);


                if (string.IsNullOrEmpty(msgResEjecucion))
                {

                    usuario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                    usuario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");

                    if (usuario.pTransaccionEstado == 0)
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
                usuario.pTransaccionMensaje = e.Message;
                res = false;
            }

            return res;

        }

      
        public bool UsuarioId(ref List<UsuarioEntity> usuario, string IdUsuario)
        {
            bool res = false;
            try
            {
                IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                EjecProcAlm objStoreProc = new EjecProcAlm("Sp_AdminUsuario", "", "");

                objStoreProc.Add_Par_VarChar_Input("@I_tipo_transaccion", "o");
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario", IdUsuario);
                objStoreProc.Add_Par_VarChar_Input("@I_PNombre", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SNombre", null);
                objStoreProc.Add_Par_VarChar_Input("@I_PApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@I_SApellido", null);
                objStoreProc.Add_Par_VarChar_Input("@i_direccion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_No_DPI", null);
                objStoreProc.Add_Par_VarChar_Input("@i_telefono", null);
                objStoreProc.Add_Par_VarChar_Input("@i_Correo", null);
                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_creacion", null);
                objStoreProc.Add_Par_VarChar_Input("@i_contrasenia", null);
                objStoreProc.Add_Par_Int_Input("@i_id_rol", 0);

                objStoreProc.Add_Par_VarChar_Input("@i_id_usuario_modificacion", null);

                objStoreProc.Add_Par_Int_Input("@i_id_organizacion", 0);
                objStoreProc.Add_Par_VarChar_Output("@o_msgerror", 200);
                objStoreProc.Add_Par_Int_Output("@o_ret_value");

                DataTable data = new DataTable();
                string msgResEjecucion = objStoreProc.Ejecutar_proc_alm_datatable_parametros(ref data);

                if (string.IsNullOrEmpty(msgResEjecucion))

                {
                    {
                        foreach (DataRow row in data.Rows)
                        {
                            DataLayer.EntityModel.UsuarioEntity ousuario = new DataLayer.EntityModel.UsuarioEntity();
                            ousuario = new DataLayer.EntityModel.UsuarioEntity();
                            ousuario.pIdUsuario = row["Id_Usuario"].ToString();
                            ousuario.pPNombre = row["PNombre"].ToString();
                            ousuario.SNombre = row["SNombre"].ToString();
                            ousuario.pPApellido = row["PApellido"].ToString();
                            ousuario.pDireccion = row["Direccion"].ToString();
                            ousuario.pNoDPI = row["No_DPI"].ToString();
                            ousuario.pNoTelefono = row["No_Telefono"].ToString();
                            ousuario.pCorreo = row["Correo"].ToString();
                            ousuario.pSApellido = row["SApellido"].ToString();
                            ousuario.pIdRol = Convert.ToInt32(row["IdRol"]);

                            if (ousuario.pTransaccionEstado == 0)
                            {
                                ousuario.pTransaccionEstado = Convert.ToInt32(objStoreProc.obtenerValorParametroOutput("@o_ret_value"));
                                ousuario.pTransaccionMensaje = (string)objStoreProc.obtenerValorParametroOutput("@o_msgerror");
                                usuario.Add(ousuario);
                                res = true;
                            }
                            else
                            {
                                ousuario.pTransaccionMensaje = row["TransaccionMensaje"].ToString();
                                usuario.Add(ousuario);
                                res = false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                DataLayer.EntityModel.UsuarioEntity ousuario = new DataLayer.EntityModel.UsuarioEntity();
                ousuario.pTransaccionMensaje = e.Message;
                usuario.Add(ousuario);
                res = false;
            }
            return res;
        }


    }
}




