using SpacePark.DB.Models;
namespace SpacePark.Logic
{
    public class Logic
    {
        public bool EndParkingByName(string name)
        {
            var res = new ParkingStatus().GetByCusomterName(name);
            if (res == null)
                return false;


            Customer.GetByID(res.CustomerID).Delete();
            return true;
        }
    }
}