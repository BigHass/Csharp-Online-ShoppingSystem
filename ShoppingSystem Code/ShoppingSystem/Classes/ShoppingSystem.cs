using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Threading;

namespace ShoppingSystem
{
	public static class ShoppingSystem
	{
        
        public static int CListSize = 0;
        public static int PListSize = 0;
        public static Customer[] CList = new Customer[100];                 //To store the data temporarily after program launch
        public static Product[] PList = new Product[100];

        
        public static void Update()//For updating the data base
		{
            BinaryFormatter bs = new BinaryFormatter();

            //save Customers changes to file
            FileStream fc = new FileStream("Customerx", FileMode.Create, FileAccess.Write);
            for (int i = 0; i < CListSize; i++)
            {
                bs.Serialize(fc, CList[i]);
            }
            fc.Close();
            //save product changes to file
            FileStream fp = new FileStream("Productx", FileMode.Create, FileAccess.Write);
            for (int i = 0; i < PListSize; i++)
            {
                bs.Serialize(fp, PList[i]);
            }

            fp.Close();

        }

		//Husni
		public static void Login()
		{
			while (true)
			{
				Console.Clear();
				string user_Name;
				string password;


				Console.WriteLine("Please Enter Your Choice");
				Console.WriteLine("1-Log In As Administrator");
				Console.WriteLine("2-Log In As Customer");
				Console.WriteLine("3-Exit");
				string choice = (Console.ReadLine());

				switch (choice)
				{

					case "1":
						Administrator admin = new Administrator();
						Console.Write("Enter user name: ");
						user_Name = Console.ReadLine();
						Console.Write("Enter password: ");
						password = Console.ReadLine();
						if (user_Name == admin.User_Name && password == admin.Password)
						{
							Console.Clear();
							Console.WriteLine("Login is successful\n");
							admin.AdminMenu();//load Administrator Menu
						}
						else
						{
							Console.WriteLine("\nWrong User name or password, please try again\n");
							Console.WriteLine("Press any key to try again");
							Console.ReadLine();
							continue;

						}
						break;
					case "2":
						bool success = false;
						Customer temp = new Customer();
						Console.Write("Enter user name: ");
						user_Name = Console.ReadLine();
						Console.Write("Enter password: ");
						password = Console.ReadLine();


						for (int i = 0; i < CListSize; i++)//find user
						{
							if (user_Name == CList[i].User_Name && password == CList[i].Password)
							{
								success = true;
								temp = CList[i];
							}
						}
						if (success)
						{
							Console.Clear();
							Console.WriteLine("Login is successful\n");
							temp.CustomerMenu();


						}
						else//not found
						{
							Console.WriteLine("\nWrong User name or password, please try again\n");
							Console.WriteLine("Press any key to try again");
							Console.ReadLine();
							continue;

						}
						break;
					case "3":
						Console.WriteLine("\nExiting the program.........\n\n");
						Environment.Exit(0);
						break;
					default:
						Console.WriteLine("Please Enter a valid key!.");
						Thread.Sleep(1000);
						break;

				}
			}
		}

	}
}
