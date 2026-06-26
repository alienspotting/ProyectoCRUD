using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoCRUD.Data
{

    /// <summary>
    /// Contiene la configuración de conexión a la base de datos.
    /// </summary>

    public static class DbConfig
    {
        public static string ConnectionString = @"Server=.\SQLEXPRESS;Database=proyectoCRUD;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
