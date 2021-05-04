using congestion.calculator.Interface;

namespace congestion.calculator
{
    public class Motorbike : IVehicle
    {
        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}