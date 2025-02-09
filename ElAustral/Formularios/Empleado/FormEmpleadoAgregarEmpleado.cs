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
    public partial class FormEmpleadoAgregarEmpleado : System.Windows.Forms.Form
    {
        public FormEmpleadoAgregarEmpleado()
        {
            InitializeComponent();
        }

        //Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        //Agregar
        private void button2_Click(object sender, EventArgs e)
        {
            InsertarEmpleado();
            this.Hide();
        }

        //Funcione para insertar
        private void InsertarEmpleado()
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string fechaNacimiento = dtNacimiento.Value.ToString("yyyy-MM-dd");
            string direccion = txtDireccion.Text.Trim();
            string telefono = txtTelefono.Text.Trim();
            string correoElectronico = txtCorreo.Text.Trim();

            string nombreRol = cbxRol.Text.Trim();
            string descripcion = txtDescripcion.Text.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(nombreRol))
            {
                MessageBox.Show("Por favor, complete todos los campos obligatorios.");
                return;
            }

            string queryEmpleado = @"
            INSERT INTO empleados 
            (nombre, apellido, fecha_nacimiento, direccion, telefono, correo_electronico, id_rol) 
            VALUES (@Nombre, @Apellido, @FechaNacimiento, @Direccion, @Telefono, @CorreoElectronico, @IdRol);";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Insertar el empleado
                    using (var commandEmpleado = new MySqlCommand(queryEmpleado, conexion))
                    {
                        commandEmpleado.Parameters.AddWithValue("@Nombre", nombre);
                        commandEmpleado.Parameters.AddWithValue("@Apellido", apellido);
                        commandEmpleado.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                        commandEmpleado.Parameters.AddWithValue("@Direccion", direccion);
                        commandEmpleado.Parameters.AddWithValue("@Telefono", telefono);
                        commandEmpleado.Parameters.AddWithValue("@CorreoElectronico", correoElectronico);

                        int idRolAux = 0;

                        // Asignar idRolAux según el rol seleccionado con switch-case
                        switch (nombreRol)
                        {
                            case "Gerente del bar":
                                idRolAux = 1;
                                break;
                            case "Bartender":
                                idRolAux = 2;
                                break;
                            case "Ayudante de bar":
                                idRolAux = 3;
                                break;
                            case "Cajero":
                                idRolAux = 4;
                                break;
                            default:
                                MessageBox.Show("Rol inexistente. Por favor seleccione un rol válido.");
                                return;
                        }

                        commandEmpleado.Parameters.AddWithValue("@IdRol", idRolAux);

                        commandEmpleado.ExecuteNonQuery();
                    }

                    MessageBox.Show("Empleado insertado correctamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void FormEmpleadoAgregarEmpleado_Load(object sender, EventArgs e)
        {

        }
    }

}

