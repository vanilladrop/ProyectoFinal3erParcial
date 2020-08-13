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

        enum operacionCRUD
        {
            Crear,
            Leer,
            Actualizar,
            Eliminar,
        }

        operacionCRUD accionBotonOK = operacionCRUD.Leer;

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
            txtIdProducto.Text = "";
            txtNombre.Text = "";
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            gpbDatos.BackColor = Color.Red;
            lblEstado.Text = "ELIMINANDO UN PRODUCTO";
            accionBotonOK = operacionCRUD.Eliminar;
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gpbDatos.BackColor = Color.Green;
            gpbDatos.Enabled = true;
            gpbBotones.Enabled = true;
            lblEstado.Text = "AÑADIENDO UN PRODUCTO";
            accionBotonOK = operacionCRUD.Crear;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            gpbDatos.BackColor = Color.Cyan;
            lblEstado.Text = "MODIFICANDO UN PRODUCTO";
            accionBotonOK = operacionCRUD.Actualizar;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (accionBotonOK)
            {
                case operacionCRUD.Crear:
                    MessageBox.Show("Sí jala el \"crear\"", "Crear", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case operacionCRUD.Leer:
                    MessageBox.Show("Sí jala el \"Leer\"", "Leer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case operacionCRUD.Actualizar:
                    MessageBox.Show("Sí jala el \"Actualizar\"", "Actualizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case operacionCRUD.Eliminar:
                    MessageBox.Show("Sí jala el \"eLIMINAR\"", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                default:
                    MessageBox.Show("Error en acción de botón OK.\nCierre el programa y vuélvalo a abrir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }
    }
}
