using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Super_Sistema
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmProductos formularioProductos = new frmProductos();
            formularioProductos.Owner = this;
            formularioProductos.Show();
            this.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close(); //Añadir confirmación de cierre
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            mostrarMensajeConstruccion();
        }

        private void mostrarMensajeConstruccion()
        {
            MessageBox.Show("Sección aún no disponible.", "En construcción", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            mostrarMensajeConstruccion();
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            mostrarMensajeConstruccion();
        }
    }
}
