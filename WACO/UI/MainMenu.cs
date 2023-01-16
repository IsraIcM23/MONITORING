using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WACO.UI
{
    internal class MainMenu
    {
        private WacoController waco;
        public MainMenu()
        {
            List<User> users = new List<User>();
            waco = new WacoController(users);
        }

        internal void DisplayMainMenu()
        {
            while (true)
            {
                ShowMainMenuOptions();
                try
                {
                    int userInput = ReadIntFromMenu();
                    switch (userInput)
                    {
                        case 0:
                            return;
                        case 1:
                            DisplayRegisterUser();
                            break;
                        case 2:
                            DisplayRegisterLecture();
                            break;
                        case 3:
                            ShowDebt();
                            break;
                        case 4:
                            DisplayPayPartialDebt();
                            break;
                        case 5:
                            GetTotalDebtsByUsers();
                            break;
     
                        default:
                        break;
                    }    
                }
                catch (UserException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void DisplayPayPartialDebt()
        {
            Console.WriteLine("Enter CI");
            int ci = ReadIntFromMenu();
            if (waco.ExistsUserWithCI(ci))
            {
                var user = waco.FindUser(ci);
                user.TotalDebt();
                Console.WriteLine("Enter number of period to be paid");
                var numPer = Int32.Parse(Console.ReadLine());
                Console.WriteLine("Are you sure to paid the number of periods selected? Y(Yes) N(No)");
                var option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "Y":
                        user.PaidPartialDebt(numPer);
                        Console.WriteLine("The amount has been paid");
                        break;
                    case "N":
                        Console.WriteLine("Canceled by the user");
                        break;
                    default:
                        Console.WriteLine($"Invalid option entered. '{option}'");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No user found for CI {0}", ci);
            }
        }

        private void ShowDebt()
        {
            Console.WriteLine("Enter CI");
            int ci = ReadIntFromMenu();
            if (waco.ExistsUserWithCI(ci))
            {
                var user = waco.FindUser(ci);
                user.TotalDebt();
            }
            else
            {
                Console.WriteLine("No user found for CI {0}", ci);
            }
        }

        private void GetTotalDebtsByUsers()
        {
            Console.WriteLine("Enter CI");
            int ci = ReadIntFromMenu();
            if (waco.ExistsUserWithCI(ci))
            {
                var user = waco.FindUser(ci);
                user.TotalDebt();
                Console.WriteLine("Are you sure to Paid the total debt? Y(Yes) N(No)");
                var option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "Y":
                        user.PaidTotalDebt();
                        Console.WriteLine("The amount has been paid");
                        break;
                    case "N":
                        Console.WriteLine("Canceled by the user");
                        break;
                    default:
                        Console.WriteLine($"Invalid option entered. '{option}'");
                        break;
                }
            }
            else
            {
                Console.WriteLine("No user found for CI {0}", ci);
            }
        }

        private void ShowMainMenuOptions()
        {
            Console.WriteLine("-------------WACO SYSTEM----------------");
            Console.WriteLine("----------Choose an Option---------------");
            Console.WriteLine("1: Register User");
            Console.WriteLine("2: Register Water Consumption");
            Console.WriteLine("3: Show Comsumption Record");
            Console.WriteLine("4: Payment the debt per month / months from a user ");
            Console.WriteLine("5: Payment total debt of a user");
            Console.WriteLine("0: Exit");
            Console.WriteLine("-----------------------------------------");
        }


        private void DisplayRegisterUser()
        {
            Console.WriteLine("Enter CI");
            int ci = ReadIntFromMenu();
            if (waco.ExistsUserWithCI(ci))
            {
                Console.WriteLine("The CI already Exists");
            }
            else
            {
                Console.WriteLine("Enter Name");
                var name = Console.ReadLine();
                Console.WriteLine("Enter Sur Name");
                var surname = Console.ReadLine();
                Console.WriteLine("Are you sure to register the user? Y(Yes) N(No)");
                var option = Console.ReadLine().ToUpper();

                switch (option)
                {
                    case "Y":
                        User myUser = new User(ci, name, surname);
                        waco.Add(myUser);
                        Console.WriteLine("User Added");
                        break;
                    case "N":
                        Console.WriteLine("Canceled by the user");
                        break;
                    default:
                        Console.WriteLine($"Invalid option entered. '{option}'");
                        break;
                }
            }
        }

        private void DisplayRegisterLecture()
        {
            Console.WriteLine("Enter CI");
            int ci = ReadIntFromMenu();
            if (waco.ExistsUserWithCI(ci))
            {
                var find = waco.FindUser(ci);
                Console.WriteLine("Enter Period by example:'MM/YYYY'");
                var period = Console.ReadLine();
               
                if (find.VerifyLecture(period))
                {
                    Console.WriteLine("There is already reading in this period");
                }
                else
                {
                    Console.WriteLine("Enter Consumption");
                    int consumption = ReadIntFromMenu();
                    Consumption myConsumption = new Consumption(period, consumption);
                    find.Add_Consumption(myConsumption);
                    Console.WriteLine($"The consumption of the period {period} has been registered correctly");
                }
            }

        }

        private int ReadIntFromMenu()
        {
            string option = Console.ReadLine();
            return int.Parse(option);
        }
    }
}
