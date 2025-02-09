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

namespace ElAustral
{
    public partial class FormMenuPrincipal : Form
    {
        public FormMenuPrincipal()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormLogin formLogin = new FormLogin();
            MessageBox.Show("Se cerro sesion");
            formLogin.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormInventario formInventario = new FormInventario();
            formInventario.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FormCaja formCaja = new FormCaja();
            formCaja.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormEmpleado formEmpleado = new FormEmpleado();
            formEmpleado.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormReporte formReporte = new FormReporte();
            formReporte.Show();
            this.Hide();
        }

        private void FormMenuPrincipal_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
