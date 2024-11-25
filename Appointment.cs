using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int UserID { get; set; }
        public int PatientID { get; set; }
        public DateOnly AppointmentDate { get; set; }
        public TimeOnly AppointmentTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean Active { get; set; }
    }
}
