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
    public partial class frmProducto : Form
    {
        private Producto Actual;

        #region "Singleton"

        private static frmProducto frm;

        private frmProducto()
        {
            InitializeComponent();
        }

        public static frmProducto Crear(Form padre)
        {
            if (frmProducto.frm == null)
            {
                frmProducto.frm = new frmProducto();
                frm.MdiParent = padre;
                frm.WindowState = FormWindowState.Maximized;
            }

            return frmProducto.frm;
        }

        private void frmProducto_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmProducto.frm = null;
        }

        #endregion

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.ActivarControles(true);
            this.LimpiarControles();
            this.Actual = null;
        }

        private void LimpiarControles()
        {
            this.cboCategoria.SelectedIndex = -1;
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.nudPrecio.Value = this.nudPrecio.Minimum;
            this.nudStock.Value = this.nudStock.Minimum;
            this.chkVigente.Checked = true;
            this.chkVigente.Enabled = false;
        }

        private void ActivarControles(bool estado)
        {
            this.gbEntidad.Enabled = estado;
            this.gbListado.Enabled = !estado;
            if (estado == true)
            {
                this.cboCategoria.Focus();
            }
            else
            {
                this.txtProducto.Focus();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.ActivarControles(false);
        }

        private void frmProducto_Load(object sender, EventArgs e)
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
                //this.cboCategoria.ValueMember = "Codigo";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Producto prod;

            if (this.ValidateChildren() == true)
            {
                if (this.Actual == null)
                {
                    prod = new Producto();
                    Program.Productos.Add(prod);
                    prod.Codigo = Program.Productos.Count;
                }
                else
                {
                    prod = this.Actual;
                }
                //falta categoria
                prod.Categoria = (Categoria)this.cboCategoria.SelectedItem ;
                prod.Nombre = this.txtNombre.Text;
                prod.Descripcion = this.txtDescripcion.Text;
                prod.Precio = (double)this.nudPrecio.Value;
                prod.Stock = (int)this.nudStock.Value;
                prod.Vigente = this.chkVigente.Checked;
                this.ActivarControles(false);
                this.btnListar.PerformClick();
                this.AutoValidate = AutoValidate.Disable;
            }
            else
            {
                this.AutoValidate = AutoValidate.EnableAllowFocusChange;
            }
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            List<Producto> productos;

            productos = this.Filtrar(this.txtProducto.Text);
            this.dgvListado.DataSource = null;
            if (productos.Count > 0){
                this.dgvListado.AutoGenerateColumns = false;
                this.dgvListado.DataSource = productos;
            }
        }
        private List<Producto> Filtrar(string nombreBuscar)
        {
            List<Producto> productos ;

            if (string.IsNullOrEmpty(nombreBuscar) == true)
            {
                return Program.Productos;
            }

            productos = new List<Producto>();
            foreach (var prod in Program.Productos){
                if ( prod.Nombre.Length >= nombreBuscar.Length 
                        && prod.Nombre.Substring(0, nombreBuscar.Length).Equals(nombreBuscar) == true){
                    
                    productos.Add(prod);
                }
            }
            return productos;
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (this.dgvListado.CurrentRow != null)
            {
                this.Actual = (Producto)(this.dgvListado.CurrentRow.DataBoundItem);
                this.PresentarDatos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un producto", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dgvListado.Focus();
            }
        }

        private void PresentarDatos()
        {
            this.cboCategoria.SelectedItem = this.Actual.Categoria;
            this.txtNombre.Text = this.Actual.Nombre;
            this.txtDescripcion.Text = this.Actual.Descripcion;
            this.nudPrecio.Value = (decimal) this.Actual.Precio;
            this.nudStock.Value = this.Actual.Stock;
            this.chkVigente.Checked = this.Actual.Vigente;
            this.chkVigente.Enabled = true;
            this.ActivarControles(true);
        }

        private void cboCategoria_Validating(object sender, CancelEventArgs e)
        {
            if (this.cboCategoria.SelectedIndex > -1)
            {
                this.errError.SetError(this.cboCategoria, "");
            }
            else
            {
                this.errError.SetError(this.cboCategoria, "Debe seleccionar una categoría");
                e.Cancel = true;
            }
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if( string.IsNullOrEmpty( this.txtNombre.Text ) == false)
            {
                this.errError.SetError(this.txtNombre, "");
            }
            else
            {
                this.errError.SetError(this.txtNombre, "Debe indicar el nombre");
                e.Cancel = true;
            }
        }

    }
}
