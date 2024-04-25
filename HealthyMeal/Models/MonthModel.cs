using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HealthyMeal.Models
{
    public class MonthModel
    {
        public int Number { get; set; }

        public string Name { get; set; }

        public MonthModel(int monthNumber) 
        { 
            Number = monthNumber;
            Name = CultureInfo.GetCultureInfoByIetfLanguageTag("ru-RU").DateTimeFormat.GetMonthName(monthNumber);
            Name = char.ToUpper(Name[0]) + Name.Substring(1);
        }
    }
}
