using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class SideEffect
    {
        public int Id { get; set; }
        public string SideEffect1 { get; set; }
        public int? DuiCitizen { get; set; }

        public virtual Citizen DuiCitizenNavigation { get; set; }
    }
}
