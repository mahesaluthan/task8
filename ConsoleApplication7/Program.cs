using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;

namespace ConsoleApplication7
{
    class Program
    {
        static void Main(string[] args)
        {
            Filtering filter = new Filtering();
            Ordering order = new Ordering();
            Aggregate aggre = new Aggregate();
            Partitioning part = new Partitioning();
            Conversion conv = new Conversion();
            Join_Operators join = new Join_Operators();
            ElementOperator elemnt = new ElementOperator();
            Grouping groups = new Grouping();
            NavRelationship nav = new NavRelationship();
            Projection pro = new Projection();
            pro.select3();
            Console.ReadLine();
        }
    }
}
