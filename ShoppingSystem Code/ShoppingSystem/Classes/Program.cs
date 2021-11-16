using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Threading;

namespace ShoppingSystem
{
	class Program
	{

		
		public static string Indent(int count)//For formating the print Functions
		{
			return "".PadLeft(count);
		}



		
		public static void Main(string[] args)
		{


            BinaryFormatter bs = new BinaryFormatter();

            //Load Customers from file and store in array
            FileStream fc = new FileStream("Customerx", FileMode.Open, FileAccess.Read);
            while (fc.Position < fc.Length)
            {
                ShoppingSystem.CList[ShoppingSystem.CListSize] = (Customer)bs.Deserialize(fc);
                ShoppingSystem.CListSize++;
            }
            fc.Close();

            //Load Products from file and store in array
            FileStream fp = new FileStream("Productx", FileMode.Open, FileAccess.Read);
            while (fp.Position < fp.Length)
            {
                ShoppingSystem.PList[ShoppingSystem.PListSize] = (Product)bs.Deserialize(fp);
                ShoppingSystem.PListSize++;
            }
            fp.Close();

            ShoppingSystem.Login();


















        }
	}
}
