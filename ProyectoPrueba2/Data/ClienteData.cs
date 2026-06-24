using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

/*ACCESO A LA INFORMACIÓN DE CLIENTE EN LA BD*/

namespace ProyectoPrueba2.Data
{
    public class ClienteData
    {

        /*-----------------LEE CLIENTES-----------------*/
        public DataTable ObtenerClientes(string empresaId, byte operativo, string nombre) // Es el método que obtiene los clientes
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
                using (SqlCommand cmd = new SqlCommand("cargar_clientes_mod", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaid", empresaId);
                    cmd.Parameters.AddWithValue("@operativo", operativo);
                    cmd.Parameters.Add("@nombre", SqlDbType.VarChar, 100).Value =
                        string.IsNullOrWhiteSpace(nombre) ? "" : nombre.Trim(); 
                                    /*
                                       Aquí usamos Add en lugar de AddWithValue para asegurarnos de pasar el tipo y tamaño correctos
                                        El ? es operador ternario, no comprobar si es null (eso lo hace la función)
                                    */

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) //El adapter solo es necesario cuando RECIBIMOS datos. Si es un insert o update no se usa adapter 
                    {
                        DataTable dt = new DataTable();
                        conn.Open(); // abre la conexión
                        da.Fill(dt); // reyena la tabla con lo que trae el adaptador
                        return dt; // devuelve la DataTable creada y con los datos insertados
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener clientes", ex);
            }
        }

        

        /*----------------- DETALLE CLIENTE -----------------*/

        public DataTable ObtenerClienteDetalle(string empresaId, string grupoId, int cuentaId) // en este caso coge el cl exacto usando las tres claves primarias
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
                using (SqlCommand cmd = new SqlCommand("cargar_detalle_cliente_lee", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure; // indica que es un procedimiento 

                    /*Asocia las variables a los atributos del objeto*/

                    cmd.Parameters.AddWithValue("@empresaid", empresaId);
                    cmd.Parameters.AddWithValue("@grupoid", grupoId);
                    cmd.Parameters.AddWithValue("@cuentaid", cuentaId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) // usamos Adapter porque recibimos datos
                    {
                        DataTable dt = new DataTable(); //Se crea la DataTable
                        conn.Open(); // abrimos conexion
                        da.Fill(dt); // rellenamos dataTable con los datos que recibe el adapter.
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

        public bool ExisteEmpresa(string empresaId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT COUNT(1) FROM empresas WHERE empresaid = @empresaid", conn)) // aquí le estamos dando diréctamente la query
            {
                cmd.Parameters.AddWithValue("@empresaid", empresaId); // se añade el único parámetro necesario para la consulta

                conn.Open(); // abre conexion
                int count = (int)cmd.ExecuteScalar(); /* Se usa cuando la consulta devuelve un único valor. 
                                                       * En lugar de crear un DataTable, con ExecuteScallar 
                                                       * se recoge únicamente ese valor. El (int) es un casting
                                                       * porque ExecuteScalar devuelve object*/

                return count > 0; // Si es mayor que 0 es porque ha devuelto dato por lo que existe

                /*Sería equivalente a usar esto: 
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt.Rows.Count > 0;
                   Solo que esto sería mucho menos eficiente porque tiene que recoger todas las filas y columnas. 
                 */
            }
        }


        /*-------------------------------OBTIENE GRUPOS--------------------------------------*/

        public DataTable ObtenerGruposClientes(string empresaId)
        {
            using (SqlConnection conn = new SqlConnection(DbConfig.connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "SELECT grupoid FROM grupos WHERE empresaid = @empresaid ORDER BY grupoid", conn))// esto coge los grupos en funcion de la empresa id
            {
                cmd.Parameters.AddWithValue("@empresaid", empresaId); // añade el parametro necesario para la consulta
                using (SqlDataAdapter da = new SqlDataAdapter(cmd)) /* crea el adaptador ¿Se usa using porque si no habriá que cerrarlo?
                                                                     * si no se usase using, se tendría que poner da.Dispose()*/
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

