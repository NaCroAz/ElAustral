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
    public partial class FormInventarioAgregar : Form
    {
        public FormInventarioAgregar()
        {
            InitializeComponent();
        }

        //Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Agregar
        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            string Nombre = txtNombre.Text;
            string Descripcion = txtDescripcion.Text;
            int Precio;
            if (!int.TryParse(txtPrecio.Text, out Precio))
            {   
                MessageBox.Show("Precio invalido");
                return;
            }
            string Horario_Comida = cbxHorarioComida.Text;
            string Categoria = cbxCategoria.Text;
            int Stock;
            if (!int.TryParse(txtStock.Text, out Stock))
            {
                MessageBox.Show("Stock invalido");
                return;
            }

            string querry = "INSERT INTO productos (nombre_producto, descripcion, precio_venta, horario_comida, categoria, stock) " +
                            "VALUES (@Nombre, @Descripcion, @Precio, @Horario_Comida, @Categoria, @Stock);";

            using (var conexion = new MySqlConnection(connectionString))
            {
                using (var comand = new MySqlCommand(querry, conexion))
                {
                    comand.Parameters.AddWithValue("@Nombre",Nombre);
                    comand.Parameters.AddWithValue("@Descripcion", Descripcion);
                    comand.Parameters.AddWithValue("@Precio", Precio);
                    comand.Parameters.AddWithValue("@Horario_Comida", Horario_Comida);
                    comand.Parameters.AddWithValue("@Categoria", Categoria);
                    comand.Parameters.AddWithValue("@Stock", Stock);

                    try
                    {
                        conexion.Open();

                        int filasAfectadas = comand.ExecuteNonQuery();

                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Producto agregado exitosamente");
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo agregar el producto");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cbxCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        //Opciones para Categoria, headhache
        private void cbxHorarioComida_SelectedIndexChanged(object sender, EventArgs e)
        {
            string horarioComida = cbxHorarioComida.SelectedItem.ToString();
            List<string> datos = new List<string>();

            switch (horarioComida)
            {
                case "Desayuno":
                    cbxCategoria.Enabled = true;
                    datos.Add("Clásicos");
                    datos.Add("Saludables");
                    datos.Add("Dulces");
                    datos.Add("Bebidas");
                    break;
                case "Merienda":
                    cbxCategoria.Enabled = true;
                    datos.Add("Sandwiches y bocadillos");
                    datos.Add("Ensaladas");
                    datos.Add("Reposteria");
                    datos.Add("Bebidas calientes y frias");
                    break;
                case "Almuerzo":
                    cbxCategoria.Enabled = true;
                    datos.Add("Platos combinados");
                    datos.Add("Pastas");
                    datos.Add("Sopas y cremas");
                    datos.Add("Hamburguesas y sandwiches");
                    break;
                case "Cenas":
                    cbxCategoria.Enabled = true;
                    datos.Add("Carnes");
                    datos.Add("Pescados y mariscos");
                    datos.Add("Vegetarianos y veganas");
                    datos.Add("Pizzas");
                    break;
                case "Bebidas":
                    cbxCategoria.Enabled = true;
                    datos.Add("Alcoholicas");
                    datos.Add("No Alcoholicas");
                    break;
                case "Otros":
                    cbxCategoria.Enabled = true;
                    datos.Add("Postres");
                    datos.Add("Menu infantil");
                    datos.Add("Opciones para llevar");
                    break;
                default:
                    cbxCategoria.Enabled = false;
                    break;
            }

            cbxCategoria.DataSource = datos;
        }

        private void FormInventarioAgregar_Load(object sender, EventArgs e)
        {
            cbxCategoria.Enabled = false;
        }
    }
}
