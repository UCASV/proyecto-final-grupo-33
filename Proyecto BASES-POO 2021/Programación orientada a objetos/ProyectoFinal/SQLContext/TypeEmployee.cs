using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class TypeEmployee
    {
        public TypeEmployee()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }
        public string Employee { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
