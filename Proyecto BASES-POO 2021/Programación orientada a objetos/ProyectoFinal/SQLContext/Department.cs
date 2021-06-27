using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Department
    {
        public Department()
        {
            Cabins = new HashSet<Cabin>();
            Citizens = new HashSet<Citizen>();
            Municipalities = new HashSet<Municipality>();
        }

        public int Id { get; set; }
        public string Department1 { get; set; }

        public virtual ICollection<Cabin> Cabins { get; set; }
        public virtual ICollection<Citizen> Citizens { get; set; }
        public virtual ICollection<Municipality> Municipalities { get; set; }
    }
}
