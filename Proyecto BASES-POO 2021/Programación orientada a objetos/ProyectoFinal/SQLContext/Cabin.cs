using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Cabin
    {
        public Cabin()
        {
            Appointments = new HashSet<Appointment>();
            Managerxcabins = new HashSet<Managerxcabin>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string ManagerName { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public int? IdDepartment { get; set; }
        public int? IdMunicipality { get; set; }

        public virtual Department IdDepartmentNavigation { get; set; }
        public virtual Municipality IdMunicipalityNavigation { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Managerxcabin> Managerxcabins { get; set; }
    }
}
