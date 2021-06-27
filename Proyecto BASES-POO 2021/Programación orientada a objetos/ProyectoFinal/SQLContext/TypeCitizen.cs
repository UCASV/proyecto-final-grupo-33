using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class TypeCitizen
    {
        public TypeCitizen()
        {
            Citizens = new HashSet<Citizen>();
        }

        public int Id { get; set; }
        public string TypeCitizen1 { get; set; }

        public virtual ICollection<Citizen> Citizens { get; set; }
    }
}
