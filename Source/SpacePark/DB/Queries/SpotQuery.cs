using System.Linq;
using System.Collections.Generic;
using SpacePark.DB.Interfaces;
using SpacePark.DB.Models;

namespace SpacePark.DB.Queries
{
    public class SpotQuery : ISpotQuery
    {
        public List<Spot> GetSpots()
        {
            using (var ctx = new SpaceParkDbContext())
            {
                return ctx.Spot.ToList();
            }
        }
    }
}