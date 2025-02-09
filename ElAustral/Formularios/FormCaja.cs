using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElAustral
{
    public partial class FormCaja : Form
    {
        public FormCaja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMenuPrincipal principal = new FormMenuPrincipal();
            principal.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dtFechaFinal.Enabled = true;
            dtFechaInicial.Enabled = true;
            btnFiltrar.Enabled = true;

            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT id_venta AS ID, fecha AS Fecha, total AS Total, detalle_venta AS Detalles FROM ventas";

                    var adapter = new MySqlDataAdapter(query, conexion);
                    DataTable ventas = new DataTable();
                    adapter.Fill(ventas);

                    dataGridView1.DataSource = ventas;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las ventas" + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dtFechaFinal.Enabled = false;
            dtFechaInicial.Enabled = false;
            btnFiltrar.Enabled = false;

            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT pedidos.id_pedido AS ID, pedidos.cantidad AS Cantidad, " +
               "ventas.detalle_venta AS Detalles " +
               "FROM pedidos " +
               "JOIN ventas ON pedidos.id_venta = ventas.id_venta";

                    var adapter = new MySqlDataAdapter(query, conexion);
                    DataTable ventas = new DataTable();
                    adapter.Fill(ventas);

                    dataGridView1.DataSource = ventas;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar las ventas" + ex.Message);
                }
            }
        }

        private void FormCaja_Load(object sender, EventArgs e)
        {
            dtFechaFinal.Enabled = false;
            dtFechaInicial.Enabled = false;
            btnFiltrar.Enabled = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTime fechaInicio = dtFechaInicial.Value.Date;
            DateTime fechaFin = dtFechaFinal.Value.Date;

            // Llamar al método para cargar los datos filtrados
            CargarVentasFiltradas(fechaInicio, fechaFin);
        }

        private void CargarVentasFiltradas(DateTime fechaInicio, DateTime fechaFin)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            try
            {
                string query = "SELECT id_venta AS ID, fecha AS Fecha, total AS Total, detalle_venta AS Detalles FROM ventas " +
                    "WHERE DATE(fecha) BETWEEN @fechaInicio AND @fechaFin;";

                using (MySqlConnection con = new MySqlConnection(connectionString))
                {
                    con.Open();

                    using (MySqlCommand command = new MySqlCommand(query, con))
                    {
                        command.Parameters.AddWithValue("@fechaInicio", fechaInicio);
                        command.Parameters.AddWithValue("@fechaFin", fechaFin);

                        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                        DataTable ventasTable = new DataTable();
                        adapter.Fill(ventasTable);

                        // Asignar los datos al DataGridView
                        dataGridView1.DataSource = ventasTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}
