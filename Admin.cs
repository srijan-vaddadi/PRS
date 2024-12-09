using PRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Admin : Users
    {
        public override void DisplayFeatures(User user)
        {            
            List<Feature> features = new List<Feature>{
                new Feature(1, "Logout"),
                new Feature(2, "AddUser"),
                new Feature(3, "ChangePassword"),
                new Feature(4,"EnableUser"),
                new Feature(5,"DisableUser"),
                new Feature(6,"UpdateUserType"),
                new Feature(7,"FetchAllUsers"),
                new Feature(8,"AddNewPatient"),
                new Feature(9,"FetchAllPatients"),
                new Feature(10,"SearchPatient"),
                new Feature(11,"ViewAppointments")

            };          
            RunFeatures(features, user);
           
        }
    }
}
