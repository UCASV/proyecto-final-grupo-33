using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Employee
    {
        public int Id { get; set; }
        public string Fullname { get; set; }
        public string Addres { get; set; }
        public string Email { get; set; }
        public int? IdTypeEmployee { get; set; }
        public int? IdManager { get; set; }

        public virtual Manager IdManagerNavigation { get; set; }
        public virtual TypeEmployee IdTypeEmployeeNavigation { get; set; }
    }
}
