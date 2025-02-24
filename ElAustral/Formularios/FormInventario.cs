using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElAustral.Formularios.Inventorio;
using MySql.Data.MySqlClient;



namespace ElAustral
{
    public partial class FormInventario : Form
    {
        public FormInventario()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, EventArgs e)
        {

        }

        //Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            FormMenuPrincipal principal = new FormMenuPrincipal();
            principal.Show();
            this.Hide();
        }

        private void FormInventario_Load(object sender, EventArgs e)
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Cargar
        //Cargar
        //Cargar
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Text = ("Recargar Productos");
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))  // Cambia MySqlConnection por el de MySqlConnector
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT id_producto AS ID, nombre_producto AS Producto, descripcion AS Descripcion, precio_venta AS Precio, horario_comida AS Horario, categoria AS Categoria, stock AS Stock FROM productos";  // Cambia 'productos' por el nombre de tu tabla

                    var adapter = new MySqlDataAdapter(query, conexion);  // Cambia MySqlDataAdapter por el de MySqlConnector
                    DataTable tablaProductos = new DataTable();
                    adapter.Fill(tablaProductos);

                    // Asigna el DataTable al DataGridView
                    dataGridView1.DataSource = tablaProductos;

                    // Ajustar el ancho de las columnas
                    dataGridView1.Columns["ID"].Width = 50; // Ancho fijo para la columna ID
                    dataGridView1.Columns["Precio"].Width = 70; // Ancho fijo para la columna Precio
                    dataGridView1.Columns["Stock"].Width = 70; // Ancho fijo para la columna Stock
                    dataGridView1.Columns["Producto"].Width = 150; // Ancho fijo para la columna Producto
                    dataGridView1.Columns["Descripcion"].Width = 200; // Ancho fijo para la columna Descripcion
                    dataGridView1.Columns["Horario"].Width = 100; // Ancho fijo para la columna Horario
                    dataGridView1.Columns["Categoria"].Width = 100; // Ancho fijo para la columna Categoria

                    // Ajustar automáticamente el ancho de las columnas según el contenido
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        //Agregar
        private void button3_Click(object sender, EventArgs e)
        {
            FormInventarioAgregar formInventarioAgregar = new FormInventarioAgregar();
            formInventarioAgregar.Show();

        }

        //Eliminar
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row and its ID
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                if (id > 0)
                {
                    // Confirmation dialog
                    DialogResult result = MessageBox.Show(
                        "Estas seguro que quieres eliminar este producto?",
                        "Confirmar Eliminacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Delete the product from the database
                        DeleteProductFromDatabase(id);
                        string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

                        var conexion = new MySqlConnection(connectionString);  
                        string query = "SELECT id_producto AS ID, nombre_producto AS Producto, descripcion AS Descripcion, precio_venta AS Precio, horario_comida AS Horario, categoria AS Categoria, stock AS Stock FROM productos";

                        var adapter = new MySqlDataAdapter(query, conexion);
                        DataTable tablaProductos = new DataTable();
                        adapter.Fill(tablaProductos);

                        // Asigna el DataTable al DataGridView
                        dataGridView1.DataSource = tablaProductos;
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un producto valido a eliminar.");
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un producto a eliminar.");
            }
        }
        private void DeleteProductFromDatabase(int id)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";
            string query = "DELETE FROM productos WHERE id_producto = @id";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected row and its ID
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                if (id > 0)
                {
                    FormInventarioEditar formInventarioEditar = new FormInventarioEditar(id);
                    formInventarioEditar.Show();
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un producto valido a editar.");
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un producto a editar.");
            }

        }
    }
}
