using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCRUD.Models
{

    /// <summary>
    /// Representa un cliente de la aplicación.
    /// Contiene los datos básicos de identificación y contacto.
    /// </summary>
    internal class ClienteModel
    {      

            public string EmpresaId { get; set; }
            public string GrupoId { get; set; }
            public int CuentaId { get; set; }
            public string Nombre { get; set; }
            public string Comercial { get; set; }
            public string Direccion { get; set; }
            public string Direccion1 { get; set; }
            public string Cpid { get; set; }
            public string Poblacion { get; set; }
            public string NifId { get; set; }
            public string NifPaisId { get; set; }
            public string Telefono { get; set; }
            public string Observaciones { get; set; }


    }
}
