using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class PhysicalActivityModel
    {
        public string Id { get; set; } = null!;

        public string Name { get; set; }

        public string Description { get; set; }

        public double FactorActivity { get; set; }
    }
}
