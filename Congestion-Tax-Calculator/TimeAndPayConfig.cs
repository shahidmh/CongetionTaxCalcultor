using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Congestion_Tax_Calculator
{
    public class TimeAndPayDetails
    {
        public string Start { get; set; }
        public string End { get; set; }
        public int Amount { get; set; }
    }

    public class TimeAndPayConfig
    {
        public List<TimeAndPayDetails> timeAndPayDetails { get; set; }
    }
}
