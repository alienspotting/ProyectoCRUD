namespace ProyectoPrueba2
{
    partial class FrmClientes
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Empresa = new Label();
            label2 = new Label();
            txtEmpresaId = new TextBox();
            cmbOperativo = new ComboBox();
            btnConsultar = new Button();
            dgvClientes = new DataGridView();
            nombre = new DataGridViewTextBoxColumn();
            empresaid = new DataGridViewTextBoxColumn();
            grupoid = new DataGridViewTextBoxColumn();
            cuentaid = new DataGridViewTextBoxColumn();
            comercial = new DataGridViewTextBoxColumn();
            direccion = new DataGridViewTextBoxColumn();
            direccion1 = new DataGridViewTextBoxColumn();
            poblacion = new DataGridViewTextBoxColumn();
            cpid = new DataGridViewTextBoxColumn();
            nifid = new DataGridViewTextBoxColumn();
            nifpaisid = new DataGridViewTextBoxColumn();
            telefono = new DataGridViewTextBoxColumn();
            fax = new DataGridViewTextBoxColumn();
            movil = new DataGridViewTextBoxColumn();
            correo = new DataGridViewTextBoxColumn();
            factura_electronica = new DataGridViewCheckBoxColumn();
            txtNombre = new TextBox();
            lblNombre = new Label();
            btnNuevoCliente = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // Empresa
            // 
            Empresa.AutoSize = true;
            Empresa.Location = new Point(13, 28);
            Empresa.Name = "Empresa";
            Empresa.Size = new Size(66, 20);
            Empresa.TabIndex = 0;
            Empresa.Text = "Empresa";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(198, 29);
            label2.Name = "label2";
            label2.Size = new Size(75, 20);
            label2.TabIndex = 1;
            label2.Text = "Operativo";
            // 
            // txtEmpresaId
            // 
            txtEmpresaId.Location = new Point(77, 25);
            txtEmpresaId.Margin = new Padding(3, 4, 3, 4);
            txtEmpresaId.Name = "txtEmpresaId";
            txtEmpresaId.Size = new Size(114, 27);
            txtEmpresaId.TabIndex = 3;
            // 
            // cmbOperativo
            // 
            cmbOperativo.FormattingEnabled = true;
            cmbOperativo.Items.AddRange(new object[] { "Operativos", "No operativos", "Todos" });
            cmbOperativo.Location = new Point(270, 25);
            cmbOperativo.Margin = new Padding(3, 4, 3, 4);
            cmbOperativo.Name = "cmbOperativo";
            cmbOperativo.Size = new Size(138, 28);
            cmbOperativo.TabIndex = 4;
            // 
            // btnConsultar
            // 
            btnConsultar.Location = new Point(661, 23);
            btnConsultar.Margin = new Padding(3, 4, 3, 4);
            btnConsultar.Name = "btnConsultar";
            btnConsultar.Size = new Size(238, 31);
            btnConsultar.TabIndex = 6;
            btnConsultar.Text = "Consultar";
            btnConsultar.UseVisualStyleBackColor = true;
            btnConsultar.Click += btnConsultar_Click;
            // 
            // dgvClientes
            // 
            dgvClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { nombre, empresaid, grupoid, cuentaid, comercial, direccion, direccion1, poblacion, cpid, nifid, nifpaisid, telefono, fax, movil, correo, factura_electronica });
            dgvClientes.Location = new Point(14, 117);
            dgvClientes.Margin = new Padding(3, 4, 3, 4);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(887, 467);
            dgvClientes.TabIndex = 7;
            dgvClientes.CellDoubleClick += dgvClientes_CellDoubleClick;
            dgvClientes.SelectionChanged += dgvClientes_SelectionChanged;
            // 
            // nombre
            // 
            nombre.DataPropertyName = "nombre";
            nombre.HeaderText = "Nombre";
            nombre.MinimumWidth = 6;
            nombre.Name = "nombre";
            nombre.ReadOnly = true;
            nombre.Width = 76;
            // 
            // empresaid
            // 
            empresaid.DataPropertyName = "empresaid";
            empresaid.HeaderText = "Empresa ID";
            empresaid.MinimumWidth = 6;
            empresaid.Name = "empresaid";
            empresaid.ReadOnly = true;
            empresaid.Width = 125;
            // 
            // grupoid
            // 
            grupoid.DataPropertyName = "grupoid";
            grupoid.HeaderText = "Grupo ID";
            grupoid.MinimumWidth = 6;
            grupoid.Name = "grupoid";
            grupoid.ReadOnly = true;
            grupoid.Width = 125;
            // 
            // cuentaid
            // 
            cuentaid.DataPropertyName = "cuentaid";
            cuentaid.HeaderText = "Cuenta ID";
            cuentaid.MinimumWidth = 6;
            cuentaid.Name = "cuentaid";
            cuentaid.ReadOnly = true;
            cuentaid.Width = 125;
            // 
            // comercial
            // 
            comercial.DataPropertyName = "comercial";
            comercial.HeaderText = "Comercial";
            comercial.MinimumWidth = 6;
            comercial.Name = "comercial";
            comercial.ReadOnly = true;
            comercial.Width = 86;
            // 
            // direccion
            // 
            direccion.DataPropertyName = "direccion";
            direccion.HeaderText = "Direccion";
            direccion.MinimumWidth = 6;
            direccion.Name = "direccion";
            direccion.ReadOnly = true;
            direccion.Width = 82;
            // 
            // direccion1
            // 
            direccion1.DataPropertyName = "direccion1";
            direccion1.HeaderText = "Direccion";
            direccion1.MinimumWidth = 6;
            direccion1.Name = "direccion1";
            direccion1.ReadOnly = true;
            direccion1.Width = 82;
            // 
            // poblacion
            // 
            poblacion.DataPropertyName = "poblacion";
            poblacion.HeaderText = "Poblacion";
            poblacion.MinimumWidth = 6;
            poblacion.Name = "poblacion";
            poblacion.ReadOnly = true;
            poblacion.Width = 85;
            // 
            // cpid
            // 
            cpid.DataPropertyName = "cpid";
            cpid.HeaderText = "CP";
            cpid.MinimumWidth = 6;
            cpid.Name = "cpid";
            cpid.ReadOnly = true;
            cpid.Width = 47;
            // 
            // nifid
            // 
            nifid.DataPropertyName = "nifid";
            nifid.HeaderText = "NIF";
            nifid.MinimumWidth = 6;
            nifid.Name = "nifid";
            nifid.ReadOnly = true;
            nifid.Width = 50;
            // 
            // nifpaisid
            // 
            nifpaisid.DataPropertyName = "nifpaisid";
            nifpaisid.HeaderText = "Pais";
            nifpaisid.MinimumWidth = 6;
            nifpaisid.Name = "nifpaisid";
            nifpaisid.ReadOnly = true;
            nifpaisid.Width = 53;
            // 
            // telefono
            // 
            telefono.DataPropertyName = "telefono";
            telefono.HeaderText = "Teléfono";
            telefono.MinimumWidth = 6;
            telefono.Name = "telefono";
            telefono.ReadOnly = true;
            telefono.Width = 77;
            // 
            // fax
            // 
            fax.DataPropertyName = "fax";
            fax.HeaderText = "FAX";
            fax.MinimumWidth = 6;
            fax.Name = "fax";
            fax.ReadOnly = true;
            fax.Width = 52;
            // 
            // movil
            // 
            movil.DataPropertyName = "movil";
            movil.HeaderText = "Móvil";
            movil.MinimumWidth = 6;
            movil.Name = "movil";
            movil.ReadOnly = true;
            movil.Width = 62;
            // 
            // correo
            // 
            correo.DataPropertyName = "correo";
            correo.HeaderText = "Correo";
            correo.MinimumWidth = 6;
            correo.Name = "correo";
            correo.ReadOnly = true;
            correo.Width = 68;
            // 
            // factura_electronica
            // 
            factura_electronica.DataPropertyName = "factura_electronica";
            factura_electronica.HeaderText = "Factura Electrónica";
            factura_electronica.MinimumWidth = 6;
            factura_electronica.Name = "factura_electronica";
            factura_electronica.ReadOnly = true;
            factura_electronica.Width = 102;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(508, 23);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(114, 27);
            txtNombre.TabIndex = 9;
            txtNombre.KeyDown += txtNombre_KeyDown;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(438, 27);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(64, 20);
            lblNombre.TabIndex = 8;
            lblNombre.Text = "Nombre";
            // 
            // btnNuevoCliente
            // 
            btnNuevoCliente.Location = new Point(14, 73);
            btnNuevoCliente.Margin = new Padding(3, 4, 3, 4);
            btnNuevoCliente.Name = "btnNuevoCliente";
            btnNuevoCliente.Size = new Size(115, 31);
            btnNuevoCliente.TabIndex = 10;
            btnNuevoCliente.Text = "Nuevo cliente";
            btnNuevoCliente.UseVisualStyleBackColor = true;
            btnNuevoCliente.Click += btnNuevoCliente_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(785, 73);
            btnEliminar.Margin = new Padding(3, 4, 3, 4);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(113, 31);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // FrmClientes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(btnEliminar);
            Controls.Add(btnNuevoCliente);
            Controls.Add(txtNombre);
            Controls.Add(lblNombre);
            Controls.Add(dgvClientes);
            Controls.Add(btnConsultar);
            Controls.Add(cmbOperativo);
            Controls.Add(txtEmpresaId);
            Controls.Add(label2);
            Controls.Add(Empresa);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmClientes";
            Text = "Clientes";
            Load += FrmClientes_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Empresa;
        private Label label2;
        private TextBox txtEmpresaId;
        private ComboBox cmbOperativo;
        private Button btnConsultar;
        private DataGridView dgvClientes;
        private DataGridViewTextBoxColumn nombre;
        private DataGridViewTextBoxColumn empresaid;
        private DataGridViewTextBoxColumn grupoid;
        private DataGridViewTextBoxColumn cuentaid;
        private DataGridViewTextBoxColumn comercial;
        private DataGridViewTextBoxColumn direccion;
        private DataGridViewTextBoxColumn direccion1;
        private DataGridViewTextBoxColumn poblacion;
        private DataGridViewTextBoxColumn cpid;
        private DataGridViewTextBoxColumn nifid;
        private DataGridViewTextBoxColumn nifpaisid;
        private DataGridViewTextBoxColumn telefono;
        private DataGridViewTextBoxColumn fax;
        private DataGridViewTextBoxColumn movil;
        private DataGridViewTextBoxColumn correo;
        private DataGridViewCheckBoxColumn factura_electronica;
        private TextBox txtNombre;
        private Label lblNombre;
        private Button btnNuevoCliente;
        private Button btnEliminar;
    }
}
