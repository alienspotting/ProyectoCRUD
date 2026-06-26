namespace ProyectoCRUD
{
    partial class FrmPantallaBienvenida
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bienvenido = new Label();
            panel1 = new Panel();
            tableLayoutPanel1 = new TableLayoutPanel();
            btnIniciar = new Button();
            panel1.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // bienvenido
            // 
            bienvenido.AutoSize = true;
            bienvenido.Dock = DockStyle.Fill;
            bienvenido.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            bienvenido.Location = new Point(107, 38);
            bienvenido.Name = "bienvenido";
            bienvenido.Size = new Size(306, 67);
            bienvenido.TabIndex = 1;
            bienvenido.Text = "¡Bienvenido!";
            bienvenido.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(521, 360);
            panel1.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(btnIniciar, 1, 2);
            tableLayoutPanel1.Controls.Add(bienvenido, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 4;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 15.12605F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 26.7782421F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 58.1589966F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 107F));
            tableLayoutPanel1.Size = new Size(521, 360);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // btnIniciar
            // 
            btnIniciar.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnIniciar.BackColor = Color.FromArgb(192, 192, 255);
            btnIniciar.BackgroundImageLayout = ImageLayout.Center;
            btnIniciar.Location = new Point(107, 109);
            btnIniciar.Margin = new Padding(3, 4, 3, 4);
            btnIniciar.Name = "btnIniciar";
            btnIniciar.Size = new Size(306, 139);
            btnIniciar.TabIndex = 1;
            btnIniciar.Text = "INICIAR";
            btnIniciar.UseVisualStyleBackColor = false;
            btnIniciar.Click += btnIniciar_Click_1;
            // 
            // FrmPantallaBienvenida
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(521, 360);
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FrmPantallaBienvenida";
            Text = "Bienvenida";
            panel1.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label bienvenido;
        private Panel panel1;
        private TableLayoutPanel tableLayoutPanel1;
        private Button btnIniciar;
    }
}