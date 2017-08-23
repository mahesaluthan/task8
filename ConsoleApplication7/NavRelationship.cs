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
    class NavRelationship
    {
        //NAVIGATING RELATIONSHIP
        //The following example uses the SalesOrderHeader.Address and SalesOrderHeader.
        //Contact navigation properties to get the collection of Address and Contact objects associated with each order. 
        //The last name of the contact, the street address, the sales order number, and the total due for each order to the 
        //city of Seattle are returned in an anonymous type.
        public void navcity()
        {
            string Seattle = "Seattle";
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.SalesOrderHeaders
                    .Where(o => o.Address.City == Seattle)
                    .Select(o => new
                    {
                        StreetAddress = o.Address.AddressLine1,
                        OrderNumber = o.SalesOrderID,
                        TotalDue = o.TotalDue
                    });

                foreach(var order in query)
                {
                    Console.WriteLine("Address: {0}",order.StreetAddress);
                    Console.WriteLine("Order Number: {0}",order.OrderNumber);
                    Console.WriteLine("Total Due: {0}",order.TotalDue);
                    Console.WriteLine("");
                }
            }
        }

        //The following example uses the Where method to find orders that were made after December 1, 2003, 
        //and then uses the order.SalesOrderDetail navigation property to get the details for each order.
        public void nav()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                IQueryable<SalesOrderHeader> query =
                    from order in context.SalesOrderHeaders
                    where order.OrderDate >= new DateTime(2003, 12, 1)
                    select order;

                Console.WriteLine("Order that We made after December 1, 2003");

                foreach (SalesOrderHeader order in query)
                {
                    Console.WriteLine("Order ID: {9} \t Order Date: {1:d}", order.SalesOrderID, order.OrderDate);
                    foreach(SalesOrderDetail details in order.SalesOrderDetails)
                    {
                        Console.WriteLine("Product ID: {0} \t Unir Price: {1}",details.ProductID,details.UnitPrice);
                    }
                }
            }
        }
    }
}
