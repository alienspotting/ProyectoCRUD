using Microsoft.Data.SqlClient;
using ProyectoCRUD.Data;
using ProyectoCRUD.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCRUD
{
    /// <summary>
    /// Métodos y configuración asociados al formulario ClienteDetalle
    /// </summary> 
    public partial class FrmClienteDetalle : Form
    {

        private string _empresaId;
        private string _grupoId;
        private int _cuentaId;
        private bool _modoEdicion = false;
            /*False = lectura. True = edicion.*/
        private bool _esNuevo; // se utiliza para modificar el comportamiento según sea cliente nuevo o modificación de uno existente

        /*Se recogen resultados de guardar cliente para hacer scroll diréctamente 
         * al cliente guardado al volver a FormClientes*/
        public string EmpresaIdResultado => _empresaId;        
        public string GrupoIdResultado => cmbGrupoid.SelectedValue?.ToString() ?? cmbGrupoid.Text;
        public int CuentaIdResultado { get; private set; } 
                
        ClienteData clienteData = new ClienteData();  // Almacena los datos del cliente
                                                     


        /*CONSTRUCTORES*/
        public FrmClienteDetalle()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Inicializa el formulario para mostrar un cliente existente.
        /// </summary>
        public FrmClienteDetalle(string empresaId, string grupoId, int cuentaId)
        {
            InitializeComponent();

            _empresaId = empresaId;
            _grupoId = grupoId;
            _cuentaId = cuentaId;
        }

        /// <summary>
        /// Inicializa formulario vacío con empresa rellenada automáticamente con la empresa buscada previamente en el formulario general
        /// </summary>
        public FrmClienteDetalle(string empresaId)
        {
            InitializeComponent();
            _empresaId = empresaId; 
            _esNuevo = true;
        }

        /// <summary>
        /// Carga los datos del cliente en el formulario DetalleCliente. 
        /// </summary>  
        private void LoadDatos() 
        {
            try
            {
                DataTable dt = clienteData.ObtenerClienteDetalle(
                    _empresaId,
                    _grupoId,
                    _cuentaId
                );

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el cliente");
                    this.Close();
                    return;
                }

                DataRow row = dt.Rows[0];
                
                txtEmpresaid.Text = row["empresaid"].ToString();
                CargarGrupos();
                cmbGrupoid.Text = row["grupoid"].ToString();
                txtCuentaid.Text = row["cuentaid"].ToString();
                txtNombre.Text = row["nombre"].ToString();
                txtNombreComercial.Text = row["comercial"].ToString();
                txtDireccion.Text = row["direccion"].ToString();
                txtDireccion2.Text = row["direccion1"].ToString();
                txtCP.Text = row["cpid"].ToString();
                txtPoblacion.Text = row["poblacion"].ToString();
                txtNif.Text = row["nifid"].ToString();
                txtIdentificacionFiscal.Text = row["nifpaisid"].ToString();
                txtTelefono.Text = row["telefono"].ToString();
                txtObservaciones.Text = row["observaciones"].ToString();
                chkOperativo.Checked = Convert.ToBoolean(row["operativo"]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Inicializa DetalleCliente en modo lectura o edición según el estado de _esNuevo.
        /// </summary>  

        private void FrmClienteDetalle_Load(object sender, EventArgs e)
        {
            if (_esNuevo)
            {
                cmbGrupoid.DropDownStyle = ComboBoxStyle.DropDownList;
                

                if (!string.IsNullOrWhiteSpace(_empresaId))
                    CargarGrupos();  // Si empresa NO ES NULL, entonces carga los grupos.
                SetModoEdicion(true); 
                LimpiarFormulario(); // Crea un formulario vacío a excepción de empresa en caso de que se haya introducido
            }
            else
            {
                cmbGrupoid.DropDownStyle = ComboBoxStyle.DropDown;
                SetModoEdicion(false);
                LoadDatos(); 
            }
        }

        /// <summary>
        /// Obtiene los grupos de la empresa introducida y los carga en el ComboBox cmbGrupoid.
        /// </summary>     

        private void CargarGrupos()
        {
            DataTable dt = clienteData.ObtenerGruposClientes(_empresaId); 
            cmbGrupoid.DataSource = dt; 
            cmbGrupoid.DisplayMember = "grupoid";
            cmbGrupoid.ValueMember = "grupoid";
        }



        
        /// <summary>
        /// LIMPIA EL FORMULARIO DETAllE PARA NUEVO CLIENTE
        /// </summary> 
        /// 
        private void LimpiarFormulario()
        {
            txtEmpresaid.Text = _empresaId;
            if (cmbGrupoid.DataSource != null)  
                cmbGrupoid.SelectedIndex = 0; 
            txtCuentaid.Text = "";
            txtNombre.Text = "";
            txtNombreComercial.Text = "";
            txtDireccion.Text = "";
            txtDireccion2.Text = "";
            txtCP.Text = "";
            txtPoblacion.Text = "";
            txtNif.Text = "";
            txtIdentificacionFiscal.Text = "";
            txtTelefono.Text = "";
            txtObservaciones.Text = "";
        }


        /*ACTIVA EL MODO EDITAR*/

        private void btnEditar_Click(object sender, EventArgs e)
        {
            
            if (!_modoEdicion)
            {
                SetModoEdicion(true);
            }
            else
            {
                GuardarCliente();
                SetModoEdicion(false);

            }

        }

        /// <summary>
        ///  Alterna entre modo lectura y modo edición.
        /// </summary> 

        private void SetModoEdicion(bool edicion) 
        {
            txtEmpresaid.ReadOnly = !_esNuevo; 
            txtCuentaid.ReadOnly = true; 
            txtNombre.ReadOnly = !edicion;
            txtNombreComercial.ReadOnly = !edicion;
            txtDireccion.ReadOnly = !edicion;
            txtDireccion2.ReadOnly = !edicion;
            txtCP.ReadOnly = !edicion;
            txtPoblacion.ReadOnly = !edicion;
            txtNif.ReadOnly = !edicion;
            txtIdentificacionFiscal.ReadOnly = !edicion;
            txtTelefono.ReadOnly = !edicion;
            txtObservaciones.ReadOnly = !edicion;
            chkOperativo.Enabled = edicion;
            _modoEdicion = edicion;

            btnEditar.Text = edicion ? "Guardar" : "Editar"; // modifica el texto del botón


            if (_esNuevo)
            {
                cmbGrupoid.Enabled = true; 
                btnEliminar.Visible = false;
            }
            else
            {
                cmbGrupoid.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbGrupoid.Enabled = false; // en modo edicion deshabilitado. Son claves primarias así que no permitimos modificar
            }

        }


        /// <summary>
        /// Revisa que los campos obligatorios estén completos y llama a la función GuardarCliente de ClienteData para guardar los datos en la base de datos.
        /// </summary> 

        private void GuardarCliente()
        {
            ClienteUpdateData clienteUpdateData = new ClienteUpdateData(); 

            if (!clienteData.ExisteEmpresa(_empresaId)) 
            {
                MessageBox.Show("La empresa no existe");
                return; 
            }

            int cuentaId = 0; 

            if (!_esNuevo) 
            {
                if (!int.TryParse(txtCuentaid.Text, out cuentaId))
                {
                    MessageBox.Show("El ID de cuenta debe ser un número");
                    return;
                }
            }

            int modo = _esNuevo ? 2 : 1; 
            string grupoId = cmbGrupoid.SelectedValue?.ToString() ?? cmbGrupoid.Text;
            
            int nuevoCuentaId = clienteUpdateData.GuardarCliente( 
                modo,
                _empresaId,
                grupoId,        
                cuentaId,
                txtNombre.Text,
                txtNombreComercial.Text,
                txtDireccion.Text,
                txtDireccion2.Text,
                txtCP.Text,
                txtPoblacion.Text,
                txtNif.Text,
                txtIdentificacionFiscal.Text,
                txtTelefono.Text,
                txtObservaciones.Text,
                chkOperativo.Checked
            ); 

            if (_esNuevo)
                CuentaIdResultado = nuevoCuentaId; 


            MessageBox.Show("Cliente guardado correctamente"); 
            this.DialogResult = DialogResult.OK; 
            this.Close(); 
        }


        /*-----------------------CARGA LOS GRUPOS AL SALIR DEL TEXTBOX DE EMPRESA-----------------------------*/

        private void txtEmpresaid_Leave(object sender, EventArgs e)
        {
            if (!_esNuevo) return; 
            if (string.IsNullOrWhiteSpace(txtEmpresaid.Text))
            {
                cmbGrupoid.DataSource = null; 
                return;
            }
            _empresaId = txtEmpresaid.Text.Trim(); 
            if (!clienteData.ExisteEmpresa(_empresaId))
            {
                MessageBox.Show("La empresa no existe");
                txtEmpresaid.Focus(); 
                return; 
            }
            CargarGrupos();

        }

        /*BOTÓN ELIMINAR*/
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_esNuevo) return; 
            DialogResult confirmacion = MessageBox.Show(
                "Estás seguro de que quieres eliminar este cliente?", "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (confirmacion == DialogResult.Yes)
            {
                ClienteUpdateData clienteUpdateData = new ClienteUpdateData();
                clienteUpdateData.EliminarCliente(_empresaId, _grupoId, _cuentaId);
                MessageBox.Show("Cliente eliminado correctamente");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
