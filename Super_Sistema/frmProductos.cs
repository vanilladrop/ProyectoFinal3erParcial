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
            Ninguno
        }

        operacionCRUD accionBotonOK = operacionCRUD.Ninguno;

        public frmProductos()
        {
            InitializeComponent();
        }

        private void frmProductos_Load(object sender, EventArgs e)
        {
            llenarDGV();
            gpbBusqueda.BackColor = System.Drawing.ColorTranslator.FromHtml("#D4CF79");
            llenarCBOs();
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

        private void limpiarBusqueda()
        {
            cboCodigoBarrasBusqueda.SelectedIndex = -1;
            cboNombreProductoBusqueda.SelectedIndex = -1;
            nudCostoMayoreoBusqueda.Value = 0;
            nudPrecioVentaBusqueda.Value = 0;
            nudInventarioBusqueda.Value = 0;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (accionBotonOK != operacionCRUD.Ninguno)
            {
                if (accionBotonOK != operacionCRUD.Leer)
                {
                    DialogResult resultado = MessageBox.Show("¿Seguro que desea salir? Perderá la información no guardada", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (resultado == DialogResult.Yes)
                    {
                        cerrarPadre();
                    }
                }
            }
            else
            {
                cerrarPadre();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            switch (accionBotonOK)
            {
                case operacionCRUD.Leer:
                    limpiarBusqueda();
                    break;
                default:
                    limpiar();
                    break;
            }
        }

        private void frmProductos_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (accionBotonOK != operacionCRUD.Ninguno)
            {
                if (accionBotonOK != operacionCRUD.Leer && accionBotonOK != operacionCRUD.Eliminar)
                {
                    DialogResult resultado = MessageBox.Show("¿Seguro que desea salir? Perderá la información no guardada", "Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (resultado == DialogResult.Yes)
                    {
                        cerrarPadre();
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    cerrarPadre();
                }
            }
            else
            {
                cerrarPadre();
            }
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
            gpbDatos.BackColor = System.Drawing.ColorTranslator.FromHtml("#CCA19B");
            lblEstado.Text = "ELIMINANDO UN PRODUCTO";
            accionBotonOK = operacionCRUD.Eliminar;
            activarControles();
            limpiar();
            esconderGpbBusqueda();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            gpbDatos.BackColor = System.Drawing.ColorTranslator.FromHtml("#9ACC97");
            activarControles();
            lblEstado.Text = "AÑADIENDO UN PRODUCTO";
            accionBotonOK = operacionCRUD.Crear;
            limpiar();
            esconderGpbBusqueda();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            gpbDatos.BackColor = System.Drawing.ColorTranslator.FromHtml("#9FA2CC");
            lblEstado.Text = "MODIFICANDO UN PRODUCTO";
            accionBotonOK = operacionCRUD.Actualizar;
            activarControles();
            limpiar();
            esconderGpbBusqueda();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            switch (accionBotonOK)
            {
                case operacionCRUD.Crear:
                    insertarNuevoProducto();
                    break;
                case operacionCRUD.Leer:
                    buscarProducto();
                    break;
                case operacionCRUD.Actualizar:
                    modificarDatosProducto();
                    break;
                case operacionCRUD.Eliminar:
                    eliminarProducto();
                    break;
                default:
                    MessageBox.Show("Error en acción de botón OK.\nCierre el programa y vuélvalo a abrir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void insertarNuevoProducto()
        {
            string insercion = "INSERT INTO productos (idProducto, nombre, costoMayoreo, precioVenta, enInventario) VALUES (@idProducto, @nombre, @costoMayoreo, @precioVenta, @enInventario)";
            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(insercion, conexion);
                comando.Parameters.AddWithValue("@idProducto", txtIdProducto.Text);
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@costoMayoreo", nudCostoMayoreo.Value);
                comando.Parameters.AddWithValue("@precioVenta", nudPrecioVenta.Value);
                comando.Parameters.AddWithValue("@enInventario", nudInventario.Value);
                comando.ExecuteNonQuery();
                conexion.Close();
                MessageBox.Show("Producto \"" + txtNombre.Text + "\" con código de barras \"" + txtIdProducto.Text + "\" añadido al inventario.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                llenarDGV();
                limpiar();
            }
            catch (MySqlException excepcionSQL)
            {
                MessageBox.Show("Fallo al almacenar datos. Error \"" + excepcionSQL + "\".", "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexion.Close();
            }
        }

        private void buscarProducto()
        {
            string busqueda = "SELECT * FROM productos ";
            if (rdbCodigoDeBarras.Checked)
            {
                busqueda += "WHERE idProducto = @idProducto";
            }
            else if (rdbNombreProducto.Checked)
            {
                busqueda += "WHERE nombre = @nombre";
            }
            else if (rdbCostoMayoreo.Checked)
            {
                busqueda += "WHERE costoMayoreo = @costoMayoreo";
            }
            else if (rdbPrecioVenta.Checked)
            {
                busqueda += "WHERE precioVenta = @precioVenta";
            }
            else if (rdbInventario.Checked)
            {
                busqueda += "WHERE enInventario = @enInventario";
            }

            try
            {
                conexion.Open();
                DataTable dt = new DataTable();
                MySqlCommand comando = new MySqlCommand(busqueda, conexion);
                comando.Parameters.AddWithValue("@idProducto", cboCodigoBarrasBusqueda.Text);
                comando.Parameters.AddWithValue("@nombre", cboNombreProductoBusqueda.Text);
                comando.Parameters.AddWithValue("@costoMayoreo", float.Parse(nudCostoMayoreoBusqueda.Text));
                comando.Parameters.AddWithValue("@precioVenta", float.Parse(nudPrecioVenta.Text));
                comando.Parameters.AddWithValue("@enInventario", int.Parse(nudInventario.Text));
                MySqlDataAdapter da = new MySqlDataAdapter(comando);
                da.Fill(dt);
                conexion.Close();
                dgvProductos.DataSource = dt;
            }
            catch (MySqlException excepcionSQL)
            {
                MessageBox.Show("Comando SQL: " + busqueda + "\nFallo al buscar datos. Error \"" + excepcionSQL + "\".", "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexion.Close();
            }
        }

        private void modificarDatosProducto()
        {
            string modificacion = "UPDATE productos SET idProducto = @idProducto, nombre = @nombre, costoMayoreo = @costoMayoreo, precioVenta = @precioVenta, enInventario = @enInventario WHERE idProducto = @idProducto";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(modificacion, conexion);
                comando.Parameters.AddWithValue("@idProducto", txtIdProducto.Text);
                comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
                comando.Parameters.AddWithValue("@costoMayoreo", nudCostoMayoreo.Value);
                comando.Parameters.AddWithValue("@precioVenta", nudPrecioVenta.Value);
                comando.Parameters.AddWithValue("@enInventario", nudInventario.Value);
                DialogResult resultado = MessageBox.Show("El producto \"" + dgvProductos.CurrentRow.Cells[1].Value.ToString() + "\" y código de barras \"" + dgvProductos.CurrentRow.Cells[0].Value.ToString() + "\" a punto de ser modificado.\n¿Deseas continuar?.", "Confirmación de actualización", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("Producto \"" + txtNombre.Text + "\" con código de barras \"" + txtIdProducto.Text + "\" modificado exitosamente.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    desactivarControles();
                    llenarDGV();
                }
                conexion.Close();
            }
            catch (MySqlException excepcionSQL)
            {
                MessageBox.Show("Fallo al actualizar datos. Error \"" + excepcionSQL + "\".", "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexion.Close();
            }
        }

        private void eliminarProducto()
        {
            string eliminacion = "DELETE FROM productos WHERE idProducto = @idProducto";

            try
            {
                conexion.Open();
                MySqlCommand comando = new MySqlCommand(eliminacion, conexion);
                comando.Parameters.AddWithValue("@idProducto", dgvProductos.CurrentRow.Cells[0].Value.ToString());
                DialogResult resultado = MessageBox.Show("Producto \"" + txtNombre.Text + "\", con código de barras \"" + txtIdProducto.Text + "\", y " + nudInventario.Value + " está a punto de ser removido del inventario.\n¿Deseas continuar?.", "Confirmación de eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (resultado == DialogResult.Yes)
                {
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    MessageBox.Show("Producto eliminado de manera exitosa.", "Operación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    limpiar();
                    desactivarControles();
                    llenarDGV();
                }
                conexion.Close();
            }
            catch (MySqlException excepcionSQL)
            {
                MessageBox.Show("Fallo al almacenar datos. Error \"" + excepcionSQL + "\".", "Error en la operación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conexion.Close();
            }
        }

        private void llenarCampos()
        {
            DataGridViewRow registroActual = dgvProductos.CurrentRow;
            txtIdProducto.Text = registroActual.Cells[0].Value.ToString();
            txtNombre.Text = registroActual.Cells[1].Value.ToString();

            if (decimal.TryParse(registroActual.Cells[2].Value.ToString(), out decimal costoMayoreo))
            {
                costoMayoreo = decimal.Parse(registroActual.Cells[2].Value.ToString());
            }
            else
            {
                costoMayoreo = 0;
            }
            nudCostoMayoreo.Value = costoMayoreo;


            if (decimal.TryParse(registroActual.Cells[3].Value.ToString(), out decimal precioVenta))
            {
                precioVenta = decimal.Parse(registroActual.Cells[3].Value.ToString());
            }
            else
            {
                precioVenta = 0;
            }
            nudPrecioVenta.Value = precioVenta;


            if (int.TryParse(registroActual.Cells[4].Value.ToString(), out int enInventario))
            {
                enInventario = int.Parse(registroActual.Cells[4].Value.ToString());
            }
            else
            {
                enInventario = 0;
            }
            nudInventario.Value = enInventario;

            if (registroActual.Cells[0].Value.ToString() == "")
            {
                desactivarControles();
            }
            else
            {
                activarControles();
            }
        }

        private void activarControles()
        {
            gpbBotones.Enabled = true;
            if(accionBotonOK == operacionCRUD.Eliminar)
            {
                btnLimpiar.Enabled = false;
            }
            else
            {
                btnLimpiar.Enabled = true;
                gpbDatos.Enabled = true;
            }
        }

        private void desactivarControles()
        {
            limpiar();
            gpbDatos.Enabled = false;
            gpbBotones.Enabled = false;
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(accionBotonOK == operacionCRUD.Actualizar || accionBotonOK == operacionCRUD.Eliminar)
            {
                llenarCampos();
            }
        }

        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            if (accionBotonOK == operacionCRUD.Actualizar || accionBotonOK == operacionCRUD.Eliminar)
            {
                llenarCampos();
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            gpbDatos.SendToBack();
            limpiar();
            limpiarBusqueda();
            gpbBusqueda.Enabled = true;
            activarControles();
            accionBotonOK = operacionCRUD.Leer;
        }

        private void esconderGpbBusqueda()
        {
            gpbBusqueda.SendToBack();
            gpbBusqueda.Enabled = false;
        }

        private void llenarCBOs()
        {
            string queryIdProducto = "SELECT DISTINCT idProducto FROM productos",
                   queryNombre = "SELECT DISTINCT nombre FROM productos",
                   queryCostoMayoreo = "SELECT DISTINCT costoMayoreo FROM productos",
                   queryPrecioVenta = "SELECT DISTINCT precioVenta FROM productos",
                   queryInventario = "SELECT DISTINCT enInventario FROM productos";

            DataTable dtIdProducto = new DataTable(),
                      dtNombre = new DataTable(),
                      dtCostoMayoreo = new DataTable(),
                      dtPrecioVenta = new DataTable(),
                      dtInventario = new DataTable();

            MySqlCommand comandoIdProducto = new MySqlCommand(queryIdProducto, conexion),
                         comandoNombre = new MySqlCommand(queryNombre, conexion),
                         comandoCostoMayoreo = new MySqlCommand(queryCostoMayoreo, conexion),
                         comandoPrecioVenta = new MySqlCommand(queryPrecioVenta, conexion),
                         comandoInventario = new MySqlCommand(queryInventario, conexion);

            MySqlDataAdapter daIdProducto = new MySqlDataAdapter(comandoIdProducto),
                             daNombre = new MySqlDataAdapter(comandoNombre),
                             daCostoMayoreo = new MySqlDataAdapter(comandoCostoMayoreo),
                             daPrecioVenta = new MySqlDataAdapter(comandoPrecioVenta),
                             daInventario = new MySqlDataAdapter(comandoInventario);

            daIdProducto.Fill(dtIdProducto);
            daNombre.Fill(dtNombre);
            daCostoMayoreo.Fill(dtCostoMayoreo);
            daPrecioVenta.Fill(dtPrecioVenta);
            daInventario.Fill(dtInventario);

            int filasIdProducto = dtIdProducto.Rows.Count,
                filasNombre = dtNombre.Rows.Count,
                filasCostoMayoreo = dtCostoMayoreo.Rows.Count,
                filasPrecioVenta = dtPrecioVenta.Rows.Count,
                filasInventario = dtInventario.Rows.Count,
                i = 0;

            while (i < filasIdProducto)
            {
                cboCodigoBarrasBusqueda.Items.Add(dtIdProducto.Rows[i].Field<string>(0));
                i++;
            }
            i = 0;
            while (i < filasNombre)
            {
                cboNombreProductoBusqueda.Items.Add(dtNombre.Rows[i].Field<string>(0));
                i++;
            }
            i = 0;
            /*while (i < filasCostoMayoreo)
            {
                cboCostoMayoreoBusqueda.Items.Add(dtCostoMayoreo.Rows[i].Field<decimal>(0));
                i++;
            }
            i = 0;
            while (i < filasPrecioVenta)
            {
                cboPrecioVentaBusqueda.Items.Add(dtPrecioVenta.Rows[i].Field<decimal>(0));
                i++;
            }
            i = 0;
            while (i < filasInventario)
            {
                cboInventarioBusqueda.Items.Add(dtInventario.Rows[i].Field<int>(0));
                i++;
            }*/
        }

        private void rdbCodigoDeBarras_CheckedChanged(object sender, EventArgs e)
        {
            cboCodigoBarrasBusqueda.Enabled = true;
            cboNombreProductoBusqueda.Enabled = false;
            nudCostoMayoreoBusqueda.Enabled = false;
            nudPrecioVentaBusqueda.Enabled = false;
            nudInventarioBusqueda.Enabled = false;
        }

        private void rdbNombreProducto_CheckedChanged(object sender, EventArgs e)
        {
            cboCodigoBarrasBusqueda.Enabled = false;
            cboNombreProductoBusqueda.Enabled = true;
            nudCostoMayoreoBusqueda.Enabled = false;
            nudPrecioVentaBusqueda.Enabled = false;
            nudInventarioBusqueda.Enabled = false;
        }

        private void rdbCostoMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            cboCodigoBarrasBusqueda.Enabled = false;
            cboNombreProductoBusqueda.Enabled = false;
            nudCostoMayoreoBusqueda.Enabled = true;
            nudPrecioVentaBusqueda.Enabled = false;
            nudInventarioBusqueda.Enabled = false;
        }

        private void rdbPrecioVenta_CheckedChanged(object sender, EventArgs e)
        {
            cboCodigoBarrasBusqueda.Enabled = false;
            cboNombreProductoBusqueda.Enabled = false;
            nudCostoMayoreoBusqueda.Enabled = false;
            nudPrecioVentaBusqueda.Enabled = true;
            nudInventarioBusqueda.Enabled = false;
        }

        private void rdbInventario_CheckedChanged(object sender, EventArgs e)
        {
            cboCodigoBarrasBusqueda.Enabled = false;
            cboNombreProductoBusqueda.Enabled = false;
            nudCostoMayoreoBusqueda.Enabled = false;
            nudPrecioVentaBusqueda.Enabled = false;
            nudInventarioBusqueda.Enabled = true;
        }
    }
}
