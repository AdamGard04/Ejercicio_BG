using System;
using System.Collections.Generic;

namespace WebAPP_Adam_Garcia_2024_09_10.Models
{
    public partial class CuentaContable
    {
        public CuentaContable()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int CuentaId { get; set; }
        public string NumCuenta { get; set; } = null!;

        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
