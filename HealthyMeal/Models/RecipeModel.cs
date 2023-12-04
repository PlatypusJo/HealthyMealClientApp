using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthyMeal.Models
{
    public class RecipeModel
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public ObservableCollection<StepModel> StepsCooking { get; set; }
        public ObservableCollection<IngredientModel> Ingredients { get; set; }
    }
}
