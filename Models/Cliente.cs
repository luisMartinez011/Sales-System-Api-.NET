using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Venta = new HashSet<Venta>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Venta> Venta { get; set; }
    }
}
