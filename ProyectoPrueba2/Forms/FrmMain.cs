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
    public partial class FrmMain : Form
    {
        public FrmMain() // El constructor
        {
            InitializeComponent();
        }

        private void listadoToolStripMenuItem_Click(object sender, EventArgs e) /*Cuando se clica sobre "listado" se activa el evento*/
        {
            panelContenedor.Controls.Clear(); // primero elimina todo lo que hay en el contenedor

            FrmClientes frm = new FrmClientes(); // crea un nuevo formulario clientes 

            frm.TopLevel = false; /*Normalmente los formularios son ventanas independientes flotantes que se abren encima
                                   * de las demás, al poner false se dice a windows que el formulario va a vivir DENTRO de otro control.
                                   * Si no hiciésemos primero esto, luego no podríamos añadirlo más abajo al PanelContenedor.Controls.Add... porque daría error*/
            frm.FormBorderStyle = FormBorderStyle.None; /* quita el borde. Si no se nota quizá es porque al estar dentro del formulario no se ve el borde igualmente*/
            frm.Dock = DockStyle.Fill; // esto hace que el formulario de clientes ocupe todo el espacio restante que deja el menu

            panelContenedor.Controls.Add(frm); // Esto creo que es para que el formulario de clientes se muestre dentro del panelContenedor
            frm.Show(); // y esto llama a que se muestre el forulario de clientes
        }

        /*El flujo completo cuando pulsas "Listado" es:
            Limpia el panel por si había otro formulario antes
            Crea FrmClientes
            Lo configura para que pueda vivir dentro de un panel
            Lo añade al panel
            Lo muestra
        */
    }
}
