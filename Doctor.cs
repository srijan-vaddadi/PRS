using PRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Doctor : Users
    {
        public override void DisplayFeatures(User user)
        {
         
            List<Feature> features = new List<Feature>{
                new Feature(1, "Logout"),
                new Feature(2,"AddNewPatient"),
                new Feature(3,"FetchAllPatients"),
                new Feature(4,"SearchPatient"),
                new Feature(5,"AddAppointment"),
                new Feature(6,"AddPrescription"),
                new Feature(7,"AddPatientNotes"),
                new Feature(8,"ViewAppointments"),
                new Feature(9,"ViewPrescriptions"),
                new Feature(10,"ViewPatientNotes"),
                new Feature(11,"DeactivatePrescription"),
                new Feature(12,"DeactivateNote")
            };

           RunFeatures(features,user);
        }
    }

}
