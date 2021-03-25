using SpacePark.Config;
using SpacePark.DB.Interfaces;
using SpacePark.DB.Queries;
using System;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {

            AppConfig config = new AppConfig().GetConfig();
            
            ConsoleGUI gui = new();
            gui.LoadGUI(config.Name);


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