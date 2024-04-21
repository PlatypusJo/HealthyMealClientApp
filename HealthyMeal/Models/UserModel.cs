using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HealthyMeal.Models
{
    public class UserModel
    {
        public string Id { get; set; } = null!;

        public string SexId { get; set; }

        public string PhysicalActivityId { get; set; }

        public string Name { get; set; }

        public string Login { get; set; }

        public double RDC { get; set; }

        public double KcalAmountGoal { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public int Age { get; set; }

        #nullable enable
        public byte[]? Photo { get; set; }
    }
}
