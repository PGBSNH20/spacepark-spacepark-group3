using SpacePark.DB.Models;
using System.Collections.Generic;

namespace SpacePark.DB.Interfaces
{
    public interface ISpotQuery
    {
         
         List<Spot> GetSpots();
    }
}