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
    class Partitioning
    {
        //SKIP
        //The following example uses the Skip method to get all but the first five contacts of the Contact table.
        public void skip()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                //LINQ to entities only supports SKIP on ordered collections
                IOrderedQueryable<Product> product = context.Products
                    .OrderBy(p => p.ListPrice);
                IQueryable<Product> AllAboutFirst3Product = product.Skip(3);

                Console.WriteLine("All about first 3 product: ");

                foreach(Product productS in AllAboutFirst3Product)
                {
                    Console.WriteLine("Name: {0} \t ID: {1}", productS.Name, productS.ProductID);
                }
            }
        }

        //The following example uses the Skip method to get all but the first two addresses in Seattle.
        public void skip2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = (from address in context.Addresses
                             from order in context.SalesOrderHeaders
                             where address.AddressID == order.Address.AddressID
                             && address.City == "Seattle"
                             orderby order.SalesOrderID
                             select new
                             {
                                 City = address.City,
                                 OrderID = order.SalesOrderID,
                                 OrderDate = order.OrderDate
                             }).Skip(2);
                Console.WriteLine("All but first 2 orders in Seattle");
                foreach (var o in query)
                {
                    Console.WriteLine("City: {0} \t OrderID: {1} \t TotalDue: {2:d}", o.City, o.OrderID, o.OrderDate);
                }
            }
        }


        //TAKE
        //The following example uses the Take method to get only the first five contacts from the Contact table.
        public void take()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Console.WriteLine("First 5 Product");
                foreach(Product product in context.Products.Take(5))
                {
                    Console.WriteLine("Product ID: {0} \t Product Number: {1}", product.ProductID, product.ProductNumber);
                }
            }
        }

        //The following example uses the Take method to get the first three addresses in Seattle.
        public void take2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                string seattle = "Seattle";

                var query = (from address in context.Addresses
                             from order in context.SalesOrderHeaders
                             where address.AddressID == order.Address.AddressID
                             && address.City == seattle
                             select new
                             {
                                 City = address.City,
                                 OrderID = order.SalesOrderID,
                                 OrderDate = order.OrderDate
                             }).Take(3);

                Console.WriteLine("First 3 order in Seattle");

                foreach(var order in query)
                {
                    Console.WriteLine("City: {0} \t Order ID: {1} \t Order Date: {2}", order.City, order.OrderID, order.OrderDate);
                }
            }
        }
    }
}
