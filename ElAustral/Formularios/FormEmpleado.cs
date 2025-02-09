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

        //Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            FormMenuPrincipal principal = new FormMenuPrincipal();
            principal.Show();
            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Cargar empleados
        private void button5_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;

            txtNombre.Enabled = true;  
            txtApellido.Enabled = true;
            cbxRol.Enabled = true;
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))  // Cambia MySqlConnection por el de MySqlConnector
            {
                try
                {
                    conexion.Open();
                    string query = "SELECT empleados.id_empleado AS ID, CONCAT_WS(', ', empleados.apellido, empleados.nombre) AS Nombre, roles.nombre_rol AS Rol, roles.descripcion AS Descripcion FROM empleados " +
                        "JOIN roles ON empleados.id_rol = roles.id_rol;";  // Cambia 'productos' por el nombre de tu tabla

                    var adapter = new MySqlDataAdapter(query, conexion);  // Cambia MySqlDataAdapter por el de MySqlConnector
                    DataTable tablaProductos = new DataTable();
                    adapter.Fill(tablaProductos);

                    dataGridView2.DataSource = null; // Elimina el enlace existente
                    dataGridView2.Rows.Clear();     // Limpia las filas
                    dataGridView2.Refresh();        // Refresca el control para evitar datos residuales

                    // Asigna el DataTable al DataGridView
                    dataGridView1.DataSource = tablaProductos;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }
        }

        //Agregar empleados
        private void button2_Click(object sender, EventArgs e)
        {
            FormEmpleadoAgregarEmpleado formEmpleadoAgregarEmpleado = new FormEmpleadoAgregarEmpleado();
            formEmpleadoAgregarEmpleado.Show();
        }

        //Cambiar nombre de formulario
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Text = "El Austral - " + tabControl1.SelectedTab.Text;
        }

        //Eliminar empleados
        private void button4_Click(object sender, EventArgs e)
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
                        "Estas seguro de querer eliminar este empleado?",
                        "Confirmar Eliminacion",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        // Delete the product from the database
                        DeleteProductFromDatabase(id); 

                        string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

                        using (var conexion = new MySqlConnection(connectionString))  // Cambia MySqlConnection por el de MySqlConnector
                        {
                            try
                            {
                                conexion.Open();
                                string query = "SELECT empleados.id_empleado AS ID, CONCAT_WS(',', empleados.apellido, empleados.nombre) AS Nombre, roles.nombre_rol AS Rol, roles.descripcion AS Descripcion FROM empleados " +
                                    "JOIN roles ON empleados.id_rol = roles.id_rol;";  // Cambia 'productos' por el nombre de tu tabla

                                var adapter = new MySqlDataAdapter(query, conexion);  // Cambia MySqlDataAdapter por el de MySqlConnector
                                DataTable tablaProductos = new DataTable();
                                adapter.Fill(tablaProductos);

                                // Asigna el DataTable al DataGridView
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
    
        //Funcion eliminar
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

        //Editar empleados
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Obtener la fila seleccionada y su ID
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idEmpleado = Convert.ToInt32(selectedRow.Cells["ID"].Value);

                // Cadena de conexión
                string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

                int idRol = 0;

                using (var conexion = new MySqlConnection(connectionString)) // Cambia a MySqlConnector si es necesario
                {
                    try
                    {
                        // Abrir conexión
                        conexion.Open();

                        // Consulta para obtener el id_rol del empleado seleccionado
                        string query = "SELECT id_rol FROM empleados WHERE id_empleado = @idEmpleado;";

                        using (var command = new MySqlCommand(query, conexion))
                        {
                            // Parámetro para evitar inyecciones SQL
                            command.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                            // Ejecutar el comando
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
                        // Manejo de errores al conectarse a la base de datos
                        MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                        return;
                    }
                }

                // Verificar que se obtuvo un idRol válido
                if (idEmpleado > 0 && idRol > 0)
                {
                    // Abrir el formulario de edición con los datos del empleado y el rol
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

        //Cargar horarios y asistencias
        private void button8_Click(object sender, EventArgs e)
        {
            button6.Enabled = true;
            button7.Enabled = true;
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    string query = @"SELECT DISTINCT empleados.id_empleado AS ID, horarios.fecha AS Fecha, horarios.hora_entrada AS Inicio,
                    asistencias.hora_entrada_real AS Llegada, asistencias.hora_salida_real AS Salida,
                    horarios.hora_salida AS Fin
                    FROM empleados
                    JOIN horarios ON empleados.id_empleado = horarios.id_empleado
                    JOIN asistencias ON empleados.id_empleado = asistencias.id_empleado
                    ORDER BY empleados.id_empleado ASC, horarios.hora_entrada ASC;
                    ";

                    var adapter = new MySqlDataAdapter(query, conexion);
                    DataTable tablaHorarios = new DataTable();

                    // Limpia las filas existentes del DataGridView antes de cargar nuevos datos
                    dataGridView2.DataSource = null; // Elimina el enlace existente
                    dataGridView2.Rows.Clear();     // Limpia las filas
                    dataGridView2.Refresh();        // Refresca el control para evitar datos residuales

                    adapter.Fill(tablaHorarios);

                    // Asigna el DataTable al DataGridView
                    dataGridView2.DataSource = tablaHorarios;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }


        }

        //Agregar horario y asistencia
        private void button6_Click(object sender, EventArgs e)
        {
            FormEmpleadoAgregarHorarioAsistencia formEmpleadoAgregarHorarioAsistencia = new FormEmpleadoAgregarHorarioAsistencia();
            formEmpleadoAgregarHorarioAsistencia.Show();
        }

        private void FormEmpleado_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            button6.Enabled = false;
            button7.Enabled = false;

            txtApellido.Enabled = false;
            txtNombre.Enabled = false;
            cbxRol.Enabled = false;

            txtNombre.TextChanged += new EventHandler(ValidarCampos);
            txtApellido.TextChanged += new EventHandler(ValidarCampos);
            cbxRol.SelectedIndexChanged += new EventHandler(ValidarCampos);
            button9.Enabled = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView2.SelectedRows[0];

                // Extrae los valores de las celdas
                string idEmpleado = filaSeleccionada.Cells["ID"].Value.ToString();
                string fecha = filaSeleccionada.Cells["Fecha"].Value.ToString();
                string horaEntrada = filaSeleccionada.Cells["Inicio"].Value.ToString();
                string horaSalida = filaSeleccionada.Cells["Fin"].Value.ToString();

                // Abre el formulario de edición y pasa los valores como parámetros
                FormEmpleadoEditarHorarioAsistencia formEmpleadoEditarHorarioAsistencia = new FormEmpleadoEditarHorarioAsistencia(idEmpleado, horaEntrada, horaSalida);
                formEmpleadoEditarHorarioAsistencia.ShowDialog();

                // Refresca el DataGridView después de cerrar el formulario
                CargarHorarios();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para editar.");
            }
        }
        private void CargarHorarios()
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            string query = @"SELECT DISTINCT empleados.id_empleado AS ID, horarios.fecha AS Fecha, horarios.hora_entrada AS Inicio,
                            asistencias.hora_entrada_real AS Llegada, asistencias.hora_salida_real AS Salida,
                            horarios.hora_salida AS Fin
                     FROM empleados
                     JOIN horarios ON empleados.id_empleado = horarios.id_empleado
                     JOIN asistencias ON empleados.id_empleado = asistencias.id_empleado
                     ORDER BY empleados.id_empleado ASC, horarios.hora_entrada ASC;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    var adapter = new MySqlDataAdapter(query, conexion);
                    DataTable tablaHorarios = new DataTable();
                    adapter.Fill(tablaHorarios);
                    dataGridView2.DataSource = tablaHorarios;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar horarios: " + ex.Message);
                }
            }
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
                // Iniciar la consulta base
                StringBuilder query = new StringBuilder(@"SELECT empleados.id_empleado AS ID, 
                                                        CONCAT_WS(', ', empleados.apellido, empleados.nombre) AS Nombre, 
                                                        roles.nombre_rol AS Rol, 
                                                        roles.descripcion AS Descripcion 
                                                 FROM empleados 
                                                 JOIN roles ON empleados.id_rol = roles.id_rol 
                                                 WHERE 1=1"); // WHERE 1=1 para facilitar la concatenación de filtros dinámicos

                // Añadir condiciones dinámicas según los valores introducidos
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

                // Crear la conexión a la base de datos
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear el comando SQL
                    using (MySqlCommand cmd = new MySqlCommand(query.ToString(), connection))
                    {
                        // Añadir parámetros solo si se introducen valores
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

                        // Ejecutar el comando y obtener los datos en un DataTable
                        MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                        DataTable empleadosTable = new DataTable();
                        adapter.Fill(empleadosTable);

                        // Asignar los datos al DataGridView
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
            // Comprobar si alguno de los campos tiene algún valor
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) ||
                !string.IsNullOrWhiteSpace(txtApellido.Text) ||
                cbxRol.SelectedIndex != -1) // -1 significa que no hay ninguna opción seleccionada
            {
                button9.Enabled = true;  // Activar el botón si hay algo en los campos
            }
            else
            {
                button9.Enabled = false; // Desactivar el botón si todos los campos están vacíos
            }
        }
    }
}

