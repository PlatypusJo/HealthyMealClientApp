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

        private string _mealTypeId = string.Empty;

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
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string isFromMenu = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromMenu");
            await Shell.Current.GoToAsync($"..?{isFromMenu}&{userId}&{mealTypeId}&{date}");
        }

        [RelayCommand]
        private void OpenAddToMenuPopup()
        {
            SelectedDate = _date;
            SelectedMealType = MealTypes.Find(m => m.Id == _mealTypeId);
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
            IsShoppingListPopupVisible = false;
            List<ProductToBuyModel> productsToBuy = await GlobalDataStore.ProductsToBuy.GetAllItemsAsync();
            bool result = productsToBuy.Exists(p => p.Date == SelectedDate);
                        
            if (result)
            {
                IsChoicePopupVisible = true;
            }
            else
            {
                foreach (IngredientModel ingredient in Ingredients)
                {
                    ProductToBuyModel shopListItem = new()
                    { 
                        Id = Guid.NewGuid().ToString(),
                        FoodId = ingredient.FoodId,
                        FoodName = ingredient.Name,
                        UnitsId = ingredient.UnitsId,
                        UserId = _userId,
                        UnitsName = ingredient.UnitsName,
                        UnitsAmount = ingredient.UnitsAmount,
                        Date = SelectedDate,
                        IsBought = false,
                    };
                    await GlobalDataStore.ProductsToBuy.AddItemAsync(shopListItem);
                }
                IsNextPopupVisible = true;
                NextPopupText = "Список покупок успешно создан";
            }
        }

        [RelayCommand]
        private async Task AddToShoppingList()
        {
            IsChoicePopupVisible = false;

            foreach (IngredientModel ingredient in Ingredients)
            {
                ProductToBuyModel shopListItem = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FoodId = ingredient.FoodId,
                    FoodName = ingredient.Name,
                    UnitsId = ingredient.UnitsId,
                    UserId = _userId,
                    UnitsName = ingredient.UnitsName,
                    UnitsAmount = ingredient.UnitsAmount,
                    Date = SelectedDate,
                    IsBought = false,
                };
                await GlobalDataStore.ProductsToBuy.AddItemAsync(shopListItem);
            }

            IsNextPopupVisible = true;
            NextPopupText = "В список успешно добавлены продукты";
        }

        [RelayCommand]
        private async Task ReplaceShoppingList()
        {
            IsChoicePopupVisible = false;

            List<ProductToBuyModel> productsToBuy = await GlobalDataStore.ProductsToBuy.GetAllItemsAsync();
            productsToBuy = productsToBuy.Where(p => p.Date == SelectedDate).ToList();

            foreach (ProductToBuyModel productToBuy in productsToBuy)
            {
                await GlobalDataStore.ProductsToBuy.DeleteItemAsync(productToBuy.Id);
            }

            foreach (IngredientModel ingredient in Ingredients)
            {
                ProductToBuyModel shopListItem = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FoodId = ingredient.FoodId,
                    FoodName = ingredient.Name,
                    UnitsId = ingredient.UnitsId,
                    UserId = _userId,
                    UnitsName = ingredient.UnitsName,
                    UnitsAmount = ingredient.UnitsAmount,
                    Date = SelectedDate,
                    IsBought = false,
                };
                await GlobalDataStore.ProductsToBuy.AddItemAsync(shopListItem);
            }

            IsNextPopupVisible = true;
            NextPopupText = "Список успешно заменён";
        }

        [RelayCommand]
        private async Task AddToMenu()
        {
            bool isExist = false;
            IsMenuAddPopupVisible = false;
            List<MenuStringModel> menuStrings = await GlobalDataStore.MenuStrings.GetAllItemsAsync();
            List<MenuModel> menus = await GlobalDataStore.Menus.GetAllItemsAsync();
            MenuModel menu = menus.Find(m => m.Date == SelectedDate);
            if (menu is null)
            {
                menu = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = _userId,
                    Date = SelectedDate,
                };
                await GlobalDataStore.Menus.AddItemAsync(menu);
            }
                        
            menuStrings = menuStrings.Where(m => m.MenuId == menu.Id).ToList();
            isExist = menuStrings.Exists(m => m.MealTypeId == SelectedMealType.Id && m.RecipeId == _recipe.Id);
            if (isExist)
            {
                NextPopupText = "Это блюдо уже есть в меню";
                IsNextPopupVisible = true;
                return;
            }

            menu.Kcal = menuStrings.Sum(m => m.Kcal) + _recipe.Kcal;
            menu.Proteins = menuStrings.Sum(m => m.Proteins) + _recipe.Proteins;
            menu.Fats = menuStrings.Sum(m => m.Fats) + _recipe.Fats;
            menu.Carbohydrates = menuStrings.Sum(m => m.Carbohydrates) + _recipe.Carbohydrates;
            await GlobalDataStore.Menus.UpdateItemAsync(menu);

            MenuStringModel menuString = new()
            {
                Id = Guid.NewGuid().ToString(),
                MealTypeId = SelectedMealType.Id,
                RecipeId = _recipe.Id,
                MenuId = menu.Id,
                RecipeName = _recipe.Name,
                CookingTime = _recipe.CookingTime,
                Kcal = _recipe.Kcal,
                Proteins = _recipe.Proteins,
                Fats = _recipe.Fats,
                Carbohydrates = _recipe.Carbohydrates,
            };

            await GlobalDataStore.MenuStrings.AddItemAsync(menuString);
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
            bool isOnlyInfo = false;

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

            if (query.ContainsKey("IsOnlyInfo"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsOnlyInfo"]);
                isOnlyInfo = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
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

            LoadRecipeInfo(recipeId, mealTypeId, isAddToMenu, isOnlyInfo);
        }

        #endregion

        #region Внутренние методы

        private async void LoadRecipeInfo(string recipeId, string mealTypeId, bool isAddToMenu, bool isOnlyInfo)
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

            if (isOnlyInfo)
            {
                FromMenu = false;
                FromRecipes = false;
            }
            else
            {
                FromMenu = isAddToMenu;
                FromRecipes = !isAddToMenu;
            }
            
            SelectedDate = _date;
            _mealTypeId = mealTypeId;

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
