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
    public partial class FormReporte : Form
    {
        public FormReporte()
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
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))  // Cambia MySqlConnection por el de MySqlConnector
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT fecha AS Fecha, SUM(total) AS Total " +
                        "FROM ventas " +
                        "GROUP BY fecha " +
                        "ORDER BY fecha;";  // Cambia 'productos' por el nombre de tu tabla

                    var adapter = new MySqlDataAdapter(query, conexion);  // Cambia MySqlDataAdapter por el de MySqlConnector
                    DataTable ventasTotales = new DataTable();
                    adapter.Fill(ventasTotales);
                    dataGridView1.DataSource = ventasTotales;


                    string queryTotal = "SELECT SUM(total) AS total_vendido FROM ventas;";
                    MySqlCommand commandTotal = new MySqlCommand(queryTotal, conexion);
                    object result = commandTotal.ExecuteScalar();
                    decimal totalVendido = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                    lblTotal.Text = $"{totalVendido:C}"; // Formato moneda

                    string queryProductos = "SELECT nombre_producto AS Producto, precio_venta AS Precio, stock AS Stock FROM productos WHERE stock <= 20;";
                    MySqlCommand commandProductos = new MySqlCommand(queryProductos, conexion);
                    MySqlDataAdapter adapterProductos = new MySqlDataAdapter(commandProductos);
                    DataTable table = new DataTable();
                    adapterProductos.Fill(table);
                    dataGridView2.DataSource = table;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }
    }
}
