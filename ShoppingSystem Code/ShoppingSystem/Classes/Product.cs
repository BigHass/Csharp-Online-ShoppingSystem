using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Threading;
namespace ShoppingSystem
{
	
	[Serializable]
	public class Product
	{
		string id;
		string name;
		string catagory;
		double price;
		int quantity;

		//Set And Get
		public string Id
		{
			set { id = value; }

			get { return id; }

		}

		public string Name
		{
			set { name = value; }

			get
			{
				return name;
			}
		}

		public string Catagory
		{
			set
			{ catagory = value; }

			get
			{ return catagory; }
		}

		public double Price
		{
			set { price = value; }

			get
			{ return price; }
		}

		public int Quantity
		{
			set
			{ quantity = value; }

			get
			{ return quantity; }
		}

		public Product()
		{

		}//for temp

		public Product(string id, string name, string catagory, double price, int quantity)
		{
			this.id = id;
			this.name = name;
			this.catagory = catagory;
			this.price = price;
			this.quantity = quantity;
		}

		public void PrintProduct()
		{

			string priceLength = price.ToString();

			Console.Write(Program.Indent(0) + id);
			Console.Write(Program.Indent(8) + name);
			Console.Write(Program.Indent(15 + (15 - name.Length)) + catagory);
			Console.Write(Program.Indent(10 + (10 - catagory.Length)) + price);
			Console.Write(Program.Indent(8 + (8 - priceLength.Length)) + quantity);

		}

	}
}
