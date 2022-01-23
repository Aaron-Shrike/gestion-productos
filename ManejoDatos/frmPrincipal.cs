using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManejoDatos
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuCategorias_Click(object sender, EventArgs e)
        {
            //patron de diseño : singleton
            frmCategoria frm = frmCategoria.Crear(this);

            frm.Show();
            frm.BringToFront();
        }

        private void mnuProductos_Click(object sender, EventArgs e)
        {
            frmProducto frm = frmProducto.Crear(this);

            frm.Show();
            frm.BringToFront();
        }

        private void mnuListadoProductos_Click(object sender, EventArgs e)
        {
            frmListadoProductos frm = frmListadoProductos.Crear(this);

            frm.Show();
            frm.BringToFront();
        }

            //frmCategorias frm = new frmCategorias();

            //frm.MdiParent = this;
            //frm.WindowState = FormWindowState.Maximized;
            //frm.Show();


    }
}
