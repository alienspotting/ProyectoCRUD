using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

/// <summary>
/// Acceso a la BD para lectura.
/// </summary>

namespace ProyectoCRUD.Data
{
    public class ClienteData
    {

        /*-----------------CARGA LISTA DE CLIENTES-----------------*/

        /// <summary>
        /// Obtiene la lista de todos los clientes de una empresa
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa</param>
        /// <param name="operativo">Estado de los clientes a mostrar: 
        ///     1= operativos
        ///     2= no operativos
        ///     3= todos 
        /// </param>
        /// <param name="nombre">Opcional para filtrar por nombre</param>
        /// <returns>DataTable con la información de todos los clientes obtenidos</returns>
        
        public DataTable ObtenerClientes(string empresaId, byte operativo, string nombre) 
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("cargar_clientes_mod", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaid", empresaId);
                    cmd.Parameters.AddWithValue("@operativo", operativo);
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(nombre) ? "" : nombre.Trim(); 
                                   

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) 
                    {
                        DataTable dt = new DataTable();
                        conn.Open(); 
                        da.Fill(dt); 
                        return dt; 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes", ex);
            }
        }



        /*----------------- DETALLE CLIENTE -----------------*/
        /// <summary>
        /// Obtiene la información de un cliente en concreto para el formulario Detalle Cliente
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa</param>
        /// <param name="grupoId">Grupo dentro de la empresa al que pertenece el cliente</param>
        /// <param name="cuentaId">Identificador del cliente </param>
        /// <returns>DataTable con la información del cliente </returns>

        public DataTable ObtenerClienteDetalle(string empresaId, string grupoId, int cuentaId) 
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
                using (SqlCommand cmd = new SqlCommand("cargar_detalle_cliente_lee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; 


                    cmd.Parameters.AddWithValue("@empresaid", empresaId);
                    cmd.Parameters.AddWithValue("@grupoid", grupoId);
                    cmd.Parameters.AddWithValue("@cuentaid", cuentaId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) 
                    {
                        DataTable dt = new DataTable(); 
                        conn.Open(); 
                        da.Fill(dt); 
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalle del cliente", ex);
            }
        }

        /*-------------------------------COMPRUEBA SI EXISTE LA EMPRESA--------------------------------------*/

        /// <summary>
        /// Comprueba existencia de la empresa en la BD antes de intentar acceder a sus clientes
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa</param>     
        /// <returns>booleano indicando si la empresa existe en la BD o no</returns>
        /// 
        public bool ExisteEmpresa(string empresaId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(1) FROM empresas WHERE empresaid = @empresaid", conn)) 
            {
                cmd.Parameters.AddWithValue("@empresaid", empresaId); 

                conn.Open(); 
                int count = (int)cmd.ExecuteScalar(); 

                return count > 0;                 
            }
        }


        /*-------------------------------OBTIENE GRUPOS--------------------------------------*/

        /// <summary>
        /// Obtiene los grupos de una empresa
        /// </summary>
        /// <param name="empresaId">Identificador de la empresa</param>     
        /// <returns>DataTable con la información de los grupos</returns>
        /// 

        public DataTable ObtenerGruposClientes(string empresaId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT grupoid FROM grupos WHERE empresaid = @empresaid ORDER BY grupoid", conn))
            {
                cmd.Parameters.AddWithValue("@empresaid", empresaId); 
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) 
                                                                     
                {
                    DataTable dt = new DataTable();
                    conn.Open();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

    }
}

