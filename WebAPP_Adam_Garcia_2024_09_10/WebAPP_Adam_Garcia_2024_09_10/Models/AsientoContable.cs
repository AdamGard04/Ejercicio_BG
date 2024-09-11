using System;
using System.Collections.Generic;

namespace WebAPP_Adam_Garcia_2024_09_10.Models
{
    public partial class AsientoContable
    {
        public AsientoContable()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int DepartamentoId { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Estado { get; set; } = null!;

        public virtual Departamento Departamento { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; }
    }
}
