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
using System.Threading;

namespace ConsoleApplication7
{
    class Projection
    {
        //SELECT
        //The following example uses the Select method to return all the rows from the Product table and display the product names.
        public void select()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                IQueryable<Product> ProdQeury = from product in context.Products select product;

                Console.WriteLine("Product Names: ");
                Thread.Sleep(1000);
                foreach(var prod in ProdQeury)
                {
                    Console.WriteLine(prod.Name);
                }
            }
        }

        //The following example uses Select to return a sequence of only product names.
        public void select2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                IQueryable<string> Prodname = 
                    from p in context.Products select p.Name;

                Console.WriteLine("Product Names: ");
                Thread.Sleep(1000);

                foreach (String prod in Prodname)
                {
                    Console.WriteLine(prod);
                }
            }
                
        }

        //The following example uses the Select method to project the Product.Name and Product.
        //ProductID properties into a sequence of anonymous types.
        public void select3()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from prod in context.Products
                            select new
                            {
                                ProdID = prod.ProductID,
                                ProdName = prod.Name
                            };
                Console.WriteLine("Product Info: ");
                Thread.Sleep(1000);

                foreach(var prodinfo in query)
                {
                    Console.WriteLine("Product ID: {0} \t Product Name: {1}",
                                        prodinfo.ProdID,
                                        prodinfo.ProdName);
                }
            }
        }


        //MANY SELECT
        //The following example uses From … From … (the equivalent of the SelectMany method) 
        //to select all orders where TotalDue is less than 500.00.
        /*public void many()
        {
            decimal totaldue = 500.00M;
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from prod in context.Products
                            from order in context.SalesOrderHeaders
                            where prod.ProductID == order.product
            }
        }*/
    }
}
