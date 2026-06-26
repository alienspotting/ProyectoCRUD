
using System.Data;
using ProyectoCRUD.Data;

namespace ProyectoCRUD
{
    public partial class FrmClientes : Form
    {
        ClienteData clienteData = new ClienteData();

        /* ---------------------------CONSTRUCTORES ----------------------------------- */

        public FrmClientes()
        {
            InitializeComponent();
        }

        /* --------------------------- MÉTODOS Y EVENTOS ----------------------------------- */

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            

            cmbOperativo.SelectedIndex = 0; 
            ConfigurarGrid(); 
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros()) 
                return; 
            CargarClientes(); 


        }

        /// <summary>
        /// Obtiene información de los clientes llamando a ClienteData y la muestra en el DataGridView
        /// </summary>       


        private void CargarClientes()
        {
            try
            {
                string nombre = txtNombre.Text?.Trim(); 

                DataTable dt = clienteData.ObtenerClientes(
                    txtEmpresaId.Text,
                    ObtenerOperativo(),
                    nombre
                ); 

                dgvClientes.DataSource = dt; 

                if (dt.Rows.Count == 0)
                {
                    dgvClientes.DataSource = null;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Almacena info del cliente sobre el que se ha hecho doble click para cargar el formulario DetalleCliente
        /// </summary>       

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex < 0) return; 

            DataGridViewRow row = dgvClientes.Rows[e.RowIndex]; 

            
            string empresaId = row.Cells["empresaid"].Value?.ToString();  
            string grupoId = row.Cells["grupoid"].Value?.ToString();
            int cuentaId = Convert.ToInt32(row.Cells["cuentaid"].Value ?? 0); 

            FrmClienteDetalle frm = new FrmClienteDetalle(empresaId, grupoId, cuentaId); 
            if (frm.ShowDialog() == DialogResult.OK) 
            {
                CargarClientes();
                SeleccionarCliente(empresaId, grupoId, cuentaId); 
            }
        }



        /// <summary>
        /// Recoge la info del cliente guardado en DetalleCliente y hace scroll en el DataGridView para mostrarlo
        /// cuando el usuario vuelve a FrmClientes tras guardar un cliente nuevo o editar uno existente
        /// </summary>       
        

        private void SeleccionarCliente(string empresaId, string grupoId, int cuentaId)
        {
            foreach (DataGridViewRow row in dgvClientes.Rows) 
            {
                if (row.Cells["empresaid"].Value.ToString() == empresaId &&
                    row.Cells["grupoid"].Value.ToString() == grupoId &&
                    Convert.ToInt32(row.Cells["cuentaid"].Value) == cuentaId)
                {
                    row.Selected = true; 
                    dgvClientes.FirstDisplayedScrollingRowIndex = row.Index; 
                    dgvClientes.CurrentCell = row.Cells[0]; 
                    break; 
                }
            }
        }

        /// <summary>
        /// Valida que se haya introducido los filtros de búsqueda necesarios (empresaid)
        /// </summary> 

        private bool ValidarFiltros() 
        {
            if (string.IsNullOrWhiteSpace(txtEmpresaId.Text))
            {
                MessageBox.Show("Introduce la empresa"); // revisa que hayan introducido una empresa
                return false;
            }

            return true;
        }

        /// <summary>
        /// Traduce el valor seleccionado en el ComboBox a un valor válido para el procedimiento almacenado
        /// </summary> 
        /// 
        private byte ObtenerOperativo() 
        {
            return cmbOperativo.SelectedItem.ToString() switch
            {
                "Operativos" => 1,
                "No operativos" => 2,
                _ => 3
            };
                        
        }

        /* --------------------------- CONFIGURACION DEL GRID ----------------------------------- */
        /// <summary>
        /// Configuración visual del grid
        /// </summary> 


        private void ConfigurarGrid()
        {
            dgvClientes.AllowUserToAddRows = false; 
            dgvClientes.AllowUserToDeleteRows = false; 

            dgvClientes.AutoGenerateColumns = false; 
            dgvClientes.ReadOnly = true; 
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; 

            btnEliminar.Visible = false; 

            foreach (DataGridViewColumn col in dgvClientes.Columns) 
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; 

                if (col.Name == "correo") 
                    col.Width = 200;
                else if (col.Name == "nombre") 
                    col.Width = 180;
                else if (col.Name == "direccion") 
                    col.Width = 250;
                else
                    col.Width = 120; 
            }

            dgvClientes.ScrollBars = ScrollBars.Both; 
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyCode == Keys.Enter) 
            {
                CargarClientes(); // cuando se pulsa enter se cargan clientes
                e.SuppressKeyPress = true; 
                                
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e) 
        {
            FrmClienteDetalle frm = new FrmClienteDetalle(txtEmpresaId.Text); 
            if (frm.ShowDialog() == DialogResult.OK) 
            {
                CargarClientes(); 
                SeleccionarCliente(frm.EmpresaIdResultado, frm.GrupoIdResultado, frm.CuentaIdResultado); // Para hacer scroll al nuevo cliente creado
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {

            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecciona un cliente primero");
                return;
            }

            DataGridViewRow row = dgvClientes.SelectedRows[0];
            string empresaId = row.Cells["empresaid"].Value?.ToString();
            string grupoId = row.Cells["grupoid"].Value?.ToString();
            int cuentaId = Convert.ToInt32(row.Cells["cuentaid"].Value ?? 0);

            DialogResult confirmacion = MessageBox.Show(
                $"¿Estás seguro de que quieres eliminar el cliente {row.Cells["nombre"].Value}?",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacion == DialogResult.Yes)
            {
                ClienteUpdateData clienteUpdateData = new ClienteUpdateData();
                clienteUpdateData.EliminarCliente(empresaId, grupoId, cuentaId);
                MessageBox.Show("Cliente eliminado correctamente");
                CargarClientes();
            }

        }

        private void dgvClientes_SelectionChanged(object sender, EventArgs e)
        {
            btnEliminar.Visible = dgvClientes.SelectedRows.Count > 0; // El botón eliminar solo se muestra si hay algo seleccionado
        }
    }
}