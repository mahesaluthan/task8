using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication7
{
    class ElementOperator
    {
        //FIRST
        //The following example uses the First method to find the first e-mail address that starts with 'caroline'.
        public void first()
        {
            string name = "mountain";
            using (AdventureWorks2014Entities context = new AdventureWorks2014Entities())
            {
                Product query = context.Products.First(
                                prod => prod.Name.StartsWith(name));
                Console.WriteLine("A Product starting with 'Mountain': {0}", query.Name);
            }
        }
    }
}
