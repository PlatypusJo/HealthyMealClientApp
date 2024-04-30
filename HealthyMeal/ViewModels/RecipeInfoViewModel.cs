using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class RecipeInfoViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId = string.Empty;

        private RecipeModel _recipe = new();

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        ObservableCollection<IngredientModel> _ingredients = [];

        [ObservableProperty]
        ObservableCollection<StepModel> _steps = [];

        #endregion

        #region Свойства

        public string RecipeName => _recipe.Name;

        public double Kcal => _recipe.Kcal;

        public string CookingTime => _recipe.CookingTimeString;

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        #endregion

        #region Конструкторы



        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            string recipeId = string.Empty;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("RecipeId"))
            {
                recipeId = HttpUtility.UrlDecode(query["RecipeId"]);
                recipeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(recipeId);
            }

            LoadRecipeInfo(recipeId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadRecipeInfo(string recipeId)
        {
            _recipe = await GlobalDataStore.Recipes.GetItemAsync(recipeId);

            List<StepModel> steps = await GlobalDataStore.Steps.GetAllItemsAsync();
            steps = steps.Where(s => s.RecipeId == recipeId).ToList();

            List<IngredientModel> ingredients = await GlobalDataStore.Ingredients.GetAllItemsAsync();
            ingredients = ingredients.Where(i => i.RecipeId == recipeId).ToList();
            
            List<NutritionalValueModel> nutritionalValues = await GlobalDataStore.NutritionalValues.GetAllItemsAsync();
            foreach (IngredientModel ingredient in ingredients)
            {
                ingredient.NutritionalValue = nutritionalValues.Find(n => n.FoodId == ingredient.FoodId && n.UnitsId == ingredient.UnitsId);

                FoodModel foodInfo = await GlobalDataStore.Foods.GetItemAsync(ingredient.FoodId);
                ingredient.Name = foodInfo.Name;
                ingredient.FoodId = foodInfo.Id;
                
                UnitsModel unitsInfo = await GlobalDataStore.Units.GetItemAsync(ingredient.UnitsId);
                ingredient.UnitsName = unitsInfo.Name;
            }

            Steps = new(steps);
            Ingredients = new(ingredients);

            OnPropertyChanged(nameof(RecipeName));
            OnPropertyChanged(nameof(Kcal));
            OnPropertyChanged(nameof(CookingTime));
        }

        #endregion
        
    }
}
