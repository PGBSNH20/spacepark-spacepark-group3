using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpacePark.Models
{
    public class Invoice
    {
            public int InvoiceID { get; set; }
            public string ShipName { get; set; }
            public DateTime StarteTime { get; set; }
            public DateTime EndTime { get; set; }
            public double TotalTime { get; set; }
            public double HourlyPrice { get; set; }
            public double TotalPrice { get; set; }
        } 
}


