using congestion.calculator;
using congestion.calculator.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Congestion_Tax_Calculator.Factory
{
    public class ConcreteVehicleFactory : VehicleFactory
    {
        public override IVehicle GetVehicle(string vehicle)
        {
            switch (vehicle.ToLower())
            {
                case "car":
                    return new Car();
                case "motorbike":
                    return new Motorbike();
                default:
                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", vehicle));
            }
        }
    }
}
