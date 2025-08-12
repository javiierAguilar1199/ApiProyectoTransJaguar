using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;  
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.ConexionBD
{
    public class EjecProcAlm
    {

        private SqlCommand Com;
        private SqlConnection ConSql;
        private SqlParameter Par;
        private string strConexion;

        public EjecProcAlm(string nom_proc, string usuario, string password)
        {
            prAsignarConexion(usuario, password, "", "");
            ConSql = new SqlConnection(strConexion);
            Com = new SqlCommand(nom_proc, ConSql);
            Com.CommandType = CommandType.StoredProcedure;
        }



        /// <summary>
        ///     ''' Genera la cadena de conexion.
        ///     ''' </summary>
        ///     ''' <param name="strUsuario">Usuario para conectarse a la base de datos.</param>
        ///     ''' <param name="strContrasenia">Contraseña del usuario.</param>
        private void prAsignarConexion(string strUsuario, string strContrasenia, string database = "", string servidor = "")
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.DataSource = string.IsNullOrEmpty(servidor) ? "servidorjaguares.database.windows.net" : servidor;
            builder.UserID = string.IsNullOrEmpty(strUsuario) ? "faguilar" : strUsuario;
            builder.Password = string.IsNullOrEmpty(strContrasenia) ? "Javier11" : strContrasenia;
            builder.InitialCatalog = string.IsNullOrEmpty(database) ? "DBTransJaguar" : database;
            builder.Encrypt = false;
            builder.TrustServerCertificate = false;
            strConexion = builder.ConnectionString;

    
        }

        public void Add_Par_Datetime_Input(string nombre, DateTime valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.DateTime);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Datetime_Output(string nombre, DateTime valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.DateTime);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_VarChar_Input(string nombre, string valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.VarChar);
            Par.Direction = ParameterDirection.Input;
            Par.Value = Strings.Trim(valor);
            Com.Parameters.Add(Par);
        }

        public void Add_Par_VarChar_Output(string nombre, int Tam)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.VarChar);
            Par.Size = Tam;
            Par.Direction = ParameterDirection.Output;
            // Par.Value = Trim(valor)
            Com.Parameters.Add(Par);
        }

        public void Add_Par_Int_Input(string nombre, int valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Int);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Int_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Int);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_BigInt_Input(string nombre, long valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.BigInt);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_BigInt_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.BigInt);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Float_Input(string nombre, double valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Float);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Float_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Float);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Decimal_Input(string nombre, decimal valor)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Decimal);
            Par.Direction = ParameterDirection.Input;
            Par.Value = valor;
            Com.Parameters.Add(Par);
        }
        public void Add_Par_Decimal_Output(string nombre)
        {
            // Dim Par As SqlParameter 'vector de parametros SQL
            Par = new SqlParameter(nombre, SqlDbType.Decimal);
            Par.Direction = ParameterDirection.Output;
            Com.Parameters.Add(Par);
        }

        /// <summary>
        /// RECIBE LOS N PARÁMETROS NECESARIOS PARA EJECUTAR UN PROCEDIMIENTO ALMACENADO
        /// UTILIZA DATA SET PARA LLENAR ALGUN OBJETO O DEVOLVER DATOS... 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns>UTILIZA DATA SET PARA LLENAR ALGUN OBJETO O DEVOLVER DATOS... </returns>
        public string Ejecutar_proc_alm_datatable_parametros(ref DataTable ds)
        {
            string salida = "";
            SqlDataAdapter DA;
            try
            {
                ConSql.Open();
                DA = new SqlDataAdapter(Com);
               ConSql.Close();
                DA.Fill(ds);
            }
            catch (SqlException ex)
            {
                salida = "-ERROR- " + ex.Message;
            }
            finally
            {
                ConSql.Close();
            }
            return salida;
        }

        /// <summary>
        /// RECIBE LOS N PARÁMETROS NECESARIOS PARA EJECUTAR UN PROCEDIMIENTO ALMACENADO
        /// SOLO EJECUTA EL PROCEDIMIENTO, NO DEVUELVE DATOS.
        /// </summary>
        /// <returns>Devuelve el resultado de la ejecución</returns>
        public string Ejecutar_proc_alm_parametros()
        {
            string salida = "";
            try
            {
                ConSql.Open();
                Com.ExecuteNonQuery();
                ConSql.Close();
            }
            catch (SqlException ex)
            {
                salida = "-ERROR- " + ex.Message;
            }
            finally
            {
                ConSql.Close();
            }
            return salida;
        }
        /// <summary>
        /// Esta función ejecuta un procedimiento almacenado que se configuro con 
        /// parámetros de salida o NO, y devulve el primer resultado de la primera fila
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        public string EjecutarProcParSalida(ref object result)
        {
            string Salida = "";
            try
            {
                ConSql.Open();
                result = Com.ExecuteScalar();
            }

            catch (SqlException e)
            {
                Salida = "-ERROR- " + e.Message;
            }
            finally
            {
                ConSql.Close();
            }
            return Salida;
        }

        /// <summary>
        /// Funcion que retorna el valor de un parametro.
        /// </summary>
        /// <param name="strCampo">Nombre del parametro a devolver</param>
        /// <returns></returns>
        public object obtenerValorParametroOutput(string strCampo)
        {
            return Com.Parameters[strCampo].Value;
        }

    
    }
}
