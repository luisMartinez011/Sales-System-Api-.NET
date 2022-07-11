using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Venta
    {
        public Venta()
        {
            Conceptos = new HashSet<Concepto>();
        }

        public long Id { get; set; }
        public DateOnly Fecha { get; set; }
        public int Total { get; set; }
        public long IdCliente { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual ICollection<Concepto> Conceptos { get; set; }
    }
}
