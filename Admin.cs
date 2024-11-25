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

        public override void DisplayFeatures(string connectionstring, UserType UserTypeId)
        {
            Console.WriteLine("Please select one of the below options");
            UserRepository usr=new UserRepository();
           List<Feature> features= usr.FetchUserFeatures(connectionstring, UserTypeId);
            foreach (Feature feature in features)
            {
                Console.WriteLine(feature.FeatureId +". " +feature.FeatureName);
            }
            int featureid = Convert.ToInt32(Console.ReadLine());
            Feature featurename= features.Find(f => f.FeatureId == featureid);
            Feature onjFeature = new Feature();
            onjFeature.ExecuteFeature(featurename.FeatureName);
            //Console.WriteLine("1. Add User");
            //Console.WriteLine("2. View Users");
        }
    }
}
