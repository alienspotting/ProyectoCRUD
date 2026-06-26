using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoCRUD
{

    /// <summary>
    /// Formulario principal de la aplicación.
    /// Contiene el panel contenedor donde se cargan los formularios hijos.
    /// </summary>
    public partial class FrmMain : Form
    {
        public FrmMain() 
        {
            InitializeComponent();
        }

        /// <summary>
        /// Carga el formulario de listado de clientes dentro del panel principal.
        /// </summary>
        private void listadoToolStripMenuItem_Click(object sender, EventArgs e) 
        {
            panelContenedor.Controls.Clear(); 

            FrmClientes frm = new FrmClientes(); 

            frm.TopLevel = false; 
            frm.FormBorderStyle = FormBorderStyle.None; 
            frm.Dock = DockStyle.Fill; 

            panelContenedor.Controls.Add(frm); 
            frm.Show(); 
        }
                
    }
}
