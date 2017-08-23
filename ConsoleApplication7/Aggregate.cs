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
    class Aggregate
    {
        //The following example uses the Average method to find the average list price of the products.
        public void Average()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Decimal averageListPrice =
                    context.Products.Average(p => p.ListPrice);

                Console.WriteLine("The average list price of all the product is ${0}", averageListPrice);
            }
        }

        //The following example uses the Average method to find the average list price of the products of each style.
        public void Average2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from product in context.Products
                            group product by product.Style into g
                            select new
                            {
                                style = g.Key,
                                averageListPrice = g.Average(s => s.ListPrice)
                            };
                foreach(var prodyct in query)
                {
                    Console.WriteLine("Product Style: {0} \t Average List Price: {1}", prodyct.style, prodyct.averageListPrice);
                }
            }
        }

        //The following example uses the Average method to find the average list price of the products of each style.
        public void Average3()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Decimal averageTotalDue =
                    context.SalesOrderHeaders.Average(o => o.TotalDue);
                Console.WriteLine("The average TotalDue is {0} ", averageTotalDue);
            }
        }

        //The following example uses the Average method to get the average total due for each credit card ID
        public void Average4()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.CreditCardID into g
                            select new
                            {
                                Category = g.Key
                               ,
                                averagetotaldue = g.Average(order => order.TotalDue)
                            };
                foreach(var order in query)
                {
                    Console.WriteLine("Credit Card ID: {0} \t Average TotalDue {1}"
                        , order.Category, order.averagetotaldue);
                }
            }
        }

        //The following example uses the Average method to get the orders with the average total due for each Sales person ID
        public void Average5()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.SalesPersonID into g
                            let averagerTotalDue = g.Average(order => order.TotalDue)
                            select new
                            {
                                category = g.Key 
                                ,CheapestProduct = g.Where(order => order.TotalDue == averagerTotalDue)
                            };

                foreach(var ordergroup in query)
                {
                    Console.WriteLine("Sales Person ID: {0}", ordergroup.category);
                    foreach(var order in ordergroup.CheapestProduct)
                    {
                        Console.WriteLine("Average total due for SalesOrderID {1} is: {0}", order.TotalDue, order.SalesOrderID);
                    }
                    Console.WriteLine("\n");
                }
            }
        }


        //coUNT
        //The following example uses the Count method to return the number of products in the Product table.
        public void count()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                int numProduct = context.Products.Count();
                Console.WriteLine("There are {0} products.", numProduct);
            }
        }

        //The following example uses the Count method to return a list of Product and how many orders each has.
        public void count2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from product in context.Products
                            select new
                            {
                                productID = context.Products,
                                orderCount = context.SalesOrderHeaders.Count()
                            };

                foreach(var product in query)
                {
                    Console.WriteLine("Product = {0} \t Order Count = {1}", product.productID, product.orderCount);
                }
            }
        }

        //The following example groups products by color and uses the Count method to return the number of products in each color group.
        public void count3()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from products in context.Products
                            group products by products.Color into g
                            select new
                            {
                                color = g.Key,
                                productcount = g.Count()
                            };
                foreach (var product in query)
                {
                    Console.WriteLine("Color = {0} \t\t Product Count = {1}", product.color, product.productcount);
                }
            }
        }


        //LONG COUNT
        //The following example gets the product count as a long integer.
        public void longcount()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                long productnumber = context.Products.LongCount();
                Console.WriteLine("There are {0} Products", productnumber);
            }
        }

        //MAX
        //The following example uses the Max method to get the largest total due.
        public void max()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Decimal maxtotaldue = context.SalesOrderHeaders.Max(m => m.TotalDue);
                Console.WriteLine("The Maximum TotalDue is {0}", maxtotaldue);
            }
        }

        //The following example uses the Max method to get the largest total due for each Sales Order ID.
        public void max2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.SalesOrderID into g
                            select new
                            {
                                category = g.Key,
                                maxtotaldue = g.Max(order => order.TotalDue)
                            };

                foreach(var order in query)
                {
                    Console.WriteLine("Product: {0} \t maximum  TotalDue = {1}", order.category, order.maxtotaldue);
                }
            }
        }

        //The following example uses the Max method to get the orders with the largest total due for each Sales Person ID.
        public void max3()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.SalesPersonID into g
                            let maxtotaldue = g.Max(order => order.TotalDue)
                            select new
                            {
                                category = g.Key,
                                cheapestProduct = g.Where(order => order.TotalDue == maxtotaldue)
                            };

                foreach (var ordergroup in query)
                {
                    Console.WriteLine("Sales Person ID: {0}", ordergroup.category);
                    foreach(var order in ordergroup.cheapestProduct)
                    {
                        Console.WriteLine("MaxTotalDue {0} for Shipment Method ID {1}", order.TotalDue, order.ShipMethodID);
                    }
                    Console.WriteLine("\n");
                }
            }
        }


        //MIN
        //The following example uses the Min method to get the smallest total due.
        public void min()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Decimal smalltotaldue = context.SalesOrderHeaders.Min(total => total.TotalDue);
                Console.WriteLine("The Smallest TotalDue is {0}", smalltotaldue);
            }
        }

        //The following example uses the Min method to get the smallest total due for each contact ID.
        public void min2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.CreditCardID into g
                            select new
                            {
                                category = g.Key,
                                smalltotaldue = g.Min(total => total.TotalDue)
                            };

                foreach (var order in query)
                {
                    Console.WriteLine("CreditCard ID {0} \t Minimum TotalDUe = {1}", order.category, order.smalltotaldue);
                }
            }
        }

        //The following example uses the Min method to get the orders with the smallest total due for each CreditCard ID.
        public void min3()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.CreditCardID into g
                            let mintotaldue = g.Min(s => s.TotalDue)
                            select new
                            {
                                category = g.Key,
                                smalltotaldue = g.Where(s => s.TotalDue == mintotaldue)
                            };

                foreach(var order in query)
                {
                    Console.WriteLine("CreditCard ID {0}", order.category);
                    foreach (var another in order.smalltotaldue)
                    {
                        Console.WriteLine("Minimum TotalDue {0} for Sales Order ID {1}", another.TotalDue, another.SalesOrderID);
                    }
                    Console.WriteLine("\n");
                }

            }
        }


        //SUM
        //The following example uses the Min method to get the orders with the smallest total due for each contact.
        public void sum()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                double totalorderqty = context.SalesOrderDetails.Sum(o => o.OrderQty);
                Console.WriteLine("There are {0} total of OrderQty", totalorderqty);
            }
        }

        //The following example uses the Sum method to get the total due for each Creditcard ID.
        public void sum1()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = from order in context.SalesOrderHeaders
                            group order by order.CreditCardID into g
                            select new
                            {
                                category = g.Key,
                                totaldue = g.Sum(a => a.TotalDue)
                            };

                foreach (var order in query)
                {
                    Console.WriteLine("CreditCardID {0} \t TotalDue Sum : {1}", order.category, order.totaldue);
                }

            }
        }

    }
}
