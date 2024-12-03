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

        public void ExecuteFeature(string  selectedFeature,User user)
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
                            Logout();
                             Console.ReadLine();
                            break;
                        case "FetchAllUsers":
                            FetchAllUsers(connectionString);
                            Console.ReadLine();
                            break;
                        case "AddUser":
                            AddUser(connectionString);
                            Console.WriteLine("User Created Successfully");
                            Console.ReadLine();
                            break;
                        case "ChangePassword":
                            ChangePassword(connectionString);
                            Console.WriteLine("Password Changed Successfully");
                            Console.ReadLine();
                            break;
                        case "EnableUser":
                            EnableUser(connectionString);
                            Console.WriteLine("User Enabled Successfully");
                            Console.ReadLine();
                            break;
                        case "DisableUser":
                            DisableUser(connectionString, user);
                            Console.WriteLine("User Disabled Successfully");
                            Console.ReadLine();
                            break;
                        case "UpdateUserType":
                            UpdateUserType(connectionString);
                            Console.WriteLine("UserType Updated Successfully");
                            Console.ReadLine();
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
        public void Logout()
        {
            Console.WriteLine("LoggedOut Successfully");
        }
        public void FetchAllUsers(string connectionString)
        {
            UserRepository userrepo = new UserRepository();
           
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
        public void AddUser(string connectionString)
        {
            UserRepository userrepo = new UserRepository();
         //   string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
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

        public void UpdateUser(string connectionString)
        {
            UserRepository userrepo = new UserRepository();
            //   string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            User usr = new User();
            Console.WriteLine("Enter UserName");
            usr.Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(connectionString);
            if (users.Find(u => u.Username == usr.Username) != null)
            {

                Console.WriteLine("Enter Password");
                usr.Password = Console.ReadLine();
                // usr.Active = true;
                Console.WriteLine("Select UserType");
                Console.WriteLine("1. Admin");
                Console.WriteLine("2. Doctor");
                Console.WriteLine("3. Nurse");
                string userType = Console.ReadLine();
                switch (userType)
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
                    userrepo.UpdateUser(usr, connectionString);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " doesn't  exist");
            }
        }
        public void ChangePassword(string connectionstring)
        {
            UserRepository userrepo = new UserRepository();
            User usr = new User();
            Console.WriteLine("Enter UserName");
            usr.Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(connectionstring);
            if (users.Find(u => u.Username == usr.Username) != null)
            {
                Console.WriteLine("Enter new Password");
                usr.Password = Console.ReadLine();
                try
                {
                    userrepo.ChangeUserPassword(usr, connectionstring);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to Change User Password. Please check the exception. " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " doesn't exist.");
            }
        }
        public void EnableUser(string connectionstring)
        {
            UserRepository userrepo = new UserRepository();
          //  User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(connectionstring);
            User usrfound = users.FirstOrDefault(u => u.Username == Username);
            if (usrfound != null)
            {
               
                try
                {
                    userrepo.EnableuserAccount(Username, connectionstring);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to Enable User. Please check the exception. " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User " + Username + " doesn't exist.");
            }
        }
        public void DisableUser(string connectionstring, User user)
        {
            UserRepository userrepo = new UserRepository();
            //  User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            if (Username != user.Username)
            {
                List<User> users = userrepo.FetchAllUsers(connectionstring);
                User usrfound = users.FirstOrDefault(u => u.Username == Username);
                if (usrfound != null)
                {

                    try
                    {
                        userrepo.DisableuserAccount(Username, connectionstring);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Unable to Disable User . Please check the exception. " + ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("User " + Username + " doesn't exist.");
                }
            }
            else
            {
                Console.WriteLine("User Can't disable his own account");
            }
        }
        public void UpdateUserType(string connectionstring)
        {
            UserRepository userrepo = new UserRepository();
              User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(connectionstring);
            User usrfound = users.FirstOrDefault(u => u.Username == Username);
            if (usrfound != null)
            {

                try
                {
                    usr.Username = Username;
                    Console.WriteLine("Select UserType");
                    Console.WriteLine("1. Admin");
                    Console.WriteLine("2. Doctor");
                    Console.WriteLine("3. Nurse");
                    string userType = Console.ReadLine();
                    switch (userType)
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

                    userrepo.UpdateUserType(usr, connectionstring);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unable to Change User Password. Please check the exception. " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("User " + Username + " doesn't exist.");
            }
        }

    }
}
