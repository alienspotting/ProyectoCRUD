using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProyectoPrueba2.Data
{

    /*Aquí guardamos el connectionString de manera estática para que puedan acceder a él desde todas las clases*/
    public static class DbConfig
    {
        public static string connectionString = @"Server=.\SQLEXPRESS;Database=proyectoCRUD;Trusted_Connection=True;TrustServerCertificate=True;";
    }
}
