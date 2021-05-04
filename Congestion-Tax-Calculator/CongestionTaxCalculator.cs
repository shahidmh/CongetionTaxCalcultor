using System;
using System.IO;
using congestion.calculator.Interface;
using Congestion_Tax_Calculator;
using Congestion_Tax_Calculator.Enum;
using Congestion_Tax_Calculator.Interface;
using Newtonsoft.Json;

public class CongestionTaxCalculator : ICongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */

    public int GetTax(IVehicle vehicle, DateTime[] dates)
    {
        DateTime intervalStart = dates[0];
        int totalFee = 0;
        foreach (DateTime date in dates)
        {
            int nextFee = GetTollFee(date, vehicle);
            int tempFee = GetTollFee(intervalStart, vehicle);
            var hours = (date - intervalStart).TotalMinutes;
            int diffInMillies = date.Millisecond - intervalStart.Millisecond;
            var minutes = (date - intervalStart).TotalMinutes;

            if (minutes <= 60)
            {
                if (totalFee > 0)
                    totalFee -= tempFee;
                if (nextFee >= tempFee)
                    tempFee = nextFee;
                totalFee += tempFee;
            }
            else
            {
                totalFee += nextFee;
            }
        }
        if (totalFee > 60) totalFee = 60;
        return totalFee;
    }


    private bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        string vehicleType = vehicle.GetVehicleType();
        return vehicleType.Equals(TollFreeVehicles.Motorcycle.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Tractor.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Emergency.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Diplomat.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Foreign.ToString()) ||
               vehicleType.Equals(TollFreeVehicles.Military.ToString());
    }

    public int GetTollFee(DateTime date, IVehicle vehicle)
    {
        DateTime start;
        DateTime end;
        var file = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}/Config/TimeAndAmountConfig.json");
        var timeAndPayConfig = JsonConvert.DeserializeObject<TimeAndPayConfig>(file);
      
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        foreach (var item in timeAndPayConfig.timeAndPayDetails)
        {
            start = DateTime.Parse(item.Start, System.Globalization.CultureInfo.CurrentCulture);
            end = DateTime.Parse(item.End, System.Globalization.CultureInfo.CurrentCulture);
            if (end.TimeOfDay >= date.TimeOfDay && date.TimeOfDay >= start.TimeOfDay)
            {
                return item.Amount;
            }
        }
        return 0;        
    }

    private Boolean IsTollFreeDate(DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }

}