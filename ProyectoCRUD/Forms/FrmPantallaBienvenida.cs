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
    /// Pantalla inicial de la aplicación.
    /// Permite acceder al formulario principal.
    /// </summary>
    public partial class FrmPantallaBienvenida : Form
    {
        public FrmPantallaBienvenida() 
        {
            InitializeComponent();
        }


        /// <summary>
        /// Abre la ventana principal y oculta la pantalla de bienvenida.
        /// </summary>
        private void btnIniciar_Click_1(object sender, EventArgs e) 
        {
            FrmMain main = new FrmMain(); 
            main.StartPosition = FormStartPosition.Manual; 
            main.Location = this.Location; 
            main.Show(); 

            this.Hide(); 

        }
    }

}
