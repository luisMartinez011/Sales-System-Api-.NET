using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Producto
    {
        public Producto()
        {
            Conceptos = new HashSet<Concepto>();
        }

        public long Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Precio { get; set; }

        public virtual ICollection<Concepto> Conceptos { get; set; }
    }
}
