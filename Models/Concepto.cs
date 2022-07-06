using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Concepto
    {
        public long Id { get; set; }
        public long IdVentas { get; set; }
        public long IdProducto { get; set; }
        public int Cantidad { get; set; }
        public int PrecioUnitario { get; set; }
        public int Importe { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Venta IdVentasNavigation { get; set; } = null!;
    }
}
