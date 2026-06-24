using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrueba2.Data
{
    internal class ClienteUpdateData
    {
        /*----------------- GUARDAR CLIENTE ACTUALIZADO -----------------*/

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

            /*
                Son dos using anidados pero sin llaves intermedias, es una forma abreviada. Equivale a:

                    using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("tranet.tran_clientes_mant_act_c", conn))
                        {
                            // código
                        }
                    }

                  El using en C# garantiza que los objetos se cierran y liberan de memoria automáticamente al terminar, aunque haya un error. 
                  Sin using tendrías que hacer conn.Close() y cmd.Dispose() manualmente.
             */


            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand("detalle_cliente_mantenimiento", conn)) // estas dos cosas unidas así no las entiendo bien
            {
                cmd.CommandType = CommandType.StoredProcedure; // indica que estamos introduciendo un procedimiento, no haciendo una consulta directa

                /*Aquí ya empieza a coger los atributos del objeto y asociarlos a las  variables que pide el SP. Lo que no sé es AddWithValue, ¿qué otras opciones hay? */
                cmd.Parameters.AddWithValue("@modo", modo); // elige si es usuario nuevo o modificar

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

                /*Si pones add, tienes que especificar el tipo de dato SQL, y AddWithvalue lo deduce solo.
                    Más cómodo pero puede causar problemas de rendimiento.
                La alternativa sería:
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value = nombre;
                 */

                conn.Open(); // Abre la conexión
                if (modo == 2)
                {
                    // Recoge el nuevoCuentaId que devuelve el SP
                    object resultado = cmd.ExecuteScalar();
                    return Convert.ToInt32(resultado);
                }
                else
                {
                    cmd.ExecuteNonQuery();
                    return 0; // en edición no necesitamos el id
                }

                /*
                                        . En ADO.NET ( ADO.NET es la tecnología de .NET para comunicarse con bases de datos. ) hay tres opciones:
                                                ExecuteNonQuery → para INSERT, UPDATE, DELETE. No devuelve datos, solo el número de filas afectadas
                                                ExecuteScalar → para consultas que devuelven un único valor, como SELECT COUNT(*)
                                                ExecuteReader → para consultas que devuelven múltiples filas
                                        */
            }
        }
    

    public void EliminarCliente(string empresaId, string grupoId, int cuentaId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
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
