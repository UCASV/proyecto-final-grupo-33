using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Municipality
    {
        public Municipality()
        {
            Cabins = new HashSet<Cabin>();
            Citizens = new HashSet<Citizen>();
        }

        public int Id { get; set; }
        public string Municipality1 { get; set; }
        public int? IdDepartment { get; set; }

        public virtual Department IdDepartmentNavigation { get; set; }
        public virtual ICollection<Cabin> Cabins { get; set; }
        public virtual ICollection<Citizen> Citizens { get; set; }
    }
}
