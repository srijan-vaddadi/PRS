using PRS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRS
{
    public class Feature
    {
        public int FeatureId { get; set; }
        public string FeatureName { get; set; }

        public void ExecuteFeature(string  selectedFeature)
        {
            //var selectedFeature = user.AllowedFeatures.Find(f => f.FeatureId == selectedFeatureId);
            string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            try
            {
                if (selectedFeature != null)
                {
                    switch (selectedFeature)
                    {
                        case "Logout":
                            Users user = new Admin();
                            user.Logout();
                            // Console.ReadLine();
                            break;
                        case "FetchAllUsers":
                            FetchAllUsers();
                            Console.ReadLine();
                            break;
                        case "AddUser":
                            AddUser();
                            Console.ReadLine();
                            break;
                        case "ViewPatientHistory":
                            Console.WriteLine("Viewing patient history...");
                            break;
                        case "ScheduleAppointments":
                            Console.WriteLine("Scheduling appointments...");
                            break;
                        default:
                            Console.WriteLine("Unknown feature.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Selected feature is not accessible for this user.");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public void FetchAllUsers()
        {
            UserRepository userrepo = new UserRepository();
            string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            List<User> users = userrepo.FetchAllUsers(connectionString);
            Console.WriteLine("Following is the Users List");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("UserName "+"        |         "+ " Enabled ");
            Console.WriteLine("-------------------------------------------------------");
            foreach (User user in users)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(user.Username + "           |            " + user.Active);
                Console.WriteLine("-------------------------------------------------------");
            }
        }
        public void AddUser()
        {
            UserRepository userrepo = new UserRepository();
            string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            User usr = new User();
            Console.WriteLine("Enter UserName");
            usr.Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(connectionString);
            if (users.Find(u => u.Username == usr.Username) == null)
            {

                Console.WriteLine("Enter Password");
                usr.Password = Console.ReadLine();
                // usr.Active = true;
                Console.WriteLine("Select UserType");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Doctor");
                Console.WriteLine("3. Nurse");
                string userType= Console.ReadLine();
                switch(userType)
                {
                    case "1":
                        usr.UserTypeId = UserType.Admin;
                        break;
                        
                    case "2":
                        usr.UserTypeId = UserType.Doctor;
                        break;
                    case "3":
                        usr.UserTypeId = UserType.Nurse;
                        break;

                }

                try
                {
                    userrepo.SaveUser(usr, connectionString);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message);}
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " already exists");
            }
        }
    }
}
