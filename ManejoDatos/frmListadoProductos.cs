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
    public partial class frmListadoProductos : Form
    {

        #region "Singleton"

        private static frmListadoProductos frm;

        private frmListadoProductos()
        {
            InitializeComponent();
        }

        public static frmListadoProductos Crear(Form padre)
        {
            if (frmListadoProductos.frm == null)
            {
                frmListadoProductos.frm = new frmListadoProductos();
                frm.MdiParent = padre;
                frm.WindowState = FormWindowState.Maximized;
            }

            return frmListadoProductos.frm;
        }

        private void frmListadoProductos_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmListadoProductos.frm = null;
        }

        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListadoProductos_Load(object sender, EventArgs e)
        {
            this.CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            this.CargarCategorias();
        }

        private void CargarCategorias()
        {
            this.cboCategoria.DataSource = null;
            if (Program.Categorias.Count > 0)
            {
                this.cboCategoria.DataSource = Program.Categorias;
                this.cboCategoria.DisplayMember = "Nombre";
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            List<Producto> productos;

            productos = this.Filtrar(this.cboCategoria.Text );
            this.dgvListado.DataSource = null;
            if (productos.Count > 0)
            {
                this.dgvListado.AutoGenerateColumns = false;
                this.dgvListado.DataSource = productos;
            }
        }
        private List<Producto> Filtrar(string nombreCategoria)
        {
            List<Producto> productos;

            productos = new List<Producto>();
            foreach (var prod in Program.Productos)
            {
                if ( prod.Categoria.Nombre .Equals( nombreCategoria) == true)
                {
                    productos.Add(prod);
                }
            }
            return productos;
        }

    }
}
