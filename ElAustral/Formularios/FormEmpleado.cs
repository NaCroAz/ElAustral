using ElAustral.Formularios.Empleado;
using ElAustral.Formularios.Inventorio;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
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
    public partial class FormEmpleado : System.Windows.Forms.Form
    {
        public FormEmpleado()
        {
            InitializeComponent();
        }

        // Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            FormMenuPrincipal principal = new FormMenuPrincipal();
            principal.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // Cargar empleados
        private void button5_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            txtNombre.Enabled = true;
            txtApellido.Enabled = true;
            cbxRol.Enabled = true;
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT empleados.id_empleado AS ID, CONCAT_WS(', ', empleados.apellido, empleados.nombre) AS Nombre, roles.nombre_rol AS Rol, roles.descripcion AS Descripcion FROM empleados " +
                        "JOIN roles ON empleados.id_rol = roles.id_rol;";

                    var adapter = new MySqlDataAdapter(query, conexion);
                    DataTable tablaProductos = new DataTable();
                    adapter.Fill(tablaProductos);
                    dataGridView1.DataSource = tablaProductos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        // Agregar empleados
        private void button2_Click(object sender, EventArgs e)
        {
            FormEmpleadoAgregarEmpleado formEmpleadoAgregarEmpleado = new FormEmpleadoAgregarEmpleado();
            formEmpleadoAgregarEmpleado.Show();
        }

        // Cambiar nombre de formulario
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "El Austral - " + tabControl1.SelectedTab.Text;
        }

        // Eliminar empleados
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                if (id > 0)
                {
                    DialogResult result = MessageBox.Show(
                        "Estas seguro de querer eliminar este empleado?",
                        "Confirmar Eliminacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        DeleteProductFromDatabase(id);

                        string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

                        using (var conexion = new MySqlConnection(connectionString))
                        {
                            try
                            {
                                conexion.Open();
                                string query = "SELECT empleados.id_empleado AS ID, CONCAT_WS(',', empleados.apellido, empleados.nombre) AS Nombre, roles.nombre_rol AS Rol, roles.descripcion AS Descripcion FROM empleados " +
                                    "JOIN roles ON empleados.id_rol = roles.id_rol;";

                                var adapter = new MySqlDataAdapter(query, conexion);
                                DataTable tablaProductos = new DataTable();
                                adapter.Fill(tablaProductos);

                                dataGridView1.DataSource = tablaProductos;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Por favor seleccione un empleado valido a eliminar.");
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un empleado a eliminar.");
            }
        }

        // Función eliminar
        private void DeleteProductFromDatabase(int id)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";
            string query = "DELETE FROM empleados WHERE id_empleado = @id";

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

        // Editar empleados
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idEmpleado = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

                int idRol = 0;

                using (var conexion = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conexion.Open();

                        string query = "SELECT id_rol FROM empleados WHERE id_empleado = @idEmpleado;";

                        using (var command = new MySqlCommand(query, conexion))
                        {
                            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                            object result = command.ExecuteScalar();
                            if (result != null)
                            {
                                idRol = Convert.ToInt32(result);
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el rol para el empleado seleccionado.");
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                        return;
                    }
                }

                if (idEmpleado > 0 && idRol > 0)
                {
                    FormEmpleadoEditarEmpleado formEmpleadoEditarEmpleado = new FormEmpleadoEditarEmpleado(idEmpleado, idRol);
                    formEmpleadoEditarEmpleado.Show();
                }
                else
                {
                    MessageBox.Show("No se pudo obtener el rol del empleado. Por favor, intente nuevamente.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un empleado a editar.");
            }
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            txtApellido.Enabled = false;
            txtNombre.Enabled = false;
            cbxRol.Enabled = false;

            txtNombre.TextChanged += new EventHandler(ValidarCampos);
            txtApellido.TextChanged += new EventHandler(ValidarCampos);
            cbxRol.SelectedIndexChanged += new EventHandler(ValidarCampos);
            button9.Enabled = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string rol = cbxRol.Text.Trim();

            CargarUsuariosFiltrados(nombre, apellido, rol);
        }

        private void CargarUsuariosFiltrados(string nombre, string apellido, string rol)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            try
            {
                StringBuilder query = new StringBuilder(@"SELECT empleados.id_empleado AS ID, 
                                                        CONCAT_WS(', ', empleados.apellido, empleados.nombre) AS Nombre, 
                                                        roles.nombre_rol AS Rol, 
                                                        roles.descripcion AS Descripcion 
                                                 FROM empleados 
                                                 JOIN roles ON empleados.id_rol = roles.id_rol 
                                                 WHERE 1=1");

                if (!string.IsNullOrEmpty(nombre))
                {
                    query.Append(" AND empleados.nombre LIKE @nombre");
                }
                if (!string.IsNullOrEmpty(apellido))
                {
                    query.Append(" AND empleados.apellido LIKE @apellido");
                }
                if (!string.IsNullOrEmpty(rol))
                {
                    query.Append(" AND roles.nombre_rol LIKE @rol");
                }

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand(query.ToString(), connection))
                    {
                        if (!string.IsNullOrEmpty(nombre))
                        {
                            cmd.Parameters.AddWithValue("@nombre", "%" + nombre + "%");
                        }
                        if (!string.IsNullOrEmpty(apellido))
                        {
                            cmd.Parameters.AddWithValue("@apellido", "%" + apellido + "%");
                        }
                        if (!string.IsNullOrEmpty(rol))
                        {
                            cmd.Parameters.AddWithValue("@rol", "%" + rol + "%");
                        }

                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable empleadosTable = new DataTable();
                        adapter.Fill(empleadosTable);

                        dataGridView1.DataSource = empleadosTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message);
            }
        }

        private void ValidarCampos(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) ||
                !string.IsNullOrWhiteSpace(txtApellido.Text) ||
                cbxRol.SelectedIndex != -1)
            {
                button9.Enabled = true;
            }
            else
            {
                button9.Enabled = false;
            }
        }
    }
}