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
    public partial class frmCategoria : Form
    {

        #region "Singleton"

        private static frmCategoria frm;

        private frmCategoria()
        {
            InitializeComponent();
        }

        public static frmCategoria Crear(Form padre)
        {
            if (frmCategoria.frm == null)
            {
                frmCategoria.frm = new frmCategoria();
                frmCategoria.frm.MdiParent = padre;
                frmCategoria.frm.WindowState = FormWindowState.Maximized;
            }

            return frmCategoria.frm;
        }

        private void frmCategorias_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmCategoria.frm = null;
        }

        #endregion

        private Categoria Actual;

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.ActivarControles(true);
            this.LimpiarControles();
            this.Actual = null;
        }

        private void ActivarControles(bool p)
        {
            this.gbEntidad.Enabled = p;
            this.gbListado.Enabled = !p;
            if (p == true)
            {
                this.txtNombre.Focus();
            }
            else
            {
                this.btnListar.Focus();
            }

        }

        private void LimpiarControles()
        {
            this.txtNombre.Text = "";
            this.txtDescripcion.Text = "";
            this.chkVigente.Checked = true;
            this.chkVigente.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.ActivarControles(false);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Categoria cat;

            if (this.ValidateChildren() == true)
            {
                if (this.Actual == null)
                {
                    cat = new Categoria();
                    Program.Categorias.Add(cat);
                    cat.Codigo = Program.Categorias.Count;
                }
                else
                {
                    cat = this.Actual;
                }

                cat.Nombre = this.txtNombre.Text;
                cat.Descripcion = this.txtDescripcion.Text;
                cat.Vigente = this.chkVigente.Checked;

                this.ActivarControles(false);
                //this.btnListar_Click(null, null);
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
            this.dgvListado.DataSource = null;
            if (Program.Categorias.Count > 0)
            {
                this.dgvListado.AutoGenerateColumns = false;
                this.dgvListado.DataSource = Program.Categorias;
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {

            if (this.dgvListado.CurrentRow != null)
            {
                this.Actual = (Categoria)this.dgvListado.CurrentRow.DataBoundItem;
                this.PresentarDatos();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una categoría", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.dgvListado.Focus();
            }
        }

        private void PresentarDatos()
        {
            this.txtNombre.Text = this.Actual.Nombre;
            this.txtDescripcion.Text = this.Actual.Descripcion;
            this.chkVigente.Checked = this.Actual.Vigente;
            this.chkVigente.Enabled = true;
            this.ActivarControles(true);
        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            this.txtNombre.Text = this.txtNombre.Text.Trim();
            if (this.txtNombre.Text.Length > 2)
            {
                this.errMensaje.SetError(this.txtNombre, "");
            }
            else
            {
                e.Cancel = true;
                this.errMensaje.SetError(this.txtNombre, "El nombre debe tener al menos 02 caracteres");
            }
        }

        private void gbEntidad_Enter(object sender, EventArgs e)
        {

        }

    }
}
