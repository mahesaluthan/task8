using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;
using System.Globalization;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Data.Common;

namespace ConsoleApplication7
{
    class Conversion
    {
        //TO ARRAY
        //The following example uses the ToArray method to immediately evaluate a sequence into an array.
        public void toarray()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Product[] prodArray = (
                    from product in context.Products
                    orderby product.ListPrice descending
                    select product).ToArray();

                Console.WriteLine("Price From Higher to Lowest");
                foreach(Product p in prodArray)
                {
                    Console.WriteLine("Product Name: {0} \t Price: {1}", p.Name, p.ListPrice);
                }
            }
        }


        //TO DICTIONARY
        //The following example uses the ToDictionary method to immediately 
        //evaluate a sequence and a related key expression into a dictionary.
        public void todictionary()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Dictionary<String, Product> ScoreRecordDic = context.Products.
                    ToDictionary(record => record.Name);
                Console.WriteLine("Top Tube's ProductID: {0}", ScoreRecordDic["Top Tube"].Name);
            }
        }


        //TO LIST
        //The following example uses the ToList method to immediately evaluate a sequence into a List<T>, where T is of type DataRow.
        public void tolist()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                List<Product> query = (from product in context.Products
                                       orderby product.Name
                                       select product).ToList();
                Console.WriteLine("The Product list, ordered by product name: ");
                foreach (Product product in query)
                {
                    Console.WriteLine(product.Name.ToLower(CultureInfo.InvariantCulture));
                }
            }
        }
    }
}
