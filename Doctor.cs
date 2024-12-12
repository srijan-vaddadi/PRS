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
                new Feature(3,"ViewAllPatients"),
                new Feature(4,"SearchPatient"),
                new Feature(5,"AddAppointment"),
                new Feature(6,"CancelAppointment"),
                new Feature(7,"AddPrescription"),
                new Feature(8,"AddPatientNotes"),
                new Feature(9,"ViewAppointments"),
                new Feature(10,"ViewPrescriptions"),
                new Feature(11,"ViewPatientNotes"),
                new Feature(12,"DeactivatePrescription"),
                new Feature(13,"DeactivateNote"),
                new Feature(14,"AlterNote"),
                new Feature(15,"AlterDosage")
            };
           RunFeatures(features,user);
        }
    }
}
