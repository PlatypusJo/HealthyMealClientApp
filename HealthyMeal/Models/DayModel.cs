using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class DayModel
    {
        public double KcalAmount { get; set; }

        public DateTime Date { get; set; }

        public string DayName => Date.ToString("ddd").ToUpper();

        public string DayNumber => Date.ToString("dd");
    }
}
