using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public abstract class Users
    {     
        public abstract void DisplayFeatures(User usr);
        public void RunFeatures(List<Feature> features,User user)
        {
            int featureId = 0;
            while (featureId != 1)
            {
                Console.WriteLine("Please select one of the below options");
                foreach (Feature feature in features)
                {
                    Console.WriteLine(feature.FeatureId + ". " + feature.FeatureName);
                }
                featureId = Convert.ToInt32(Console.ReadLine());
                Feature featurename = features.Find(f => f.FeatureId == featureId);
                Feature onjFeature = new Feature();
                onjFeature.ExecuteFeature(featurename.FeatureName, user);
                if (featureId != 1)
                {
                    Console.WriteLine("\nPress any key to return to the menu...");
                    Console.ReadKey();
                }
            }
        } 
    }
    public class User
    {    
        public string Username { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool Active { get; set; }

    }
    public enum UserType
    {
        Admin = 1,
        Doctor = 2,
        Nurse = 3
    }
}
