using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace HealthyMeal.ViewModels
{
    public class MenuPageViewModel
    {
        ObservableCollection<RecipeModel> _recipes;

        public MenuPageViewModel() 
        {
            _recipes = new ObservableCollection<RecipeModel>()
            {
                new RecipeModel() 
                { 
                    Name = "Овощной суп",
                    Id = 1.ToString(),
                },
                new RecipeModel() 
                { 
                    Name = "Борщ",
                    Id = 2.ToString(),
                },
                new RecipeModel() 
                { 
                    Name = "Фруктовый салат",
                    Id = 3.ToString(),
                },
                new RecipeModel() 
                { 
                    Name = "Греческий салат",
                    Id = 4.ToString(),
                },
            };
        }

        public ObservableCollection<RecipeModel> Recipes 
        {
            get => _recipes; 
        }
    }
}
