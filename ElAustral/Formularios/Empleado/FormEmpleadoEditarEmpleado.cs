
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
    public partial class FormEmpleadoEditarEmpleado : System.Windows.Forms.Form
    {
        public int EmpleadoID { get; set; }

        public int RolID { get; set; }

        public FormEmpleadoEditarEmpleado(int empleadoID, int rolID)
        {
            InitializeComponent();
            EmpleadoID = empleadoID;
            RolID = rolID;
        }

        //Funcion cargar
        public void CargarDatos()
        {
            string connectionString = "Server=127.0.0.1;Database=bar_el_austral;User Id=usuario1;Password=;SslMode=None;";
            string queryEmpleado = "SELECT * FROM empleados WHERE id_empleado = @EmpleadoID;";
            string queryRol = "SELECT nombre_rol, descripcion FROM roles WHERE id_rol = @RolID;";

            using (var conexion = new MySqlConnection(connectionString))
            {
                try
                {
                    conexion.Open();

                    // Obtener los datos del empleado
                    using (var commandEmpleado = new MySqlCommand(queryEmpleado, conexion))
                    {
                        commandEmpleado.Parameters.AddWithValue("@EmpleadoID", EmpleadoID);

                        using (var readerEmpleado = commandEmpleado.ExecuteReader())
                        {
                            if (readerEmpleado.Read())
                            {
                                txtNombre.Text = readerEmpleado["nombre"].ToString();
                                txtApellido.Text = readerEmpleado["apellido"].ToString();

                                if (readerEmpleado["fecha_nacimiento"] != DBNull.Value)
                                {
                                    dtNacimiento.Value = (DateTime)readerEmpleado["fecha_nacimiento"];
                                }
                                else
                                {
                                    dtNacimiento.Value = DateTime.Now; // Valor predeterminado
                                }

                                txtDireccion.Text = readerEmpleado["direccion"].ToString();
                                txtTelefono.Text = readerEmpleado["telefono"].ToString();
                                txtCorreo.Text = readerEmpleado["correo_electronico"].ToString();

                                // Guardar el id_rol del empleado para la siguiente consulta
                                RolID = Convert.ToInt32(readerEmpleado["id_rol"]);
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el empleado.");
                                return; // Salir si no se encuentra el empleado
                            }
                        }
                    }

                    // Obtener los datos del rol
                    using (var commandRol = new MySqlCommand(queryRol, conexion))
                    {
                        commandRol.Parameters.AddWithValue("@RolID", RolID);

                        using (var readerRol = commandRol.ExecuteReader())
                        {
                            if (readerRol.Read())
                            {
                                cbxRol.Text = readerRol["nombre_rol"].ToString();
                                txtDescripcion.Text = readerRol["descripcion"].ToString();
                            }
                            else
                            {
                                MessageBox.Show("No se encontró el rol asociado al empleado.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al conectar a la base de datos: " + ex.Message);
                }
            }

        }

        //Cargar
        private void FormEmpleadoEditarEmpleado_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        //Cerrar
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Editar
        private void button2_Click(object sender, EventArgs e)
        {

        }

    }
}
