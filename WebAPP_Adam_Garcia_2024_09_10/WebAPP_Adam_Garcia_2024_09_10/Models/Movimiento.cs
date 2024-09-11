using System;
using System.Collections.Generic;

namespace WebAPP_Adam_Garcia_2024_09_10.Models
{
    public partial class Movimiento
    {
        public int AsientoId { get; set; }
        public DateTime AsientoFecha { get; set; }
        public int CuentaId { get; set; }
        public decimal Valor { get; set; }
        public string? Descripcion { get; set; }
        public string TipoMovimiento { get; set; } = null!;

        public virtual AsientoContable Asiento { get; set; } = null!;
        public virtual CuentaContable Cuenta { get; set; } = null!;
    }
}
