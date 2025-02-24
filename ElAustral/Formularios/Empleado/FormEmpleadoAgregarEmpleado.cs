using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ElAustral.Formularios.Empleado
{
    public partial class FormEmpleadoAgregarEmpleado : System.Windows.Forms.Form
    {
        public FormEmpleadoAgregarEmpleado()
        {
            InitializeComponent();
            ApplyStyles(); // Aplicar estilos al inicializar el formulario
        }

        // Método para aplicar estilos
        private void ApplyStyles()
        {
            // Estilo del formulario
            this.BackColor = Color.FromArgb(30, 30, 30); // Fondo negro
            this.ForeColor = Color.White; // Texto blanco
            this.Font = new Font("Segoe UI", 10); // Fuente moderna

            // Recorrer todos los controles del formulario
            ApplyControlStyles(this);
        }

        private void ApplyControlStyles(Control parentControl)
        {
            foreach (Control control in parentControl.Controls)
            {
                // Aplicar estilos a los controles
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.BackColor = Color.FromArgb(45, 45, 45); // Fondo gris oscuro
                    textBox.ForeColor = Color.White; // Texto blanco
                    textBox.BorderStyle = BorderStyle.FixedSingle; // Borde fijo
                    textBox.Font = new Font("Segoe UI", 10); // Fuente moderna
                }
                else if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    comboBox.BackColor = Color.FromArgb(45, 45, 45); // Fondo gris oscuro
                    comboBox.ForeColor = Color.White; // Texto blanco
                    comboBox.Font = new Font("Segoe UI", 10); // Fuente moderna
                }
                else if (control is DateTimePicker)
                {
                    DateTimePicker dateTimePicker = (DateTimePicker)control;
                    dateTimePicker.BackColor = Color.FromArgb(45, 45, 45); // Fondo gris oscuro
                    dateTimePicker.ForeColor = Color.White; // Texto blanco
                    dateTimePicker.Font = new Font("Segoe UI", 10); // Fuente moderna
                    dateTimePicker.CalendarMonthBackground = Color.FromArgb(45, 45, 45); // Fondo del calendario
                    dateTimePicker.CalendarTitleBackColor = Color.FromArgb(45, 45, 45); // Fondo del título del calendario
                    dateTimePicker.CalendarTitleForeColor = Color.White; // Texto del título del calendario
                    dateTimePicker.CalendarForeColor = Color.White; // Texto del calendario
                }
                else if (control is Button)
                {
                    Button button = (Button)control;
                    button.BackColor = Color.FromArgb(255, 111, 0); // Fondo naranja
                    button.ForeColor = Color.White; // Texto blanco
                    button.FlatStyle = FlatStyle.Flat; // Estilo plano
                    button.FlatAppearance.BorderSize = 0; // Sin borde
                    button.Font = new Font("Segoe UI", 10, FontStyle.Bold); // Fuente moderna
                    button.UseVisualStyleBackColor = false; // Deshabilitar estilos visuales predeterminados
                }
                else if (control is Label)
                {
                    control.ForeColor = Color.White; // Texto blanco
                    control.Font = new Font("Segoe UI", 10); // Fuente moderna
                }
                else if (control is GroupBox || control is Panel)
                {
                    control.BackColor = Color.FromArgb(30, 30, 30); // Fondo negro
                    control.ForeColor = Color.White; // Texto blanco
                    control.Font = new Font("Segoe UI", 10); // Fuente moderna
                }

                // Si el control tiene controles hijos, recorrerlos también
                if (control.HasChildren)
                {
                    ApplyControlStyles(control); // Llamada recursiva
                }
            }
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
            // Puedes agregar lógica adicional al cargar el formulario
        }

        private void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }
    }
}