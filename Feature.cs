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
        public Feature(int id,string name)
        {
            FeatureId = id;
            FeatureName = name;
        }
        public Feature() { }
        public void ExecuteFeature(string  selectedFeature,User user,string filePath,string patientFilepath)
        {
            //var selectedFeature = user.AllowedFeatures.Find(f => f.FeatureId == selectedFeatureId);
           // string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            
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
                            FetchAllUsers(filePath);
                            Console.ReadLine();
                            break;
                        case "AddUser":
                            AddUser(filePath);
                            Console.WriteLine("User Created Successfully");
                            Console.ReadLine();
                            break;
                        case "ChangePassword":
                            ChangePassword(filePath);
                            Console.WriteLine("Password Changed Successfully");
                            Console.ReadLine();
                            break;
                        case "EnableUser":
                            EnableUser(filePath);
                            Console.WriteLine("User Enabled Successfully");
                            Console.ReadLine();
                            break;
                        case "DisableUser":
                            DisableUser(filePath, user);
                            Console.WriteLine("User Disabled Successfully");
                            Console.ReadLine();
                            break;
                        case "UpdateUserType":
                            UpdateUserType(filePath);
                            Console.WriteLine("UserType Updated Successfully");
                            Console.ReadLine();
                            break;
                        case "AddNewPatient":
                            AddNewPatient(patientFilepath);
                            Console.WriteLine("Patient  Created Successfully");
                            Console.ReadLine();
                            break;
                        case "FetchAllPatients":
                            FetchAllPatients(patientFilepath);
                           // Console.WriteLine("Patient  Created Successfully");
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
        public void FetchAllUsers(string filepath)
        {
            UserRepository userrepo = new UserRepository();
           
            List<User> users = userrepo.FetchAllUsers(filepath);
            Console.WriteLine("Following is the Users List");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("UserName "+"        |         "+ " Enabled " +"       |         "+"UserType");
            Console.WriteLine("-------------------------------------------------------");
            foreach (User user in users)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(user.Username + "           |            " + user.Active+"       |         "+user.UserType);
                Console.WriteLine("-------------------------------------------------------");
            }
        }
        public void FetchAllPatients(string filepath)
        {
            PatientRepository patientrepo = new PatientRepository();

            List<Patients> patients = patientrepo.FetchAllPatients(filepath);
            Console.WriteLine("Following is the Patients List");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("FirstName " + "        |         " + " Surname " + "       |       " + "DOB" + "       |       "+"Contact");
            Console.WriteLine("-------------------------------------------------------");
            foreach (Patients patient in patients)
            {
                Console.WriteLine("-------------------------------------------------------");
                Console.WriteLine(patient.FirstName + "        |        " + patient.Surname + "       |         " + patient.DateOfBirth+ "       |       "+ patient.PhoneNumber);
                Console.WriteLine("-------------------------------------------------------");
            }
        }
        public void AddUser(string filepath)
        {
            UserRepository userrepo = new UserRepository();
         //   string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            User usr = new User();
            Console.WriteLine("Enter UserName");
            usr.Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
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
                        usr.UserType = "Admin";
                        break;
                        
                    case "2":
                        usr.UserType = "Doctor";
                        break;
                    case "3":
                        usr.UserType = "Nurse";
                        break;

                }
                usr.Active = true;
                try
                {
                    userrepo.SaveUser(usr, filepath);
                }
                catch (Exception ex) { Console.WriteLine(ex.Message);}
            }
            else
            {
                Console.WriteLine("User " + usr.Username + " already exists");
            }
        }

        public void AddNewPatient(string filepath)
        {
            Patients patient = new Patients();
            Console.WriteLine("Enter FirstName");
            patient.FirstName = Console.ReadLine();
            Console.WriteLine("Enter Surname");
            patient.Surname = Console.ReadLine();
            Console.WriteLine("Enter DOB");
            patient.DateOfBirth = Console.ReadLine();
            Console.WriteLine("Enter PhoneNumber");
            patient.PhoneNumber = Console.ReadLine();
            Console.WriteLine("Enter NHS Number");
            patient.NHSNumber = Console.ReadLine();
            patient.HospitalNumber = "PRS-" + DateTime.Now.ToString("yyyyMMdd");
            PatientRepository patientrepo= new PatientRepository();
            patientrepo.SavePatient(patient, filepath);
        }

        
        public void ChangePassword(string filepath)
        {
            UserRepository userrepo = new UserRepository();
           // User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usr = users.Find(u => u.Username == Username);
            if (usr != null)
            {
                Console.WriteLine("Enter new Password");
                usr.Password = Console.ReadLine();
                try
                {
                    userrepo.WriteUsersToCsv(filepath, users);
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
        public void EnableUser(string filepath)
        {
            // User usr = new User();
            UserRepository userrepo=new UserRepository();   
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usr = users.Find(u => u.Username == Username);
            if (usr != null)
            {

                try
                {
                    usr.Active = true;
                    //userrepo.EnableuserAccount(Username, connectionstring);
                    userrepo.WriteUsersToCsv(filepath, users);
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
        public void DisableUser(string filepath, User user)
        {
            UserRepository userrepo = new UserRepository();
            //  User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            if (Username != user.Username)
            {
                List<User> users = userrepo.FetchAllUsers(filepath);
                User usrfound = users.FirstOrDefault(u => u.Username == Username);
                if (usrfound != null)
                {

                    try
                    {
                        usrfound.Active = false;
                        //userrepo.EnableuserAccount(Username, connectionstring);
                        userrepo.WriteUsersToCsv(filepath, users);
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
        public void UpdateUserType(string filepath)
        {
            UserRepository userrepo = new UserRepository();
              //User usr = new User();
            Console.WriteLine("Enter UserName");
            string Username = Console.ReadLine();
            List<User> users = userrepo.FetchAllUsers(filepath);
            User usrfound = users.FirstOrDefault(u => u.Username == Username);
            if (usrfound != null)
            {

                try
                {
                   // usr.Username = Username;
                    Console.WriteLine("Select UserType");
                    Console.WriteLine("1. Admin");
                    Console.WriteLine("2. Doctor");
                    Console.WriteLine("3. Nurse");
                    string userType = Console.ReadLine();
                    switch (userType)
                    {
                        case "1":
                            usrfound.UserType = "Admin";
                            break;

                        case "2":
                            usrfound.UserType = "Doctor";
                            break;
                        case "3":
                            usrfound.UserType = "Nurse";
                            break;

                    }
                    userrepo.WriteUsersToCsv(filepath, users);
                    // userrepo.UpdateUserType(usr, connectionstring);
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
