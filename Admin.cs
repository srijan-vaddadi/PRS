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
        //public Admin(string username, string password)
        //    : base(username, password, UserType.Admin) { }

        public override void DisplayFeatures(User user, string filePath, string patientFilepath)
        {
            // var  = @"C:\temp\PRS\users.csv";
            Console.WriteLine("Please select one of the below options");
            UserRepository usr = new UserRepository();
            //    List<Feature> features= usr.FetchUserFeatures(connectionstring, "1");

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
                new Feature(10,"SearchPatient")

            };

            foreach (Feature feature in features)
            {
                Console.WriteLine(feature.FeatureId + ". " + feature.FeatureName);
            }
            int featureid = Convert.ToInt32(Console.ReadLine());
            Feature featurename = features.Find(f => f.FeatureId == featureid);
            Feature onjFeature = new Feature();
            onjFeature.ExecuteFeature(featurename.FeatureName, user, filePath, patientFilepath);
            //Console.WriteLine("1. Add User");
            //Console.WriteLine("2. View Users");
        }
    }
}
