using congestion.calculator.Interface;

namespace Congestion_Tax_Calculator.Factory
{
    public abstract class VehicleFactory
    {
        public abstract IVehicle GetVehicle(string vehicle);
    }
}
