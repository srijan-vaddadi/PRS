using PRS.Repository;using System.Numerics;using System.Configuration;namespace PRS{    class Program    {
        static void Main()
        {
            Console.WriteLine("Select your choice");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            var choice = Console.ReadLine();
           // Console.WriteLine("Yours choice" + choice);

            switch (choice)
            {
                case "1":
                    Login();
                    break;

                case "2":
                    Console.WriteLine("Exit");
                    break;

                default:
                    return;

            }
        }
        public static void Login()
        {
           // var filePath= @"C:\temp\PRS\users.csv";
            var filePath = ConfigurationManager.AppSettings["userfilepath"];
            var patientFilepath = ConfigurationManager.AppSettings["patientbasicfilepath"];
           // var  patientFilepath = @"C:\temp\PRS\patients.csv";
            List<User> users = new List<User>();
            UserRepository userRepository = new UserRepository();
            users = userRepository.FetchAllUsers(filePath);
            if (users != null)
            {
                Console.WriteLine("Enter Username:");
                string username = Console.ReadLine();

                User usr = users.Find(u => u.Username == username);

                if (usr == null)
                {
                    Console.WriteLine("User does not exist");
                }
                else 
                {
                    Console.WriteLine("Enter Password: ");
                    string password = Console.ReadLine();
                    User usr1 = users.Find(u => u.Username == username && u.Password == password);

                    if (usr1 == null) 
                    {
                        Console.WriteLine("Username or password is incorrect");
                    }
                    else if(usr1.Active==false)
                    {
                        Console.WriteLine("Given user is Disabled.Can't login.");
                    }
                    else
                    {
                        switch (usr1.UserType)
                        {
                            case "Admin":
                                Users adm = new Admin();
                                adm.DisplayFeatures(usr1, filePath, patientFilepath);
                                break;
                            case "Doctor":
                                Users doctor = new Doctor();
                                doctor.DisplayFeatures(usr1, filePath, patientFilepath);
                                break;
                            case "Nurse":
                                Users nurse = new Nurse();
                                nurse.DisplayFeatures(usr1, filePath, patientFilepath);
                                break;
                            default:
                                return; 
                        }
                    }

                }
            }
        }
    }
}