using Microsoft.Data.SqlClient;
using ProyectoPrueba2.Data;
using ProyectoPrueba2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrueba2
{
    public partial class FrmClienteDetalle : Form
    {

        private string _empresaId;
        private string _grupoId;
        private int _cuentaId;
        private bool _modoEdicion = false;
        /*False - modo lectura. True - modo edicion. Se usa en SetModoEdicion.
          Se le llama desde FrmClienteDetalle_Load. Si es nuevo, SetModoEdicion (true). Si es existe,te SetModoEdifcion(false)
          También se le llama desde btnEditar_Click cuando el usuario pulsa el botón*/
        private bool _esNuevo; // para el formulario en modo edición. Distingue el modo de si es cliente nuevo o es modificación de cliente

        /*Estas tres siguientes es para poder acceder a los detalles del nuevo cliente para luego ahcer scroll a ese cliente<.*/
        public string EmpresaIdResultado => _empresaId;
        /* Es una forma abroviada de una propiedad que solo devuelve un valor, equivale a: 
         *  public string EmpresaIdResultado 
                { 
                    get { return _empresaId; } 
                }
        */
        public string GrupoIdResultado => cmbGrupoid.SelectedValue?.ToString() ?? cmbGrupoid.Text; //  lo mismo pero devuelve el valor del combo
        public int CuentaIdResultado { get; private set; } // Esta es propiedad de get publico (cualaquiera puede leer) y set privado (solo puede modificarla la propia clase) 

        /*----*/
        ClienteData clienteData = new ClienteData(); //Obtiene datos de cliente detalle
                                                     //


        /*CONSTRUCTOR SIN PARÁMETROS*/
        public FrmClienteDetalle()
        {
            InitializeComponent();
        }
        /*CONSTRUCTOR - crea el formulario de un cliente ya existente, cuando se hace doble click
            Se le llama desde dgvClientes_CellDoubleClick*/
        public FrmClienteDetalle(string empresaId, string grupoId, int cuentaId)
        {
            InitializeComponent();

            _empresaId = empresaId;
            _grupoId = grupoId;
            _cuentaId = cuentaId;
        }

        // Constructor para NUEVO cliente - se le llama desde btnNuevoCliente_Click
        public FrmClienteDetalle(string empresaId)
        {
            InitializeComponent();
            _empresaId = empresaId; // tiene empresaId por si el usuario ya la ha introducido previamente
            _esNuevo = true;
        }

        private void LoadDatos() // Esto reyena el formulario con los datos que vienen del SP
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
                /* En este caso, como estamos cogiendo un cliente en concreto con las claves primarias,
                 * sabemos que no va a haber más filas, es por ello que se coge la 0 - que es la primera y unica fila
                 que devuelve la DataTable*/

                /*A partir de aquí, coge los nombres de la tabla que devuelve el SP y los asocia al textBox correspondiente*/
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


        /*LOAD DEL FORMULARIO - Esto se dispara justo antes de que el formulario se muestre al usuario
            es decir cuando se ejecuta frm.ShowDialog() en FrmClientes.
        ORDEN: 
            1. Se ejecuta new FrmClienteDetalle(...) → constructor, se inicializan las variables
            2. Se ejecuta frm.ShowDialog() → Windows prepara la ventana
            3. Se dispara FrmClienteDetalle_Load → aquí decides qué cargar
            4. El formulario se muestra al usuario
         */
        private void FrmClienteDetalle_Load(object sender, EventArgs e)
        {
            if (_esNuevo)
            {
                cmbGrupoid.DropDownStyle = ComboBoxStyle.DropDownList;
                /*El comboBox tiene tres opciones: 
                   DropDownList - el usuario solo puede seleccionar de la lista, no puede escribir libremente.
                   DropDown - el usuario puede seleccionar de la lista O escribir texto libre.
                   Simple - la lista está siempre desplegada y visible, sin botón para abrir/cerrar*/

                if (!string.IsNullOrWhiteSpace(_empresaId))
                    CargarGrupos();  // Si empresa NO ES NULL, entonces carga los grupos.
                SetModoEdicion(true); // Al ser nuevo, diréctament epermite el modo edición
                LimpiarFormulario(); // Crea un formulario vacío a excepción de empresa y grupo en caso de que se haya introducido
            }
            else
            {
                cmbGrupoid.DropDownStyle = ComboBoxStyle.DropDown;
                SetModoEdicion(false);
                LoadDatos(); // ← solo se llama aquí, cuando es edición
            }
        }

        private void CargarGrupos()
        {
            DataTable dt = clienteData.ObtenerGruposClientes(_empresaId); // Llama al método que ejecuta consulta SQL y devuelve datatable
            cmbGrupoid.DataSource = dt; // indica al como que la fuente de los datos es dt
            /*Un combo tiene dos valores por item, Displaymember y ValueMember.
                DisplayMember es lo que el usuario VE en pantalla
                ValueMember es el valor interno que se recupera con cmbGrupoid.SelectedValue
                Aquí como solo tenemos una columna, ambos se llaman igual.
                Pero si hubiese dos columnas (ej grupoid y nombre_grupo), podríamos mostrar al usuario el nombre e internamente usar el id.*/
            cmbGrupoid.DisplayMember = "grupoid";
            cmbGrupoid.ValueMember = "grupoid";
        }

        /*CARGA EL FORMULARIO VACIO PARA NUEVO CLIENTE*/
        private void LimpiarFormulario()
        {
            txtEmpresaid.Text = _empresaId;
            if (cmbGrupoid.DataSource != null)  // Comprueba que haya datos en cmbGrupoid. Si lo hay:
                cmbGrupoid.SelectedIndex = 0; // En este caso selecciona el primer item de la lista - si no lo hay no hace nada
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
            /*El botón tiene dos comportamientos diferentes en función del estado de _modoEdicion
                -Si es false -> en el botón pone "editar
                -Si es true -> el botón dice guardar y por tanto guarda el cliente y luego vuelve al modo lectura*/
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

        /*PARA QUE LOS DATOS CARGUEN EN MODO READ ONLY*/

        private void SetModoEdicion(bool edicion) // Si edicion es true , read only es false etc.
        {
            txtEmpresaid.ReadOnly = !_esNuevo; // Si es nuevo, NO es readOnly.
            txtCuentaid.ReadOnly = true; // De momento lo dejamos así porque ahora mismo cuentaid no se pasa al sql, el procedimiento lo calcula solo
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
                cmbGrupoid.Enabled = true; // en nuevo siempre habilitado 
                btnEliminar.Visible = false;
            }
            else
            {
                cmbGrupoid.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbGrupoid.Enabled = false; // en modo edicion deshabilitado. Son claves primarias así que no permitimos que cambien
            }

        }


        /*GUARDAR CLIENTE - LLAMA PROCEDIMIENTO DE MODIFICACION CLIENTES*/

        private void GuardarCliente()
        {
            ClienteUpdateData clienteUpdateData = new ClienteUpdateData(); // crea un objeto clienteUpdateData que es donde está el procedimiento de update

            if (!clienteData.ExisteEmpresa(_empresaId)) // este método es el que comprueba si la empresa existe. En caso de que no, saca el mensaje y sale del método
            {
                MessageBox.Show("La empresa no existe");
                return; // Existe también en java. en métodos void, se usa return sin valor para salir del método
            }

            int cuentaId = 0; // para modo nuevo el SP lo calcula, el valor no importa, pero de momento lo dejo así porque en casa lo modificaré 

            if (!_esNuevo) // imagino que esto lo puedo borrar ahora porque ya estamos asociando una cuentaId y ya no deja añadir número a cuenta
            {
                if (!int.TryParse(txtCuentaid.Text, out cuentaId))
                {
                    MessageBox.Show("El ID de cuenta debe ser un número");
                    return;
                }
            }

            int modo = _esNuevo ? 2 : 1; // Si es nuevo, pasa 2 (que es modo insert en el SP), si es existente, pasa 1 (modo update)
            string grupoId = cmbGrupoid.SelectedValue?.ToString() ?? cmbGrupoid.Text;
            /*Ejemplo simplificado de esto: 
                string nombre = persona?.Nombre ?? "Sin nombre";
                    -Si persona no es null → devuelve persona.Nombre
                    -Si persona es null → devuelve "Sin nombre"
             */


            int nuevoCuentaId = clienteUpdateData.GuardarCliente( // guardarCliente devuelve la nueva cuentaId
                modo,
                _empresaId,
                grupoId,        // ← usamos la variable
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
            ); // Todo esto son los parámetros del procedimiento de GuardarCliente de la clase clienteUpdateData

            if (_esNuevo)
                CuentaIdResultado = nuevoCuentaId; // nuevoCuentaId es privada, aquí asociamos a la pública


            MessageBox.Show("Cliente guardado correctamente"); // Indica que se ha guardado
            this.DialogResult = DialogResult.OK; /*Esto le dice al formulario padre (FrmClientes) que el 
                                                  *formulario se cerró correctamente, porque en Fromclientes tenemos: 
                                                  *if (frm.ShowDialog() == DialogResult.OK)
                                                        {
                                                            CargarClientes(); // solo recarga si guardó correctamente
                                                        }
                                                  * Y si no pusieramos esto, frmClientes no sabría si ha ido bien o no y no recargaría la lista de clientes
                                                  */

            this.Close(); // esto es porque cierra la pantalla automáticamente
        }


        /*-----------------------CARGA LOS GRUPOS AL SALIR DEL TEXTBOX DE EMPRESA-----------------------------*/

        private void txtEmpresaid_Leave(object sender, EventArgs e)
        {
            if (!_esNuevo) return; // Si el cliente no es nuevo, no procede con el método
            if (string.IsNullOrWhiteSpace(txtEmpresaid.Text))
            {
                cmbGrupoid.DataSource = null; // si no hay empresa seleccionada, el grupoid es nulo. El dataSource entiendo que es de donde se sacan los datos, al decir null es porque queremos que sea nulo
                return;
            }
            _empresaId = txtEmpresaid.Text.Trim(); // quita espacios en blanco al id de empresa por si acaso
            if (!clienteData.ExisteEmpresa(_empresaId))
            {
                MessageBox.Show("La empresa no existe");
                txtEmpresaid.Focus(); // devuelve al cursor de vuelta al campo de empresaId
                return; //  sale del método devolviendote a ese campo, y una vez escribes algo nuevo y sales del campo vuelve a entrar al método
            }
            CargarGrupos(); // si la empresa existe y se ha insertado la info, entonces ya sí carga los grupos

        }

        /*BOTÓN ELIMINAR*/
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (_esNuevo) return; // Si estamos en cliente nuevo no continua porque no tiene sentido eliminar un cliente que no existe
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
