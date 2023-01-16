using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WACO
{
    public class ConsumptionException: Exception
    {
        public ConsumptionException() : base("Wrong number of periods")
        {

        }
    }
}
