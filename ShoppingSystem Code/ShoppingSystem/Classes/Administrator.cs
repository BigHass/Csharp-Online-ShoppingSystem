using System;
using ShoppingSystem;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingSystem
{
	
	[Serializable]
	public class Administrator
	{
		
		public void AdminMenu()
		{
			Console.Clear();
			Console.WriteLine();

			string choice;
			Console.WriteLine();
			Console.WriteLine("Administrator Functions :\n");
			Console.WriteLine("1-Add and delete the customers");
			Console.WriteLine("2-Add and delete the products.[note the admin must determine the quantity of the added product].");
			Console.WriteLine("3-Seach product based on the Product_ID");
			Console.WriteLine("4-View all users.");
			Console.WriteLine("5-View all product based on the threshold quantity .");
			Console.WriteLine("6-View all the orders that are related to specific customers.");
			Console.WriteLine("7-Back to log in screen.");
			Console.WriteLine("\n Enter your choice");
			choice = (Console.ReadLine());

			switch (choice)
			{
				case "1":
					{
						Console.Clear();
						Console.WriteLine("1-Add to customer");
						Console.WriteLine("2-Delete to customer");
						Console.WriteLine("press 3 or any key to return to menu");
						Console.WriteLine("\n Enter your choice");
						string choicex = Console.ReadLine();

						if (choicex == "1") Add_Customer();
						else if (choicex == "2") Delete_Customer();
						else
							AdminMenu();
					}

					break;
				case "2":
					{
						Console.Clear();
						Console.WriteLine("1-Add to Product");
						Console.WriteLine("2-Delete to Product");
						Console.WriteLine("press 3 or any key to return to menu");
						Console.WriteLine("\n Enter your choice");
						string choicex = Console.ReadLine();
						if (choicex == "1") Add_Product();
						else if (choicex == "2") Delete_Product();
						else
							AdminMenu();


					}
					break;
				case "3":
					Console.Clear();
					SeachProduct_by_ID();
					break;
				case "4":
					Console.Clear();
					PrintCustomers();
					break;
				case "5":
					Console.Clear();
					Print_All_Products_Threshold();
					break;
				case "6":
					Console.Clear();
					View_CustomerOrders();
					return;

				case "7":
				default:
					break;



			}
		}

		//Fields
		private string user_Name = "admin";
		private string password = "CPE 232";

		//Set And Get
		public string User_Name
		{
			set { user_Name = value; }
			get { return user_Name; }
		}
		public string Password
		{
			set
			{ password = value; }
			get
			{ return password; }
		}

		//Print all Customers Currently in the system
		public void PrintCustomers()
		{
			Console.WriteLine("Customers in the system: ");
			Console.WriteLine();

			Console.WriteLine("-------------------------------------------------");
			Console.WriteLine("User Name  | Password   |   Address   | Credit");
			Console.WriteLine("-------------------------------------------------");

			for (int i = 0; i < ShoppingSystem.CListSize; i++)
			{
                ShoppingSystem.CList[i].PrintCustomer();

			}
			Console.WriteLine();
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadLine();
			AdminMenu();
		}

		//Print all Products meeting the Threshold
		public void Print_All_Products_Threshold()
		{
			string UserInput;

		tryagain:
			Console.WriteLine("Enter a Threshold:");
			UserInput = Console.ReadLine();
			if (!double.TryParse(UserInput, out double threshold))
			{
				Console.WriteLine("Threshold entered is in wrong formate(not a number), please try again\n ");
				goto tryagain;
			}

			Console.WriteLine("Products meeting the Threshold: ");
			Console.WriteLine();
			Console.WriteLine("---------------------------------------------------------------------------------------");
			Console.Write(Program.Indent(0) + "ID");
			Console.Write(Program.Indent(8) + "Name");
			Console.Write(Program.Indent(26) + "Category");
			Console.Write(Program.Indent(12) + "Price");
			Console.WriteLine(Program.Indent(11) + "Quantity");
			Console.WriteLine("---------------------------------------------------------------------------------------");


			for (int i = 0; i < ShoppingSystem.PListSize; i++)
			{

				if (ShoppingSystem.PList[i].Quantity > threshold)
				{

                    ShoppingSystem.PList[i].PrintProduct();
					Console.WriteLine();
				}
			}
			Console.WriteLine();
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadKey();
			AdminMenu();


		}

		public void Add_Customer()
		{
			string UserInput;
			string user_Name, password, address;
			if (ShoppingSystem.CListSize >= 100)
			{
				Console.WriteLine("The system is full, You can't add more Customers!");
				Console.WriteLine("Press any key to retrun to menu");
				AdminMenu();
			}
			Console.WriteLine("Enter Name of Customer to be Added: ");
			user_Name = Console.ReadLine();
			Console.WriteLine("Password: ");
			password = Console.ReadLine();
			Console.WriteLine("Address: ");
			address = Console.ReadLine();
		tryagain:
			Console.WriteLine("Credit: ");
			UserInput = Console.ReadLine();
			if (!double.TryParse(UserInput, out double credit))
			{
				Console.WriteLine("Credit entered is in wrong formate(not a number), please try again\n ");
				goto tryagain;
			}
            ShoppingSystem.CList[ShoppingSystem.CListSize] = new Customer(user_Name, password, address, credit);
            ShoppingSystem.CListSize++;
			ShoppingSystem.Update();//save changes to file
			Console.WriteLine("Customer Added Successfully!\n\n");
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadLine();
			AdminMenu();
		}

		public void Delete_Customer()
		{
			if (ShoppingSystem.CListSize <= 0)
			{
				Console.WriteLine("The system is empty, There is no Cutomers to Delete");
				Console.WriteLine("Press any key to retrun to menu");
				AdminMenu();
			}
			bool found = false;
			string customer_Name;
			Console.WriteLine("Enter Name of customer to be Deleted: ");
			customer_Name = Console.ReadLine();
			for (int i = 0; i < ShoppingSystem.CListSize; i++)
			{
				if (ShoppingSystem.CList[i].User_Name == customer_Name)
				{
					found = true;
                    Console.WriteLine("Customer Deleted Successfully\n");
                    for (int j = 0; j < ShoppingSystem.CListSize; j++)
                    {
                        ShoppingSystem.CList[j] = ShoppingSystem.CList[j + 1];//shift the array
                       
                        
                    }
           
                    ShoppingSystem.CListSize--;
                }
			}
			if (!found)
			{
				string input;
				Console.WriteLine("No customer found with the specified name\n");
				Console.WriteLine("Press 1 To try again");
				Console.WriteLine("Press any key To return to menu");
				input = Console.ReadLine();
				switch (input)
				{
					case "1":
						Delete_Customer();
						break;
					default:
						AdminMenu();
						break;
				}


			}
			ShoppingSystem.Update();//save changes to file
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadLine();
			AdminMenu();
		}

		public void Add_Product()
		{
			if (ShoppingSystem.PListSize >= 100)
			{
				Console.WriteLine("The system is full, You can't add more Products!");
				Console.WriteLine("Press any key to retrun to menu");
				AdminMenu();
			}
			string UserInput;
			string id, name, category;
			int quantity;
			Console.WriteLine("Enter Id: ");
			id = Console.ReadLine();
			Console.WriteLine("Enter Name: ");
			name = Console.ReadLine();
			Console.WriteLine("Enter Category: ");
			category = Console.ReadLine();
		tryagain:
			Console.WriteLine("Price: ");
			UserInput = Console.ReadLine();
			if (!double.TryParse(UserInput, out double price))
			{
				Console.WriteLine("Price entered is in wrong formate(not a number), please try again\n ");
				goto tryagain;
			}
			Console.WriteLine("Enter Price: ");
			price = Convert.ToDouble(Console.ReadLine());
			Console.WriteLine("Enter Quantity: ");
			quantity = Convert.ToInt32(Console.ReadLine());

            ShoppingSystem.PList[ShoppingSystem.PListSize] = new Product(id, name, category, price, quantity);
            ShoppingSystem.PListSize++;
			ShoppingSystem.Update();//save changes to file
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadLine();
			AdminMenu();

		}

		public void Delete_Product()
		{
			bool found = false;
			string product_ID;
			if (ShoppingSystem.PListSize <= 0)
			{
				Console.WriteLine("The system is empty, There is no Products to Delete!");
				Console.WriteLine("Press any key to retrun to menu");
				AdminMenu();
			}
			Console.WriteLine("Enter Product ID: ");
			product_ID = Console.ReadLine();
			for (int i = 0; i < ShoppingSystem.PListSize; i++)
			{
				if (ShoppingSystem.PList[i].Id == product_ID)
				{
                    found = true;
                    Console.WriteLine("Product Deleted Successfully\n");
                    for (int j = 0; j < ShoppingSystem.PListSize; j++)
                    {
                        ShoppingSystem.PList[j] = ShoppingSystem.PList[j + 1];//shift the array


                    }

                    ShoppingSystem.PListSize--;
                }
			}
			if (!found)
			{
				string input;
				Console.WriteLine("No Product found with the specified ID\n");
				Console.WriteLine("Press 1 To try again");
				Console.WriteLine("Press any key To return to menu");
				input = Console.ReadLine();
				switch (input)
				{
					case "1":
						Delete_Product();
						break;
					default:
						AdminMenu();
						break;
				}


			}
			ShoppingSystem.Update();//save chanes to file
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadLine();
			AdminMenu();

		}

		//View all the orders that are related to specific customers.
		public void View_CustomerOrders()
		{
			string CustomerName;
			Customer temp = new Customer();
			Console.WriteLine("Enter Customer Name: ");
			CustomerName = Console.ReadLine();
			Console.WriteLine();
			bool found = false;
			for (int i = 0; i < ShoppingSystem.CListSize; i++)//Search for the enterd Customer Name
			{
				if (ShoppingSystem.CList[i].User_Name == CustomerName)
				{
					temp = ShoppingSystem.CList[i];
					found = true;
					break;
				}

			}
			if (found)
			{
				if (temp.OrderListSize == 0)
				{
					Console.WriteLine("No orders yet\n");
					Console.WriteLine("Press any key to return to menu...");
					Console.ReadKey();
					AdminMenu();
				}
				else//OrderListSize != 0
				{
					for (int i = 0; i < temp.OrderListSize; i++)
					{
						Console.WriteLine("Order#" + (i + 1) + " info:");
						temp.OrdersList[i].PrintOrderes();
						Console.WriteLine();

					}
					Console.WriteLine("Press any key to return to menu...");
					Console.ReadKey();
					AdminMenu();
				}
			}

			else//Not found
			{
				Console.WriteLine("No user found with the specified name\n");
				Console.WriteLine("Press any key to return to menu...");
				Console.ReadLine();
				AdminMenu();
			}
		}

		public void SeachProduct_by_ID()
		{
			string id;
			bool found = false;
			Product temp = new Product();
			Console.Write("Enter ID: ");
			id = Console.ReadLine();
			Console.WriteLine();
			for (int i = 0; i < ShoppingSystem.PListSize; i++)//Seach for the entered produt ID
			{
				if (ShoppingSystem.PList[i].Id == id)
				{
					found = true;
					temp = ShoppingSystem.PList[i];

				}

			}
			if (found)
			{
				temp.PrintProduct();
				Console.WriteLine("Press any key to return to menu:");
				Console.ReadKey();
				AdminMenu();
			}
			else//not found
			{
				Console.WriteLine("No product found with the specified name\n");
				Console.WriteLine("Press any key to return to menu...");
				Console.ReadKey();
				AdminMenu();
			}
		}


	}
}
