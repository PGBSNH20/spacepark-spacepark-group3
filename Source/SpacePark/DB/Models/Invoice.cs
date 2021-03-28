using System;
namespace SpacePark.Models
{
    public class Invoice
    {
        public DateTime StartedTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal HourlyPrice { get; set; }
        public decimal TotalCost { get; set; }

        public Invoice(DateTime startedTime, DateTime endTime, decimal hourlyPrice)
        {
            StartedTime = startedTime;
            EndTime = endTime;
            HourlyPrice = hourlyPrice;
        }

        public Invoice CalculateCost()
        {
            // Using seconds as hours to simulate time passing by
            TotalCost = (decimal)(EndTime - StartedTime).TotalSeconds * HourlyPrice;
            return this;
        }
    }
}


