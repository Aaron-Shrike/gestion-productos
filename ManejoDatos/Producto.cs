using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManejoDatos
{
    class Producto
    {

        public int Codigo { get; set; }
        public Categoria Categoria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public int Stock { get; set; }
        public bool Vigente { get; set; }

        public string NombreCategoria
        {
            get
            {
                string nom = "";

                if (this.Categoria != null)
                {
                    nom = this.Categoria.Nombre;
                }

                return nom;
            }
        }

    }
}
