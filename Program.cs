using PRS.Repository;using System.Numerics;namespace PRS{    class Program    {
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
            string connectionString = "Server=PRECISION-SRIJ\\SQLEXPRESS;Database=PRS;Trusted_Connection=True;";
            List<User> users = new List<User>();
            UserRepository userRepository = new UserRepository();
            users = userRepository.FetchAllUsers(connectionString);
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
                    else
                    {
                        switch (usr1.UserTypeId)
                        {
                            case UserType.Admin:
                                Users adm = new Admin();
                                adm.DisplayFeatures(connectionString, usr1);
                                break;
                            //case UserType.Doctor:
                            //    newUser = new Doctor(username, password);
                            //    break;
                            //case UserType.Nurse:
                            //    newUser = new Nurse(username, password);
                            //    break;
                            default:
                                return; 
                        }
                    }

                }
            }
        }
    }
}