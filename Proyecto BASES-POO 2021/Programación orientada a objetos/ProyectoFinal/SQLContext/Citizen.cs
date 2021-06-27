using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Citizen
    {
        public Citizen()
        {
            Appointments = new HashSet<Appointment>();
            Diseases = new HashSet<Disease>();
            SideEffects = new HashSet<SideEffect>();
        }

        public int Dui { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Addres { get; set; }
        public string Phone { get; set; }
        public int? IdDepartment { get; set; }
        public int? IdMunicipality { get; set; }
        public int? IdTypeCitizen { get; set; }

        public virtual Department IdDepartmentNavigation { get; set; }
        public virtual Municipality IdMunicipalityNavigation { get; set; }
        public virtual TypeCitizen IdTypeCitizenNavigation { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
        public virtual ICollection<SideEffect> SideEffects { get; set; }
    }
}
