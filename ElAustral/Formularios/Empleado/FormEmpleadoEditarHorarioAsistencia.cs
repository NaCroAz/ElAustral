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
    public partial class FormEmpleadoEditarHorarioAsistencia : System.Windows.Forms.Form
    {
        private string idEmpleado;
        private string fecha;

        public FormEmpleadoEditarHorarioAsistencia(string idEmpleado, string horaEntrada, string horaSalida)
        {
            InitializeComponent();

            // Guarda los valores iniciales
            this.idEmpleado = idEmpleado;

            // Rellena los campos con los datos recibidos
            txtEntrada.Text = horaEntrada;
            txtSalida.Text = horaSalida;
            cbxEmpleados.Text = idEmpleado;
            if (horaEntrada == "08:00:00")
            {
                cbxTurno.Text = "Mañana";
            }
            else if (horaEntrada == "16:00:00")
            {
                cbxTurno.Text = "Tarde";
            }
            else if (horaEntrada == "00:00:00")
            {
                cbxTurno.Text = "Noche";
            }
            else
            {
                cbxTurno.Text = "ERROR";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormEmpleadoEditarHorarioAsistencia_Load(object sender, EventArgs e)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            string query = "SELECT id_empleado, nombre FROM empleados";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (var command = new MySqlCommand(query, conexion))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var empleados = new List<KeyValuePair<string, string>>();
                            while (reader.Read())
                            {
                                string id = reader["id_empleado"].ToString();
                                string nombre = reader["nombre"].ToString();
                                empleados.Add(new KeyValuePair<string, string>(id, nombre));
                            }

                            // Configura el ComboBox
                            cbxEmpleados.DataSource = empleados;
                            cbxEmpleados.DisplayMember = "Value"; // Mostrar el nombre del empleado
                            cbxEmpleados.ValueMember = "Key";    // Guardar el id del empleado
                        }
                    }

                    // Selecciona el empleado actual en el ComboBox
                    cbxEmpleados.SelectedValue = idEmpleado;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar los empleados: " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Obtén los valores actualizados de los controles
            string nuevaHoraEntrada = txtEntrada.Text;
            string nuevaHoraSalida = txtSalida.Text;

            // Actualiza los datos en la base de datos
            ActualizarHorarioEnBaseDeDatos(nuevaHoraEntrada, nuevaHoraSalida);

            // Cierra el formulario después de guardar
            this.Close();
        }

        private void ActualizarHorarioEnBaseDeDatos(string nuevaHoraEntrada, string nuevaHoraSalida)
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";

            string query = @"UPDATE horarios 
                         SET hora_entrada = @HoraEntrada, hora_salida = @HoraSalida 
                         WHERE id_empleado = @IdEmpleado AND fecha = @Fecha";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();
                    using (var command = new MySqlCommand(query, conexion))
                    {
                        command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        command.Parameters.AddWithValue("@Fecha", fecha);
                        command.Parameters.AddWithValue("@HoraEntrada", nuevaHoraEntrada);
                        command.Parameters.AddWithValue("@HoraSalida", nuevaHoraSalida);

                        int filasAfectadas = command.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            MessageBox.Show("Horario actualizado correctamente.");
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el horario para actualizar.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el horario: " + ex.Message);
                }
            }
        }

        private void cbxTurno_SelectedIndexChanged(object sender, EventArgs e)
        {
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
    }
}
