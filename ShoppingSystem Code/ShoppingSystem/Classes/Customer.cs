using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Threading;

namespace ShoppingSystem
{
	
	[Serializable]
	public class Customer
	{
		string user_Name;
		string password;
		string address;
		double credit;
		int ordersListSize;

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

		public string Addresse
		{
			set { address = value; }
			get
			{ return address; }
		}

		public double Credit
		{
			set { credit = value; }
			get
			{ return credit; }
		}

		public int OrderListSize
		{
			set
			{ ordersListSize = value; }
			get
			{ return ordersListSize; }

		}

		//	Creates A list of Orders of size 100
		public Order[] OrdersList = new Order[100];

		public void CustomerMenu()
		{
			Console.Clear();


			Console.WriteLine();

			string choicex;
			Console.WriteLine();
			Console.WriteLine("1- View all products");
			Console.WriteLine("2- Search for a product based on the Product_Category.");
			Console.WriteLine("3- Make orders of the products from a shop.");
			Console.WriteLine("4- Add cash credit");
			Console.WriteLine("5- Back to login screen.");
			Console.WriteLine("\n Enter your choice");
			choicex = Console.ReadLine();

			switch (choicex)
			{
				case "1":
					{
						Console.Clear();
						Print_Products();
						Console.WriteLine("Press any key to return to menu...");
						Console.ReadKey();
						CustomerMenu();
						break;
					}

				case "2":
					{
						Console.Clear();
						Search_Based_on_Category();
						Console.WriteLine("\nPress any key to return to menu...");
						Console.ReadKey();
						CustomerMenu();
						break;


					}

				case "3":
					Console.Clear();
					Make_Order();
					CustomerMenu();
					break;

				case "4":
					Console.Clear();
					Add_Credit();
					CustomerMenu();
					break;

				case "5":
					break;




			}

		}

		public Customer()//For temp
		{

		}

		public Customer(string user_Name, string password, string address, double Credit)
		{


			this.user_Name = user_Name;
			this.password = password;
			this.address = address;
			this.credit = Credit;
		}

		
		public void PrintCustomer()

		{


			Console.WriteLine(String.Format("{0,-10} | {1,-10} | {2,-10}  |{3,7}JD", user_Name, password, address, credit.ToString("N0")));

		}

		public void Print_Products()
		{
			Console.WriteLine("Products in the system ");
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
                ShoppingSystem.PList[i].PrintProduct();
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		public void Search_Based_on_Category()

		{
			string Category;
			Console.WriteLine("Enter a Category: \n");
			Category = Console.ReadLine();
			bool found = false;
			Console.WriteLine("Products with {0} Category:\n", Category);
			Console.WriteLine("---------------------------------------------------------------------------------------");
			Console.Write(Program.Indent(0) + "ID");
			Console.Write(Program.Indent(8) + "Name");
			Console.Write(Program.Indent(26) + "Category");
			Console.Write(Program.Indent(12) + "Price");
			Console.WriteLine(Program.Indent(11) + "Quantity");
			Console.WriteLine("---------------------------------------------------------------------------------------");
			for (int i = 0; i < ShoppingSystem.PListSize; i++)
			{
				if (ShoppingSystem.PList[i].Catagory == Category)
				{
                    ShoppingSystem.PList[i].PrintProduct();
					found = true;
					Console.WriteLine();
				}

			}

			if (!found)
			{
				Console.WriteLine("There are no products related to the entered category\n");
			}


		}

		
		public void Make_Order()
		{
			Console.Clear();
			string UserInput;
			bool found = false;
			Product temp = new Product();
			string Id;
			Console.WriteLine("Enter Product ID: ");
			Id = Console.ReadLine();

		tryagain:
			Console.WriteLine("Enter Quantity: ");
			UserInput = Console.ReadLine();
			if (!int.TryParse(UserInput, out int Quantity))
			{
				Console.WriteLine("Quantity entered is in wrong formate(not a number), please try again\n ");
				goto tryagain;
			}

			double cost;
			for (int i = 0; i < ShoppingSystem.PListSize; i++)//search for product
			{
				if (ShoppingSystem.PList[i].Id == Id)
				{
					found = true;
					temp = ShoppingSystem.PList[i];

				}
			}

			if (found)
			{
				cost = temp.Price * Quantity;
				if (temp.Quantity < Quantity)//No enough Quantity available
				{
					Console.WriteLine("No enough quantity available for the desired product, please again \n ");
					Console.WriteLine("-Press 1 to try again ");
					Console.WriteLine("-Press any key to return to menu\n ");
					Console.WriteLine("\n Enter your choice");
					string choice = Console.ReadLine();
					switch (choice)
					{
						case "1": Make_Order(); break;
						default:
							break;
					}
				}

				else if (Credit < cost)//No enough credit
				{
					string choice;
					Console.WriteLine("You dont have enough credit to purchse!.\n");
					Console.WriteLine("1-Add credit");
					Console.WriteLine("2-choose diffrent Product");
					Console.WriteLine("press 3 or any key to return to menu");
					choice = Console.ReadLine();
					switch (choice)
					{
						case "1":
							Add_Credit();
							Make_Order();
							break;
						case "2":
							Make_Order();
							break;
						default:
							break;

					}

				}

				else//All goes well
				{
					string choice;
					Console.WriteLine("You order total is {0}JD\n:	", cost);
				tryagains:
					Console.WriteLine("Press Y to finish your order");
					Console.WriteLine("Press N or any key to return to menu");
					choice = (Console.ReadLine());
					switch (choice)
					{
						case "Y":
						case "y":
							{
								string Order_Date = DateTime.Now.ToString("yyyy.MM.dd");//Get the date of today
								string Order_ID = User_Name + Order_Date.Substring(5, 2);
								OrdersList[ordersListSize] = new Order(Order_ID, Order_Date);
								ordersListSize++;
								credit -= cost;
								temp.Quantity -= Quantity;
								Console.WriteLine("\n\t\tCurrent Credit={0}JD\n", credit.ToString("N0"));
								Console.WriteLine("Order Completed thanks for using our system!\n");
								Console.WriteLine("Your Order details:\n");
								OrdersList[OrderListSize - 1].PrintOrderes();
								Console.WriteLine();
								ShoppingSystem.Update();
								Console.WriteLine("Press any key to retun to menu");
								Console.ReadLine();


							}
							break;
						case "N":
						case "n":
							break;
						default:
							Console.WriteLine("Enter A valid key!");
							goto tryagains;


					}

				}


			}

			else//In case no product found 
			{
				Console.WriteLine("No Product found with the specified ID,\n ");
				Console.WriteLine("-Press 1 to try again ");
				Console.WriteLine("-Press any key to return to menu\n ");
				Console.WriteLine("\n Enter your choice");
				string choice = Console.ReadLine();
				switch (choice)
				{
					case "1": Make_Order(); break;
					default:
						break;
				}
			}






		}

		public void Add_Credit()
		{
			double Credit;
			Console.WriteLine("Enter Credit amount to be added");
			Credit = Convert.ToDouble(Console.ReadLine());
			this.Credit += Credit;
			Console.WriteLine("Credit updated successfully:		Current Credit ={0}$", credit.ToString("N0"));
			ShoppingSystem.Update();
			Console.WriteLine("Press any key to return to menu...");
			Console.ReadLine();


		}
	}
}
