using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCRUD.Data
{
    internal class ClienteUpdateData
    {
        /*----------------- GUARDAR CLIENTE ACTUALIZADO -----------------*/
        /// <summary>
        /// Ejecuta el procedimiento almacenado de mantenimiento para insertar o actualizar cliente.
        /// </summary> 
        /// /// <param name="modo">
        /// Operación a realizar:
        ///     1 = actualizar,
        ///     2 = insertar,
        ///     3 = eliminar.
        /// </param>
        /// <returns> Devuelve el identificador generado para el nuevo cliente cuando se realiza una inserción.</returns>
        ///

        public int GuardarCliente(

            int modo,
            string empresaId,
            string grupoId,
            int cuentaId,
            string nombre,
            string comercial,
            string direccion,
            string direccion1,
            string cpid,
            string poblacion,
            string nifid,
            string nifpaisid,
            string telefono,
            string observaciones,
            bool operativo)
        {            

            using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("detalle_cliente_mantenimiento", conn)) 
            {
                cmd.CommandType = CommandType.StoredProcedure; 

                
                cmd.Parameters.AddWithValue("@modo", modo); 
                cmd.Parameters.AddWithValue("@empresaid", empresaId);
                cmd.Parameters.AddWithValue("@grupoid", grupoId);
                cmd.Parameters.AddWithValue("@cuentaid", cuentaId);
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cmd.Parameters.AddWithValue("@comercial", comercial);
                cmd.Parameters.AddWithValue("@direccion", direccion);
                cmd.Parameters.AddWithValue("@direccion1", direccion1);
                cmd.Parameters.AddWithValue("@cpid", cpid);
                cmd.Parameters.AddWithValue("@poblacion", poblacion);
                cmd.Parameters.AddWithValue("@nifid", nifid);
                cmd.Parameters.AddWithValue("@nifpaisid", nifpaisid);
                cmd.Parameters.AddWithValue("@telefono", telefono);
                cmd.Parameters.AddWithValue("@observaciones", observaciones);
                cmd.Parameters.AddWithValue("@operativo", operativo);

                

                conn.Open(); 
                if (modo == 2)
                {
                    
                    object resultado = cmd.ExecuteScalar();
                    return Convert.ToInt32(resultado); 
                }
                else
                {
                    cmd.ExecuteNonQuery();
                    return 0; 
                }
                
            }
        }

        /// <summary>
        /// Llama al procedimiento almacenado para eliminar un cliente de la base de datos
        /// </summary>       
        /// <param name="empresaId">Identificador de la empresa</param>
        /// <param name="grupoId">Grupo del cliente</param>
        /// <param name="cuentaId">ID único del cliente</param>
        ///
        public void EliminarCliente(string empresaId, string grupoId, int cuentaId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.ConnectionString))
            using (SqlCommand cmd = new SqlCommand("detalle_cliente_mantenimiento", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@modo", 3);
                cmd.Parameters.AddWithValue("@empresaid", empresaId);
                cmd.Parameters.AddWithValue("@grupoid", grupoId);
                cmd.Parameters.AddWithValue("@cuentaid", cuentaId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
