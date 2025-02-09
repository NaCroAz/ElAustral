using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Austral
{
    public partial class caja : Form
    {
        public caja()
        {
            InitializeComponent();
        }



        private void caja_Load(object sender, EventArgs e)
        {

        }

        //MESA 1
        private void button1_Click(object sender, EventArgs e)
        {
            FormMesa1 formMesa1 = new FormMesa1();
            formMesa1.Show();
            this.Hide();
        }

        //MESA 2
        private void button2_Click(object sender, EventArgs e)
        {
            FormMesa2 formMesa2 = new FormMesa2();
            formMesa2.Show();
            this.Hide();
        }

        //MESA 3
        private void button4_Click(object sender, EventArgs e)
        {
            FormMesa3 formMesa3 = new FormMesa3();
            formMesa3.Show();
            this.Hide();
        }

        //MESA 4
        private void button3_Click(object sender, EventArgs e)
        {
            FormMesa4 formMesa4 = new FormMesa4();
            formMesa4.Show();
            this.Hide();
        }











        //VENTAS DE LA FECHA
        private void buttonVentasHoy_Click(object sender, EventArgs e)
        {
            FormVentasDeLaFecha formVentasDeLaFechas = new FormVentasDeLaFecha();
            formVentasDeLaFechas.Show();
            this.Hide();
        }
    }
}
    