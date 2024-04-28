using System;
using System.Collections.Generic;
using System.Text;

namespace HealthyMeal.Models
{
    public class StepModel
    {
        public string Id { get; set; } = null!;

        public string RecipeId { get; set; }

        public string Description { get; set; }

        public int StepNumber { get; set; }

        public string DescriptionToShow => $"{StepNumber} {Description}";
    }
}
