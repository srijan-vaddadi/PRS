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
                new Feature(3, "UpdateUser"),
                new Feature(4, "ChangeUsername"),
                new Feature(5, "ChangePassword"),
                new Feature(6,"EnableUser"),
                new Feature(7,"DisableUser"),
                new Feature(8,"UpdateUserType"),
                new Feature(9,"FetchAllUsers"),
                new Feature(10,"AddNewPatient"),
                new Feature(11,"FetchAllPatients"),
                new Feature(12,"SearchPatient"),
                new Feature(13,"ViewAppointments")
            };          
            RunFeatures(features, user);          
        }
    }
}
