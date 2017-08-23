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
    class Join_Operators
    {
        //GROUP JOIN
        //The following example performs a GroupJoin over the SalesOrderHeader and SalesOrderDetail tables to find the number of orders per customer. 
        //A group join is the equivalent of a left outer join, which returns each element of the first (left) data source, 
        //even if no correlated elements are in the other data source.
        public void groupjoin()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.SalesOrderHeaders.GroupJoin(context.SalesOrderDetails,
                    order => order.SalesOrderID,
                    detail => detail.SalesOrderID,
                    (Order, OrderGroup) => new
                    {
                        CustomerID = Order.SalesOrderID,
                        OrderCount = OrderGroup.Count()
                    });
                foreach(var order in query)
                {
                    Console.WriteLine("Customer ID: {0} \t Order Count: {1}", order.CustomerID, order.OrderCount);
                }
            }
        }

        //The following example performs a GroupJoin over the Contact and SalesOrderHeader tables to find the number of orders per contact. 
        //The order count and IDs for each contact are displayed.
        public void groupjoin2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.Products.GroupJoin(context.SalesOrderDetails,
                    product => product.ProductID,
                    order => order.ProductID,
                    (product, productgroup) => new
                    {
                        ProductID = product.ProductID,
                        OrderCount = productgroup.Count(),
                        Orders = productgroup
                    });
                
                foreach(var group in query)
                {
                    Console.WriteLine("ProductID: {0}", group.ProductID);
                    Console.WriteLine("Order Count: {0}", group.OrderCount);

                    foreach(var orderinfo  in group.Orders)
                    {
                        Console.WriteLine("Sales ID: {0}", orderinfo.SalesOrderID);
                    }
                    Console.WriteLine("");
                } 

            }
        }


        //JOIN
        //The following example performs a join over the Product and SalesOrderHeader tables.
        public void join()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.Products.Join(
                            context.SalesOrderHeaders,
                            order => order.ProductID,
                            sales => sales.CreditCardID,
                            (sales, order) => new
                            {
                                ProductID = sales.ProductID,
                                CreditCardID = order.CreditCardID,
                                TotalDue = order.TotalDue
                            });

                foreach(var order in query)
                {
                    Console.WriteLine("Product ID: {0}"
                                      + "\t Credit Card ID: {1}"
                                      + "\t Total Due: {2}",
                                      order.ProductID,
                                      order.CreditCardID,
                                      order.TotalDue);
                }

            }
        }

        //The following example performs a join over the Product and SalesOrderHeader tables, grouping the results by contact ID.
        public void join2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.Products.Join(
                            context.SalesOrderHeaders,
                            order => order.ProductID,
                            card => card.CreditCardID,
                            (card, order) => new
                            {
                                ProductID = card.ProductID,
                                CreditCardID = order.CreditCardID,
                                TotalDue = order.TotalDue
                            })
                            .GroupBy(record => record.ProductID);

                foreach(var group in query)
                {
                    foreach(var progroup in group)
                    {
                        Console.WriteLine("Product ID: {0}"
                                         + "\t CreditCardID ID: {1}"
                                         + "\t TotalDue: {2}",
                                         progroup.ProductID,
                                         progroup.CreditCardID,
                                         progroup.TotalDue);
                    }
                }
            }
        }
    }
}
