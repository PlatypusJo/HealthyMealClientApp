using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using HealthyMeal.Views;
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

        private DateTime _date;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        ObservableCollection<IngredientModel> _ingredients = [];

        [ObservableProperty]
        ObservableCollection<StepModel> _steps = [];

        [ObservableProperty]
        List<MealTypeModel> _mealTypes = [];

        [ObservableProperty]
        MealTypeModel _selectedMealType;

        [ObservableProperty]
        bool _isMenuAddPopupVisible = false;

        [ObservableProperty]
        bool _isShoppingListPopupVisible = false;

        [ObservableProperty]
        bool _isChoicePopupVisible = false;

        [ObservableProperty]
        bool _isNextPopupVisible = false;

        [ObservableProperty]
        string _nextPopupText = string.Empty;

        [ObservableProperty]
        bool _fromMenu = false;

        [ObservableProperty]
        bool _fromRecipes = true;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DateFormat))]
        DateTime _selectedDate;

        #endregion

        #region Свойства

        public string RecipeName => _recipe.Name;

        public double Kcal => _recipe.Kcal;

        public string CookingTime => _recipe.CookingTimeString;

        public string DateFormat => DateTime.Now.Year != SelectedDate.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenSavingRecipePage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string recipeId = NavigationParameterConverter.ObjectToPairKeyValue(_recipe.FoodId, "FoodId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(_recipe.MealTypeId, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(SelectedDate, "Date");
            await Shell.Current.GoToAsync($"{nameof(SavingRecipePage)}?{userId}&{recipeId}&{mealTypeId}&{date}");
        }

        [RelayCommand]
        private async Task GoBack()
        {
            string isFromMenu = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromMenu");
            await Shell.Current.GoToAsync($"..?{isFromMenu}");
        }

        [RelayCommand]
        private void OpenAddToMenuPopup()
        {
            SelectedDate = _date;
            SelectedMealType = MealTypes.Find(m => m.Id == _recipe.MealTypeId);
            IsMenuAddPopupVisible = true;
        }

        [RelayCommand]
        private void OpenShoppingListPopup()
        {
            SelectedDate = _date;
            IsShoppingListPopupVisible = true;
        }

        [RelayCommand]
        private async Task CreateShoppingList()
        {
            // Проверка существования покупок на выбранную дату
            // Если есть - окно выбора замена/добавление
            // Если нет - добавление в список покупок

            IsShoppingListPopupVisible = false;
            bool result = true;
            if (result)
            {
                IsChoicePopupVisible = true;
            }
            else
            {
                IsNextPopupVisible = true;
                NextPopupText = "Список покупок успешно создан";
            }
        }

        [RelayCommand]
        private async Task AddToShoppingList()
        {
            // Добавление продуктов к уже имеющемуся списку покупок

            IsChoicePopupVisible = false;
            IsNextPopupVisible = true;
            NextPopupText = "В список успешно добавлены продукты";
        }

        [RelayCommand]
        private async Task ReplaceShoppingList()
        {
            // Замена списка покупок с выбранной датой на новый

            IsChoicePopupVisible = false;
            IsNextPopupVisible = true;
            NextPopupText = "Список успешно заменён";
        }

        [RelayCommand]
        private async Task AddToMenu()
        {
            // Добавление в меню, если блюда с таким же типом приёма пищи и даты не существует иначе окно с отказом
            IsMenuAddPopupVisible = false;
            NextPopupText = "Рецепт успешно добавлен в меню!";
            IsNextPopupVisible = true;
        }

        [RelayCommand]
        private void Next()
        {
            IsNextPopupVisible = false;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsMenuAddPopupVisible = false;
            IsShoppingListPopupVisible = false;
            IsChoicePopupVisible = false;
            IsNextPopupVisible = false;
        }

        #endregion

        #region Конструкторы

        public RecipeInfoViewModel() 
        { 
            LoadMealTypes();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            string recipeId = string.Empty;
            string mealTypeId = string.Empty;
            bool isAddToMenu = false;

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

            if (query.ContainsKey("MealTypeId"))
            {
                string strBuf = HttpUtility.UrlDecode(query["MealTypeId"]);
                mealTypeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(strBuf);
            }

            if (query.ContainsKey("IsAddToMenu"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsAddToMenu"]);
                isAddToMenu = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
            }
            else
            {
                DateTime today = DateTime.Now;
                _date = new(today.Year, today.Month, today.Day);
            }

            LoadRecipeInfo(recipeId, mealTypeId, isAddToMenu);
        }

        #endregion

        #region Внутренние методы

        private async void LoadRecipeInfo(string recipeId, string mealTypeId, bool isAddToMenu)
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

            FromMenu = isAddToMenu;
            FromRecipes = !isAddToMenu;
            SelectedDate = _date;

            if (mealTypeId != string.Empty)
            {
                SelectedMealType = MealTypes.Find(m => m.Id == mealTypeId);
            }
            else
            {
                SelectedMealType = MealTypes.Find(m => m.Type == MealType.Breakfast);
            }

            OnPropertyChanged(nameof(RecipeName));
            OnPropertyChanged(nameof(Kcal));
            OnPropertyChanged(nameof(CookingTime));
        }

        private async void LoadMealTypes()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
        }

        #endregion
        
    }
}
