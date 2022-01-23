using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManejoDatos
{
    static class Program
    {

        public static List<Categoria> Categorias = new List<Categoria>();
        public static List<Producto> Productos = new List<Producto>();

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrincipal ());
        }
    }
}
