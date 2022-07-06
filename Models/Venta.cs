using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Venta
    {
        public long Id { get; set; }
        public DateOnly Fecha { get; set; }
        public int Total { get; set; }
        public long IdCliente { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Concepto Concepto { get; set; } = null!;
    }
}
