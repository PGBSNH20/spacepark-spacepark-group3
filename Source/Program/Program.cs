using System;
using SpacePark;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Class1 c = new Class1();
            Console.WriteLine(c.Test());

            using (var ctx = new Context())
            {
                var user = new User() {
                    Id = 1,
                    Name = "Luke Skywalker",                    
                    ArrivalTime = DateTime.Now,
                    DepartureTime = DateTime.Now.AddHours(5),
                    PaymentAmount = 100,
                
                };

                ctx.Users.Add(user);
                ctx.SaveChanges();
            }
        }
    }
}
