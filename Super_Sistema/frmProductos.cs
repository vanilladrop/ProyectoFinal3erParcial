using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Super_Sistema
{
    public partial class frmProductos : Form
    {
        private readonly static string datosConexion = "server=localhost; database=ProyectoBDTienda; UID=root; password=; ConvertZeroDateTime=true";
        readonly MySqlConnection conexion = new MySqlConnection(datosConexion);

        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            llenarDGV();
        }

        private void llenarDGV()
        {
            string query = "SELECT * FROM productos";
            conexion.Open();
            DataTable dt = new DataTable();
            MySqlCommand comando = new MySqlCommand(query, conexion);
            MySqlDataAdapter da = new MySqlDataAdapter(comando);
            da.Fill(dt);
            conexion.Close();

            dgvProductos.DataSource = dt;
        }

        private void limpiar()
        {
            cboIdProducto.SelectedItem = 0;
            cboIdProducto.Text = "";
            cboNombre.SelectedItem = 0;
            cboIdProducto.Text = "";
            nudCostoMayoreo.Value = 0;
            nudPrecioVenta.Value = 0;
            nudInventario.Value = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            cerrarPadre();
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();
        }

        private void frmProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            cerrarPadre();
        }

        private void cerrarPadre()
        {
            Owner.Dispose();
        }

        private void btnClientes_Click(object sender, EventArgs e)
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
