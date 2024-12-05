using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Appointment
    {
        public string HospitalNumber { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentTime { get; set; }
        public string DoctorName { get; set; }
        public bool Active { get; set; }

    }
}
