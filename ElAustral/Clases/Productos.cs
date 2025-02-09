using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElAustral
{
    internal class Productos
    {
        public int id_producto { get; set; }
        public string nombre_producto { get; set; }
        public string descripcion { get; set; }
        public int precio_venta { get; set; }
        public string categoria { get; set; }
        public int stock { get; set; }

    }
}
