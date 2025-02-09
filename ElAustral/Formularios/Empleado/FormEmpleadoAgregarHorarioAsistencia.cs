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

namespace ElAustral.Formularios.Empleado
{
    public partial class FormEmpleadoAgregarHorarioAsistencia : System.Windows.Forms.Form
    {
        public FormEmpleadoAgregarHorarioAsistencia()
        {
            InitializeComponent();
        }

        //Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void CargarEmpleados()
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";
            string query = "SELECT id_empleado, nombre FROM empleados";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (var command = new MySqlCommand(query, conexion))
                    using (var reader = command.ExecuteReader())
                    {
                        Dictionary<int, string> empleados = new Dictionary<int, string>();

                        while (reader.Read())
                        {
                            empleados.Add(Convert.ToInt32(reader["id_empleado"]), reader["nombre"].ToString());
                        }

                        cbxEmpleados.DataSource = new BindingSource(empleados, null);
                        cbxEmpleados.DisplayMember = "Value"; // Muestra el nombre
                        cbxEmpleados.ValueMember = "Key"; // Usa el ID como valor
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar empleados: " + ex.Message);
                }
            }
        }

        private void FormEmpleadoAgregarHorarioAsistencia_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
        }

        private void cbxTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Configurar horarios según el turno seleccionado
            switch (cbxTurno.Text)
            {
                case "Mañana":
                    txtEntrada.Text = "08:00";
                    txtSalida.Text = "16:00";
                    break;

                case "Tarde":
                    txtEntrada.Text = "16:00";
                    txtSalida.Text = "00:00";
                    break;

                case "Noche":
                    txtEntrada.Text = "00:00";
                    txtSalida.Text = "08:00";
                    break;

                default:
                    txtEntrada.Text = "";
                    txtSalida.Text = "";
                    break;
            }
        }

        //Ingresar
        private void button2_Click(object sender, EventArgs e)
        {
            int idEmpleado = ((KeyValuePair<int, string>)cbxEmpleados.SelectedItem).Key;
            string fecha = dtFecha.Text;
            string entrada = txtEntrada.Text;
            string salida = txtSalida.Text;

            string queryHorarios = "INSERT INTO horarios (id_empleado, fecha, hora_entrada, hora_salida) VALUES (@idEmpleado, @fecha, @entrada, @salida)";
            string queryAsistencias = "INSERT INTO asistencias (id_empleado) VALUES (@idEmpleado);";
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (var command = new MySqlCommand(queryHorarios, conexion))
                    {
                        command.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                        command.Parameters.AddWithValue("@fecha", fecha);
                        command.Parameters.AddWithValue("@entrada", entrada);
                        command.Parameters.AddWithValue("@salida", salida);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Turno agregado correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el turno: " + ex.Message);
                }
            }

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (var command = new MySqlCommand(queryAsistencias, conexion))
                    {
                        command.Parameters.AddWithValue("@idEmpleado", idEmpleado);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Turno agregado correctamente.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el turno: " + ex.Message);
                }
            }
        }

        private void cbxEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
