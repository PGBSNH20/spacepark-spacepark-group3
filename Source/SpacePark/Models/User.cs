using System;

namespace SpacePark.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public double PaymentAmount { get; set; }
    }
}
