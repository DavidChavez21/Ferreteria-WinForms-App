﻿using System;
using System.Data;
using System.Windows.Forms;
using Dominio;
using System.Media;

namespace Presentacion
{
    public partial class CompraProductos : Form
    {

        DOM_Empleados empleados = new DOM_Empleados();
        DOM_Facturacion facturacion = new DOM_Facturacion();
        DOM_Validacion LetraNum = new DOM_Validacion();
        private DOM_Inventario productos = new DOM_Inventario();
        DOM_proveedor proveedor = new DOM_proveedor();
        double subtotal;
        public CompraProductos()
        {
            InitializeComponent();
            subtotal = 0;
        }

        private void CompraProductos_Load(object sender, EventArgs e)
        {

         var tab = new BindingSource();
         tab.DataSource = productos.mostrar_inventario();
         dgvProducto.DataSource = tab;
            dgvProducto.Columns["Id Marca"].Visible = false;
            dgvProducto.Columns["Id Categoria"].Visible = false;
            dgvProducto.Rows[1].Selected = true;
            dgvProducto.CurrentRow.Selected = true;
            dgvProducto.Refresh();
        }



        private void btnAgregar_Click(object sender, EventArgs e)
        {

            btnEliminar.Enabled = false;
            btnModificar.Enabled = false;
            btnSiguiente.Visible = true;

            Lim_ha encender = new Lim_ha();
            encender.Encender(this);

            Lim_ha Limpiar = new Lim_ha();
            Limpiar.Limpiar(this);

            DataTable table = new DataTable();
            table = productos.mostrar_inventario();
            dgvProducto.DataSource = table;

            txtnombre.Enabled = true;
            txtmarca.Enabled = true;
            txtPrecio.Enabled = true;
            txtCosto.Enabled = true;
            txtCantidad.Enabled = true;
            dgvListaProductos.Rows.Add(dgvProducto.SelectedRows[0].Cells[0].Value, dgvProducto.SelectedRows[0].Cells[1].Value,  dgvProducto.SelectedRows[0].Cells[8].Value,txtCantidad.Text,dgvProducto.SelectedRows[0].Cells[6].Value);
            subtotal += double.Parse(txtCosto.Text) * int.Parse(txtCantidad.Text);
            txtSubtotal.Text = "" + subtotal;

        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            btnEliminar.Enabled = false;
            btnAgregar.Enabled = false;
            btnSiguiente.Visible = true;

            Lim_ha encender = new Lim_ha();
            encender.Encender(this);

            Lim_ha Limpiar = new Lim_ha();
            Limpiar.Limpiar(this);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            btnAgregar.Enabled = false;
            btnModificar.Enabled = false;
            btnSiguiente.Visible = true;
            dgvListaProductos.Rows.RemoveAt(dgvListaProductos.SelectedRows[0].Index);
            subtotal -= (int)dgvListaProductos.SelectedRows[0].Cells[3].Value * (double)dgvListaProductos.SelectedRows[0].Cells[2].Value;
            txtSubtotal.Text = "" + subtotal;
        }


        private void dgvProducto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvProducto.CurrentRow.Selected = true;
            txtnombre.Text = dgvProducto.CurrentRow.Cells[1].Value.ToString();
            txtmarca.Text = dgvProducto.CurrentRow.Cells[2].Value.ToString();
            txtPrecio.Text = dgvProducto.CurrentRow.Cells[3].Value.ToString();
            txtCosto.Text = dgvProducto.CurrentRow.Cells[4].Value.ToString();
            txtCantidad.Text = dgvProducto.CurrentRow.Cells[5].Value.ToString();

            btnAgregar.Enabled = true;
            btnModificar.Enabled = true;
            btnEliminar.Enabled = true;

            txtnombre.Enabled = true;
            txtmarca.Enabled = true;
            txtPrecio.Enabled = true;
            txtCosto.Enabled = true;
            txtCantidad.Enabled = true;

        }


        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            facturacion.proveedor = proveedor;
            facturacion.InsertarFacturaCompra("0",txtSubtotal.Text,"0") ;
            foreach (DataRow row in dgvListaProductos.Rows)
            {
                productos.Id_producto = row.Field<int>("id_producto");
                productos.Id_marca = row.Field<int>("id_marca");
                productos.Nom_producto = row.Field<string>("nom_producto");
                productos.Cantidad_unidad = row.Field<string>("Cantidad_por_Unidad");
                productos.Precio_actual = row.Field<double>("precio_actual");
                productos.Id_categoria = row.Field<int>("id_categoria");
                facturacion.insertarDetalleCompra(row.Field<string>("cantidad"), productos);
            }
        }
        private void txtnombrepor_TextChanged(object sender, EventArgs e)
        {
            var bd = (BindingSource)dgvProducto.DataSource;
            var dt = (DataTable)bd.DataSource;
            dt.DefaultView.RowFilter = string.Format("[Nombre Producto] LIKE '%{0}%'", txtnombreprod.Text);
            dgvProducto.Refresh();

            if (dt.DefaultView.Count < 1)
            {
                SystemSounds.Exclamation.Play();
                ToolTip tt = new ToolTip();
                tt.Show("No se encontro parametros", this.txtnombreprod, 0, 25, 3000);
            }
        }

        private void txtcodigopro_TextChanged(object sender, EventArgs e)
        {
            var bd = (BindingSource)dgvProducto.DataSource;
            var dt = (DataTable)bd.DataSource;
            if (txtcodigoprod.Text != "")

                dt.DefaultView.RowFilter = string.Format("[Id Producto] = {0}", int.Parse(txtcodigoprod.Text));

            else
            {
                dt.DefaultView.RowFilter = null;
            }
            dgvProducto.Refresh();

            if (dt.DefaultView.Count < 1)
            {
                SystemSounds.Exclamation.Play();
                ToolTip tt = new ToolTip();
                tt.Show("No se encontro parametros", this.txtcodigoprod, 0, 25, 3000);
            }
        }

        private void txtcodigoprod_KeyPress(object sender, KeyPressEventArgs e)
        {
            LetraNum.SoloNumeros(e);
        }

        private void txtnombreprod_KeyPress(object sender, KeyPressEventArgs e)
        {
            LetraNum.SoloLetras(e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
