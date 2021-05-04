using Congestion_Tax_Calculator.Factory;
using Congestion_Tax_Calculator.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Congestion_Tax_Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongestionTaxCalculatorController : ControllerBase
    {

        private ICongestionTaxCalculator _congestionTaxCalculator;
        private VehicleFactory _vehicleFactory;
        public CongestionTaxCalculatorController(ICongestionTaxCalculator congestionTaxCalculator, VehicleFactory vehicleFactory)
        {
            _congestionTaxCalculator = congestionTaxCalculator;
            _vehicleFactory = vehicleFactory;
        }
        [HttpPost]
        [ActionName("GetTax")]
        public ActionResult GetTax(string vehicle, DateTime[] dateTimes)
        {
            try
            {
                var result = _congestionTaxCalculator.GetTax(_vehicleFactory.GetVehicle(vehicle), dateTimes);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }
    }
}
