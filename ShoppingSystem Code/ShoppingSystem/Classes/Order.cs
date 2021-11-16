using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Threading;

namespace ShoppingSystem
{
	
	[Serializable]
	public class Order
	{
		private string id;
		private string date;

		//Set And Get
		public string Id
		{
			set
			{ id = value; }

			get
			{ return id; }
		}
		public string Date
		{
			set
			{ date = value; }

			get
			{ return date; }
		}

		public void PrintOrderes()
		{
			Console.WriteLine("Order ID: " + id);
			Console.WriteLine("Order Date: " + date);
		}

		public Order(string id, string date)
		{
			this.id = id;
			this.date = date;
		}
	}
}
