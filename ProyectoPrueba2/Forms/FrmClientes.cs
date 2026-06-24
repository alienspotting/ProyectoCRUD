
using System.Data;
using ProyectoPrueba2.Data;

namespace ProyectoPrueba2
{
    public partial class FrmClientes : Form
    {
        ClienteData clienteData = new ClienteData();

        /* ---------------------------CONSTRUCTORES ----------------------------------- */

        public FrmClientes()
        {
            InitializeComponent();
        }

        /* ---------------------------EVENTOS ----------------------------------- */

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            // Esto es lo que pasa nada más se carga el formulario. 

            cmbOperativo.SelectedIndex = 0; /* Elige "el primer elemento de la lista visual del comboBox (en este caso Operativos)"
                                             * Aquí no hace falta comprobar si tiene Items, proque los items están añadidos diréctamente
                                             * en el VisualStudio ("Operativos", "No operativos" y "Todos") así que sabemos que tiene items desde el principio*/
            ConfigurarGrid(); // llama a método para configurarGrid de manera personalizada
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            if (!ValidarFiltros()) // Primero revisa que los filtros necesarios para hacer la consulta están rellenos
                return; // Si no lo están, sale del método
            CargarClientes(); // Si los filtros están bien, carga los clientes


        }

        private void CargarClientes()
        {
            try
            {
                string nombre = txtNombre.Text?.Trim(); // Si text no es null, hace trim. Si es null, no lanza error y devuelve null. Sin esto, al ser null fallaría

                DataTable dt = clienteData.ObtenerClientes(
                    txtEmpresaId.Text,
                    ObtenerOperativo(),
                    nombre
                ); /* aquí estamos llamando al método para cargar los clientes generales. 
                    * nombre puede ser null porque es opcional filtrar por nombre o no. 
                    * Como el método ObtenerClientes devuelve un DataTable, asocialmos a dt
                    */

                dgvClientes.DataSource = dt; /*Determina que la fuente de la info que se mete en dgvClientes es lo que hay en dt*/

                if (dt.Rows.Count == 0)
                {
                    dgvClientes.DataSource = null;
                } // Si no se devuelve nada y por tanto no hay rows, no hay datos que cargar en dgvClientes
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e) /*Esto pasa cuando hacemos doble click en una celda 
                                                                                              * (para cargar el cliente correspondiente a esa fila)*/
        {
            if (e.RowIndex < 0) return; /* Si el RowIndex es menor que 0 sale. 
                                        * El header tiene RowIndex -1 por lo que evita que si el usuario hace doble click 
                                        * sobre el header el programa explote */

            DataGridViewRow row = dgvClientes.Rows[e.RowIndex]; /*Esto guarda toda la info de la row que se ha pulsado (que es e ) */

            /*Aquí se asocian los datos de ese row a variables, de modo que luego las pasaremos al constructor.
             *El Cells["empresaid"] imagino qué indica qué columna dentro de ese row es de donde se saca la info*/
            string empresaId = row.Cells["empresaid"].Value?.ToString();  // Si no es null, se pasa a toString
            string grupoId = row.Cells["grupoid"].Value?.ToString();
            int cuentaId = Convert.ToInt32(row.Cells["cuentaid"].Value ?? 0); // Convert.ToInt32 no acepta null así que hay que sustituirlo antes de pasarlo 

            FrmClienteDetalle frm = new FrmClienteDetalle(empresaId, grupoId, cuentaId); /*Creamos un formulario detalel cliente al constructor de cliente existente*/
            if (frm.ShowDialog() == DialogResult.OK) /*Abre el formulario y espera a recibir confirmación de FrmClienteDetalle
                                                      * la confirmación solo se envia cuando el cliente guarda un cliente modificado
                                                      * or lo que solo entra en ese caso. De esta manera se selecciona el cliente que acabamos de guardar
                                                      * Show() abre el formulario y continua ejecutando sin esperar. ShowDialog abre formulario, espera y devuelve resultado
                                                      * Si el cliente guarda, devuelve OK. si el cliente cierra, o edita sin guardar y cierra - se devuelve cancel*/
            {
                CargarClientes();
                SeleccionarCliente(empresaId, grupoId, cuentaId); /*Lleva directamente al cliente que se ha creado*/
            }
        }

        /* --------------------------- METODO QUE HACE SCROLL CUANDO GUARDAS AL CLIENTE ----------------------------------- */
        private void SeleccionarCliente(string empresaId, string grupoId, int cuentaId)
        {
            foreach (DataGridViewRow row in dgvClientes.Rows) /*Repasa toda la DataGridView hasta que la empresa, 
                                                               * grupo y cuenta coinciden.
                                                               *DataGridViewRow es el TIPO.
                                                               *row es el nombre de la variable 
                                                               * dgvClientes es el control ENTERO (columnas, cabecera etc)
                                                               * por eso se pone .Rows -> de esa manera colecciona las filas con datos*/
            {
                if (row.Cells["empresaid"].Value.ToString() == empresaId &&
                    row.Cells["grupoid"].Value.ToString() == grupoId &&
                    Convert.ToInt32(row.Cells["cuentaid"].Value) == cuentaId)
                {
                    row.Selected = true; // Marca la línea como seleccionada (pintada en azul). Si no, haría scroll pero no se resaltaría
                    dgvClientes.FirstDisplayedScrollingRowIndex = row.Index; // Esto hace el scroll hasta la fila correcta (FirstDisplayedScrollingRowIndex indica cuál es la primera fila visible en pantalla)
                    dgvClientes.CurrentCell = row.Cells[0]; // Establece el foco en la primera celda de la  linea 
                    break; // Sale del for each
                }
            }
        }

        /* ---------------------------MÉTODOS AUXILIARES ----------------------------------- */

        private bool ValidarFiltros() // Comrpueba la validez de los datos
        {
            if (string.IsNullOrWhiteSpace(txtEmpresaId.Text))
            {
                MessageBox.Show("Introduce la empresa"); // revisa que hayan introducido una empresa
                return false;
            }

            return true;
        }

        private byte ObtenerOperativo() // Traduce Operativos / No operativos al número correcto para hacer la consulta
        {
            return cmbOperativo.SelectedItem.ToString() switch
            {
                "Operativos" => 1,
                "No operativos" => 2,
                _ => 3
            };

            /*ESTO EQUIVALE A (es el modo abreviado):
            private byte ObtenerOperativo()
            {
                switch (cmbOperativo.SelectedItem.ToString())
                {
                    case "Operativos":
                        return 1;
                    case "No operativos":
                        return 2;
                    default:
                        return 3;
                }
            }
            DIFERENCIAS CON SWITCH NORMAL:
                - No hay case ni break
                - => sustituye a los dos puntos
                - _ equivale a default
                -Al ser una expresión devuelve el return fuera
             */
        }

        /* --------------------------- CONFIGURACION DEL GRID ----------------------------------- */


        private void ConfigurarGrid()
        {
            dgvClientes.AllowUserToAddRows = false; //Esto impide al usuario que añada filas
            dgvClientes.AllowUserToDeleteRows = false; //Esto impide al usuario que elimine filas

            dgvClientes.AutoGenerateColumns = false; // Esto impide que se autogeneren las columnas con lo
            dgvClientes.ReadOnly = true; // Esto hace que todo el grid sea de solo lectura de manera que el usuario no pueda escribir dentro
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Imagino que esto hace que si clica se seleccione la fila entera

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Esto cancela que el tamaño de ls columnas se genere automáticamente supongo? 

            btnEliminar.Visible = false; //oculta el botón eliminar para que solo salga cuando hay algo seleccionado

            foreach (DataGridViewColumn col in dgvClientes.Columns) // esto recorre todas las columnas del grid individualmente
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None; // aunque parece redudndante, no tiene por qué sesr así ya que asegura, columna por columna, que ninguna tenga autosize mode que sobreescribiese lo anterior

                if (col.Name == "correo") // si la columna se llama correo, se le asigna un ancho 200
                    col.Width = 200;
                else if (col.Name == "nombre") // si la columna se llama nombre, 180
                    col.Width = 180;
                else if (col.Name == "direccion") // se le da más para direccion
                    col.Width = 250;
                else
                    col.Width = 120; // lo demás 120
            }

            dgvClientes.ScrollBars = ScrollBars.Both; // permite scroll vertical y horizontal
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e) //este es el evento de cuando se pulsa CUALQUIER tecla, luego dentro se especifican cuales
        {
            if (e.KeyCode == Keys.Enter) /* keycode es la tecla concreta que se presionó Otras opciones:
                                            e.KeyCode == Keys.Escape  // tecla ESC
                                            e.KeyCode == Keys.Delete  // tecla Supr
                                            e.KeyCode == Keys.F5
                                         */
            {
                CargarClientes(); // cuando se pulsa enter se cargan clientes
                e.SuppressKeyPress = true; /* Evita que Enter haga su comportamiento por defecto despues de ejecutar el código. 
                                            * Si no, después de cargar el cliente Enter odría causar pitido o mover el foco a otro control.
                                            * Le dice a windows "ya he manejado esta tecla, no hagas m´s con ella"*/

                /* El mismo KeyDown se puede utilizar para determinar el comportamiento de varias teclas diferentes, ejemplo: 
                
                    private void txtNombre_KeyDown(object sender, KeyEventArgs e)
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            CargarClientes();
                            e.SuppressKeyPress = true;
                        }
                        else if (e.KeyCode == Keys.Escape)
                        {
                            txtNombre.Clear(); // por ejemplo, limpiar el campo con ESC
                        }
                    }   
                 */
            }
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e) // esto pasa cuando se pulsa en neuvo cliente
        {
            FrmClienteDetalle frm = new FrmClienteDetalle(txtEmpresaId.Text); // pasa lo que haya, aunque esté vacío
            if (frm.ShowDialog() == DialogResult.OK) // muestra la ventana y espera confirmacióin de que ha ido ok
            {
                CargarClientes(); // si se ha guardado el cliente (que es lo que envia el DialogResult.OK) entonces se cargan de nuevo los clientes
                SeleccionarCliente(frm.EmpresaIdResultado, frm.GrupoIdResultado, frm.CuentaIdResultado); // y se llama a la selección, habiendo recogido la info del formulario de las tres claves primarias para poder encontrar el nuevo cliente
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
            btnEliminar.Visible = dgvClientes.SelectedRows.Count > 0; // Esto es para que el botón eliminar solo se muestre si hay algo seleccionado
        }
    }
}