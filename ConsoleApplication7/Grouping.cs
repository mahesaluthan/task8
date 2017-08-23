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
    class Grouping
    {
        //GROUP
        //The following example uses the GroupBy method to return Address objects that are grouped by postal code. 
        //The results are projected into an anonymous type.
        public void group()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.Addresses
                    .GroupBy(address => address.PostalCode);

                foreach(IGrouping<string, Address> addressGroup in query)
                {
                    Console.WriteLine("Postal Code: {0}", addressGroup.Key);
                    foreach(Address add in addressGroup)
                    {
                        Console.WriteLine("\t" + add.AddressLine1 + add.AddressLine2);
                    }
                }
            }
        }

        //The following example uses the GroupBy method to return Contact objects that are grouped by the first letter 
        //of the contact's last name. The results are also sorted by the first letter of the last name and projected into an anonymous type.
        public void group2()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.Addresses
                    .GroupBy(g => g.City.Substring(0, 1))
                    .OrderBy(o => o.Key);

                foreach(IGrouping<string, Address> group in query)
                {
                    Console.WriteLine("City names that start with '{0}': ", group.Key);
                    foreach(Address add in group)
                    {
                        Console.WriteLine(add.City);
                    }
                }
            }
        }

        //The following example uses the GroupBy method to return SalesOrderHeader objects that are grouped by customer ID. 
        //The number of sales for each customer is also returned.
        public void group3()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                var query = context.SalesOrderHeaders
                    .GroupBy(o => o.CustomerID);

                foreach(IGrouping<int, SalesOrderHeader> sales in query)
                {
                    Console.WriteLine("Customer ID: {0}", sales.Key);
                    Console.WriteLine("Order Count: {0}", sales.Count());

                    foreach(SalesOrderHeader saless in sales)
                    {
                        Console.WriteLine("Sale ID: {0}", saless.SalesOrderID);
                    }
                    Console.WriteLine("");
                }
            }
        }
    }
}
