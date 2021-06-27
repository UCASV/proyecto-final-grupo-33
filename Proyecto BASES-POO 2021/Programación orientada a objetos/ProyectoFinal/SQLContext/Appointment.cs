using System;
using System.Collections.Generic;

#nullable disable

namespace ProyectoFinal.SQLContext
{
    public partial class Appointment
    {
        public int IdAppointment { get; set; }
        public string HourAppointment { get; set; }
        public string HourArrived { get; set; }
        public string HourVaccunated { get; set; }
        public DateTime? DateAppointment { get; set; }
        public int? IdCabin { get; set; }
        public int? IdCitizen { get; set; }

        public virtual Cabin IdCabinNavigation { get; set; }
        public virtual Citizen IdCitizenNavigation { get; set; }
    }
}
