namespace Austral
{
    partial class FormVentasDeLaFecha
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NumeroDeMesa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Pedido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDePago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mozo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumeroDeMesa,
            this.Pedido,
            this.Precio,
            this.TipoDePago,
            this.Mozo,
            this.FechaHora});
            this.dataGridView1.Location = new System.Drawing.Point(22, 93);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1059, 394);
            this.dataGridView1.TabIndex = 0;
            // 
            // NumeroDeMesa
            // 
            this.NumeroDeMesa.HeaderText = "NumeroDeMesa";
            this.NumeroDeMesa.Name = "NumeroDeMesa";
            this.NumeroDeMesa.Width = 175;
            // 
            // Pedido
            // 
            this.Pedido.HeaderText = "Pedido";
            this.Pedido.Name = "Pedido";
            this.Pedido.Width = 175;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 175;
            // 
            // TipoDePago
            // 
            this.TipoDePago.HeaderText = "TipoDePago";
            this.TipoDePago.Name = "TipoDePago";
            this.TipoDePago.Width = 175;
            // 
            // Mozo
            // 
            this.Mozo.HeaderText = "Mozo";
            this.Mozo.Name = "Mozo";
            this.Mozo.Width = 175;
            // 
            // FechaHora
            // 
            this.FechaHora.HeaderText = "Fecha y Hora";
            this.FechaHora.Name = "FechaHora";
            this.FechaHora.Width = 175;
            // 
            // FormVentasDeLaFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 624);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FormVentasDeLaFecha";
            this.Text = "FormVentasDeLaFecha";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroDeMesa;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pedido;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDePago;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mozo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaHora;
    }
}