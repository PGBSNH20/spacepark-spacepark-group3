using System;

namespace SpacePark.DB.Models
{
    public class DBUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public double PaymentAmount { get; set; }

    }
}