using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Managerxcabin
    {
        public int IdRegister { get; set; }
        public int? IdManager { get; set; }
        public int? IdCabin { get; set; }
        public DateTime? DateTime { get; set; }

        public virtual Cabin IdCabinNavigation { get; set; }
        public virtual Manager IdManagerNavigation { get; set; }
    }
}
