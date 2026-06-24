using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoPrueba2
{
    public partial class FrmPantallaBienvenida : Form
    {
        public FrmPantallaBienvenida() // constructor
        {
            InitializeComponent();
        }

        

        private void btnIniciar_Click_1(object sender, EventArgs e) // evento de pulsar iniciar
        {
            FrmMain main = new FrmMain(); // crea un FrmMain que es donde se muestra el formulario clientes, está el menú etc
            main.StartPosition = FormStartPosition.Manual; /*esto es para poder configurar de manera manual dónde se abre el FrmMain*/
            main.Location = this.Location; /*esto le dice que se abra en la misma localización donde está la ventana actual (la de inicio) */
            main.Show(); // muestra la ventana de main

            this.Hide(); // esconde esta

        }
    }

}
