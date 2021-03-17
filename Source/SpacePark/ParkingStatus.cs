using System;

namespace SpacePark.Models
{
    public class ParkingStatus
    {
        DateTime startedTime;
        int[,] parkingSpot = new int[,] { }; // Two dimensional array (X, Y coordinate)
    }
}