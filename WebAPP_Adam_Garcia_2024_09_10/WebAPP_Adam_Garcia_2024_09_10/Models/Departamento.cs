using System;
using System.Collections.Generic;

namespace WebAPP_Adam_Garcia_2024_09_10.Models
{
    public partial class Departamento
    {
        public Departamento()
        {
            AsientoContables = new HashSet<AsientoContable>();
        }

        public int DptoId { get; set; }
        public string DescDpto { get; set; } = null!;

        public virtual ICollection<AsientoContable> AsientoContables { get; set; }
    }
}
