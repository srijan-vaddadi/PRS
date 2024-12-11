﻿using PRS.Repository;
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
                new Feature(4, "ChangePassword"),
                new Feature(5,"EnableUser"),
                new Feature(6,"DisableUser"),
                new Feature(7,"UpdateUserType"),
                new Feature(8,"FetchAllUsers"),
                new Feature(9,"AddNewPatient"),
                new Feature(10,"FetchAllPatients"),
                new Feature(11,"SearchPatient"),
                new Feature(12,"ViewAppointments")
            };          
            RunFeatures(features, user);          
        }
    }
}
