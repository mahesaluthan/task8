using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Globalization;


namespace ConsoleApplication7
{
    class Ordering
    {
        //ASCENDING

        //he following example in method-based query syntax uses OrderBy and ThenBy to return 
        //a list of contacts ordered by last name and then by first name.
        public void thenby1()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                IQueryable<ContactType> sortedContact = context.ContactTypes
                    .OrderBy(c => c.Name)
                    .ThenBy(c => c.ModifiedDate);

                Console.WriteLine("The list of the contact sorted by Name then Modified Date: ");

                foreach (ContactType sorted in sortedContact)
                {
                    Console.WriteLine(sorted.Name + "," + sorted.ModifiedDate);
                }
            }
        }

        //DESCENDING

        //The following example uses the OrderBy and ThenByDescending methods to first sort by list price, 
        //and then perform a descending sort of the product names.
        public void thenbydescending()
        {
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                IQueryable<Product> query = context.Products
                    .OrderBy(p => p.ListPrice)
                    .ThenByDescending(p => p.Name);

                foreach (Product product in query)
                {
                    Console.WriteLine("Product ID: {0} Product Name: {1} List Price {2}"
                        , product.ProductID
                        , product.Name
                        , product.ListPrice);
                }
            }
        }
    }
}
