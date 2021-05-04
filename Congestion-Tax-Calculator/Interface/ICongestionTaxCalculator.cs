using congestion.calculator.Interface;
using System;

namespace Congestion_Tax_Calculator.Interface
{
    public interface ICongestionTaxCalculator
    {
        public int GetTax(IVehicle vehicle, DateTime[] dates);
    }
}
