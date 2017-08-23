using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;

namespace ConsoleApplication7
{
    class Filtering
    {
        //WHERE

        //The following example returns all online orders.
        public void where1()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var onlineOrder = context.SalesOrderHeaders
                    .Where(order => order.OnlineOrderFlag == true)
                    .Select(s => new { s.SalesOrderID, s.OrderDate, s.SalesOrderNumber });

                foreach (var oo in onlineOrder)
                {
                    Console.WriteLine("Order ID: {0} Order Date: {1} Order Number: {2}"
                        , oo.SalesOrderID
                        , oo.OrderDate
                        , oo.SalesOrderNumber);
                }
            }
        }

        //The following example returns the orders where the order quantity is greater than 2 and less than 6.
        public void where2()
        {
            int ordermin = 2;
            int ordermax = 6;

            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.SalesOrderDetails
                    .Where(order => order.OrderQty > ordermin && order.OrderQty < ordermax)
                    .Select(s => new { s.SalesOrderID, s.OrderQty });

                foreach(var order in query)
                {
                    Console.WriteLine("Order ID: {0} Order Quantity: {1}"
                        , order.SalesOrderID, order.OrderQty);
                }
            }
        }

        //The following example returns all red colored products.
        public void where3()
        {
            string color = "Red";

            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.Products
                    .Where(P => P.Color == color)
                    .Select(p => new { p.Name, p.ProductNumber, p.ListPrice });

                foreach(var product in query)
                {
                    Console.WriteLine("Name: {0}",product.Name);
                    Console.WriteLine("Product Number: {0}",product.ProductNumber);
                    Console.WriteLine("List Price : {0}",product.ListPrice);
                    Console.WriteLine("");
                }

            }
        }

        //The following example uses the Where method to find orders that were made after December 1, 2003 
        //and then uses the order.SalesOrderDetail navigation property to get the details for each order.
        public void where4()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                IQueryable<SalesOrderHeader> query = context.SalesOrderHeaders
                    .Where(order => order.OrderDate >= new DateTime(2003, 12, 1));

                Console.WriteLine("Order that were made after December 1, 2003: ");
                foreach(SalesOrderHeader order in query)
                {
                    Console.WriteLine("Order ID: {0} Order Date: {1:d}"
                        , order.SalesOrderID, order.OrderDate);
                    foreach(SalesOrderDetail detail in order.SalesOrderDetails)
                    {
                        Console.WriteLine("Product ID: {0} Unit Price: {1}"
                            , detail.ProductID, detail.UnitPrice);
                    }
                }
            }
        }

        //WHERE COUNTAINS

        //The following example uses an array as part of a Where…Contains clause to find 
        //all products that have a ProductModelID that matches a value in the array.
        public void contains1()
        {
            using (AdventureWorks2014Entities AWEntities = new AdventureWorks2014Entities())
            {
                int?[] productModelIds = { 19, 26, 118 };
                var products = AWEntities.Products
                    .Where(p => productModelIds.Contains(p.ProductModelID));

                foreach(var p in products)
                {
                    Console.WriteLine("ModelID: {0} = ID: {1}", p.ProductModelID, p.ProductID);
                }
            }
        }

        //The following example declares and initializes arrays in a Where…Contains clause to find 
        //all products that have a ProductModelID or a Size that matches a value in the arrays.
        public void contains2()
        {
            using (AdventureWorks2014Entities AWEntities = new AdventureWorks2014Entities())
            {
                var products = AWEntities.Products
                    .Where(p => (new int?[] { 19, 26, 18 }).Contains(p.ProductModelID) ||
                                (new string[] { "L", "XL","M","S" }).Contains(p.Size));

                foreach (var P in products )
                {
                    Console.WriteLine("ID {0}: ModelID {1}, Size {2}"
                                        , P.ProductID
                                        , P.ProductModelID
                                        , P.Size);
                }
            }
        }
    }
}
