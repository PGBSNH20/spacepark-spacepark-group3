using System;
using System.IO;
using SpacePark.DB;
using SpacePark.Models;
using SpacePark.DB.Models;
using SpacePark.Configuration;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            Config config = new AppConfig().GetConfig();
            // using (var ctx = new AppDbContext())
            // {
            //     var user = new DBUser() {
            //         Id = 1,
            //         Name = "Luke Skywalker",                    
            //         ArrivalTime = DateTime.Now,
            //         DepartureTime = DateTime.Now.AddHours(5),
            //         PaymentAmount = 100,
                
            //     };

            //     ctx.Users.Add(user);
            //     ctx.SaveChanges();
            // }
        }
    }
}
