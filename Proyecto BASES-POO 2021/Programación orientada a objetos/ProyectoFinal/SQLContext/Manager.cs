using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Manager
    {
        public Manager()
        {
            Employees = new HashSet<Employee>();
            Managerxcabins = new HashSet<Managerxcabin>();
        }

        public int Id { get; set; }
        public string UserEmployee { get; set; }
        public string PasswordManager { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Managerxcabin> Managerxcabins { get; set; }
    }
}
