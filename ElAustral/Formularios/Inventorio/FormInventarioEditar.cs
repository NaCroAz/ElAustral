using MySql.Data.MySqlClient;
using Mysqlx.Connection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElAustral.Formularios.Inventorio
{
    public partial class FormInventarioEditar : Form
    {
        public int ProductoID { get; set; } // Propiedad para almacenar el ID del producto a editar.

        public FormInventarioEditar(int productoID)
        {
            InitializeComponent();
            ProductoID = productoID;
        }


        // Cargar los datos del producto existente en los controles
        private void CargarProducto()
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";
            string query = "SELECT * FROM productos WHERE id_producto = @ProductoID;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                using (var command = new MySqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@ProductoID", ProductoID);

                    try
                    {
                        conexion.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtProductoID.Text = reader["id_producto"].ToString();
                                txtNombre.Text = reader["nombre_producto"].ToString();
                                txtDescripcion.Text = reader["descripcion"].ToString();
                                txtPrecio.Text = reader["precio_venta"].ToString();
                                cbxHorarioComida.Text = reader["horario_comida"].ToString();
                                cbxCategoria.Text = reader["categoria"].ToString();
                                txtStock.Text = reader["stock"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el producto");
                                this.Close();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void FormInventarioEditar_Load(object sender, EventArgs e)
        {
            cbxCategoria.Enabled = false;
            // Carga los datos del producto a editar
            CargarProducto();
        }

        //Cerrar
        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Aceptar ediccion
        private void button2_Click_1(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";


            int ProductoID;
            if (!int.TryParse(txtProductoID.Text, out ProductoID))
            {
                MessageBox.Show("ID inválido");
                return;
            }
            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            int Precio;
            if (!int.TryParse(txtPrecio.Text, out Precio))
            {
                MessageBox.Show("Precio inválido");
                return;
            }
            string Horario_Comida = cbxHorarioComida.Text;
            string Categoria = cbxCategoria.Text;
            int Stock;
            if (!int.TryParse(txtStock.Text, out Stock))
            {
                MessageBox.Show("Stock inválido");
                return;
            }

            // Consulta UPDATE
            string query = "UPDATE productos " +
                           "SET nombre_producto = @Nombre, descripcion = @Descripcion, precio_venta = @Precio, " +
                           "horario_comida = @Horario_Comida, categoria = @Categoria, stock = @Stock " +
                           "WHERE id_producto = @ProductoID;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                using (var command = new MySqlCommand(query, conexion))
                {
                    command.Parameters.AddWithValue("@Nombre", Nombre);
                    command.Parameters.AddWithValue("@Descripcion", Descripcion);
                    command.Parameters.AddWithValue("@Precio", Precio);
                    command.Parameters.AddWithValue("@Horario_Comida", Horario_Comida);
                    command.Parameters.AddWithValue("@Categoria", Categoria);
                    command.Parameters.AddWithValue("@Stock", Stock);
                    command.Parameters.AddWithValue("@ProductoID", ProductoID); // Parámetro para el ID

                    try
                    {
                        conexion.Open();

                        int filasAfectadas = command.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Producto editado exitosamente");
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo editar el producto");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cbxHorarioComida_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string horarioComida = cbxHorarioComida.SelectedItem.ToString();
            List<string> datos = new List<string>();

            switch (horarioComida)
            {
                case "Desayuno":
                    cbxCategoria.Enabled = true;
                    datos.AddRange(new[] { "Clásicos", "Saludables", "Dulces", "Bebidas" });
                    break;
                case "Merienda":
                    cbxCategoria.Enabled = true;
                    datos.AddRange(new[] { "Sandwiches y bocadillos", "Ensaladas", "Repostería", "Bebidas calientes y frías" });
                    break;
                case "Almuerzo":
                    cbxCategoria.Enabled = true;
                    datos.AddRange(new[] { "Platos combinados", "Pastas", "Sopas y cremas", "Hamburguesas y sandwiches" });
                    break;
                case "Cenas":
                    cbxCategoria.Enabled = true;
                    datos.AddRange(new[] { "Carnes", "Pescados y mariscos", "Vegetarianos y veganas", "Pizzas" });
                    break;
                case "Bebidas":
                    cbxCategoria.Enabled = true;
                    datos.AddRange(new[] { "Alcohólicas", "No Alcohólicas" });
                    break;
                case "Otros":
                    cbxCategoria.Enabled = true;
                    datos.AddRange(new[] { "Postres", "Menú infantil", "Opciones para llevar" });
                    break;
                default:
                    cbxCategoria.Enabled = false;
                    break;
            }

            cbxCategoria.DataSource = datos;
        }
    }

}
