using PRS.Repository;
using System.Numerics;
using System.Configuration;

namespace PRS
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Select your choice");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Exit");
            var choice = Console.ReadLine();  
            Feature fea=new Feature();
            switch (choice)
            {
                case "1":
                    fea.Login();
                    break;
                case "2":
                    fea.Exit();
                    break;
                default:
                    return;

            }
        }       
    }
}