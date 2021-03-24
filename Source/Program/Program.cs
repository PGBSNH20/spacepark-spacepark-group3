using System;
using System.IO;
using SpacePark;
using SpacePark.Models;
using SpacePark.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {

            var ParkedUsers = new List<User>();
            Config config = new AppConfig().GetConfig();
            using (var ctx = new Context(config))
            {
                var user = new User() {
                    Id =4,
                    Name = "Luke Skywalker",                    
                    ArrivalTime = DateTime.Now,
                    DepartureTime = null,
                    PaymentAmount = 100.0,
                
                };

                ctx.Users.Add(user);
                ctx.SaveChanges();
            }

            using (var ctx = new Context(config))
            {
                var parkedUsers = ctx.Users.Where(user => !user.DepartureTime.HasValue);
                foreach(var parkedUser in parkedUsers)
                {
                    ParkedUsers.Add(parkedUser);
                }
            }
        }
    }
}
