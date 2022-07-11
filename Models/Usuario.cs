using System;
using System.Collections.Generic;

namespace Ventas.Models
{
    public partial class Usuario
    {
        public int IdUsers { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
