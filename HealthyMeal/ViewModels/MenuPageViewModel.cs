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
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class MenuPageViewModel : BaseViewModel
    {
        #region Поля

        private UserModel _user;

        private DateTime _date;

        private MenuModel _menu;

        private MealTypeModel _selectedMealType;

        private List<MenuStringModel> _dishes = [];

        private TimeSpan _menuCookingTime = TimeSpan.Zero;

        //ObservableCollection<RecipeModel> _recipes;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private ObservableCollection<MenuStringModel> _dishesToShow = [];

        [ObservableProperty]
        private double _proteinsAmount;

        [ObservableProperty]
        private double _fatsAmount;

        [ObservableProperty]
        private double _carbohydratesAmount;

        [ObservableProperty]
        private double _kcalAmount;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedDateFormat))]
        private DateTime _selectedDate;

        [ObservableProperty]
        bool _isShoppingListPopupVisible = false;

        [ObservableProperty]
        bool _isChoicePopupVisible = false;

        [ObservableProperty]
        bool _isNextPopupVisible = false;

        [ObservableProperty]
        private bool _isPopupEditVisible = false;

        [ObservableProperty]
        private bool _isPopupDeleteVisible = false;

        [ObservableProperty]
        string _nextPopupText = string.Empty;

        #endregion

        #region Свойства

        public string CookingTimeString => _menuCookingTime.TotalMinutes <= 59 ?
                _menuCookingTime.ToString("%m") + " мин" :
                (_menuCookingTime.Days * 24 + _menuCookingTime.Hours).ToString() + " ч " + _menuCookingTime.ToString("%m") + " мин";

        public string SelectedDateFormat => DateTime.Now.Year != SelectedDate.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public string DateFormat => DateTime.Now.Year != Date.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                LoadDataByDateAsync();
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(DateFormat));
                OnPropertyChanged(nameof(CookingTimeString));
            }
        }

        public MealTypeModel SelectedMealType
        {
            get => _selectedMealType;
            set
            {
                _selectedMealType = value;
                LoadDataByMealTypeAsync();
                OnPropertyChanged(nameof(SelectedMealType));
                OnPropertyChanged(nameof(CookingTimeString));
            }
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task ItemTapped(MenuStringModel dish)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_user.Id, "UserId");
            string recipeId = NavigationParameterConverter.ObjectToPairKeyValue(dish.RecipeId, "RecipeId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string IsAddToMenu = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsAddToMenu");
            string isOnlyInfo = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsOnlyInfo");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            await Shell.Current.GoToAsync($"{nameof(RecipeInfoPage)}?{userId}&{recipeId}&{mealTypeId}&{IsAddToMenu}&{date}&{isOnlyInfo}");
        }

        [RelayCommand]
        private async Task OpenSavingFoodPage(MenuStringModel dish)
        {
            RecipeModel recipe = await GlobalDataStore.Recipes.GetItemAsync(dish.RecipeId);

            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_user.Id, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(recipe.FoodId, "FoodId");
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsEdit");
            await Shell.Current.GoToAsync($"{nameof(SavingFoodPage)}?{userId}&{mealTypeId}&{date}&{foodId}&{isEdit}");
        }

        [RelayCommand]
        private async Task RemoveDish(MenuStringModel dish)
        {
            await GlobalDataStore.MenuStrings.DeleteItemAsync(dish.Id);
            List<MenuStringModel> menuStrings = await GlobalDataStore.MenuStrings.GetAllItemsAsync();
            menuStrings = menuStrings.Where(m => m.MenuId == _menu.Id).ToList();

            if (menuStrings.Count == 0)
            {
                await GlobalDataStore.Menus.DeleteItemAsync(_menu.Id);
            }

            LoadDataByDateAsync();
            OnPropertyChanged(nameof(CookingTimeString));
        }

        [RelayCommand]
        private async Task OpenMenuRecipesPage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_user.Id, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, nameof(Date));
            string isFromMenu = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromMenu");
            await Shell.Current.GoToAsync($"{nameof(MenuRecipesPage)}?{userId}&{mealTypeId}&{date}&{isFromMenu}");
        }

        [RelayCommand]
        private void OpenShoppingListPopup()
        {
            if (_menu is not null)
            {
                SelectedDate = _date;
                IsShoppingListPopupVisible = true;
            }
            else
            {
                NextPopupText = "Нет меню для составления списка";
                IsNextPopupVisible = true;                
            }
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
                await CreateShoppingListByMenu();
                IsNextPopupVisible = true;
                NextPopupText = "Список покупок успешно создан";
            }
        }

        [RelayCommand]
        private async Task AddToShoppingList()
        {
            // Добавление продуктов к уже имеющемуся списку покупок
            IsChoicePopupVisible = false;
            await CreateShoppingListByMenu();
            IsNextPopupVisible = true;
            NextPopupText = "В список успешно добавлены продукты";
        }

        [RelayCommand]
        private async Task ReplaceShoppingList()
        {
            // Замена списка покупок на новый
            IsChoicePopupVisible = false;

            List<ProductToBuyModel> productsToBuy = await GlobalDataStore.ProductsToBuy.GetAllItemsAsync();
            productsToBuy = productsToBuy.Where(p => p.Date == SelectedDate).ToList();

            foreach (ProductToBuyModel productToBuy in productsToBuy)
            {
                await GlobalDataStore.ProductsToBuy.DeleteItemAsync(productToBuy.Id);
            }

            await CreateShoppingListByMenu();
            IsNextPopupVisible = true;
            NextPopupText = "Список успешно заменён";
        }

        [RelayCommand]
        private void OpenEditPopup()
        {
            if (_menu is not null)
            {
                SelectedDate = Date;
                IsPopupEditVisible = true;
            }
            else
            {
                NextPopupText = "Меню для изменения нет";
                IsNextPopupVisible = true;
            }
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            IsPopupEditVisible = false;
            List<MenuModel> menus = await GlobalDataStore.Menus.GetAllItemsAsync();
            bool result = menus.Exists(m => m.Date == SelectedDate);

            if (result)
            {
                IsNextPopupVisible = true;
                NextPopupText = "На эту дату уже есть меню";
            }
            else
            {
                _menu.Date = SelectedDate;
                await GlobalDataStore.Menus.UpdateItemAsync(_menu);

                Date = SelectedDate;
                IsNextPopupVisible = true;
                NextPopupText = "Меню успешно перенесено";
            }
        }

        [RelayCommand]
        private void OpenDeletePopup()
        {
            if (_menu is not null)
            {
                IsPopupDeleteVisible = true;
            }
            else
            {
                NextPopupText = "Меню для удаления нет";
                IsNextPopupVisible = true;
            }
        }

        [RelayCommand]
        private async Task DeleteMenu()
        {
            IsPopupDeleteVisible = false;

            foreach(MenuStringModel dish in _dishes)
            {
                await GlobalDataStore.MenuStrings.DeleteItemAsync(dish.Id);
            }

            await GlobalDataStore.Menus.DeleteItemAsync(_menu.Id);
            LoadDataByDateAsync();
            OnPropertyChanged(nameof(CookingTimeString));
            
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsPopupDeleteVisible = false;
            IsPopupEditVisible = false;
            IsShoppingListPopupVisible = false;
            IsChoicePopupVisible = false;
            IsNextPopupVisible = false;
        }

        #endregion

        #region Конструктор

        public MenuPageViewModel() 
        {
            //_recipes = [
            //    new RecipeModel()
            //    {
            //        Name = "Овощной суп",
            //        Id = 1.ToString(),
            //    },
            //    new RecipeModel()
            //    {
            //        Name = "Борщ",
            //        Id = 2.ToString(),
            //    },
            //    new RecipeModel()
            //    {
            //        Name = "Фруктовый салат",
            //        Id = 3.ToString(),
            //    },
            //    new RecipeModel()
            //    {
            //        Name = "Греческий салат",
            //        Id = 4.ToString(),
            //    },
            //    ];
            _user = new()
            {
                Id = "1",
                Name = "Иван",
                Login = "LoVan",
                Rdc = 2500,
                KcalAmountGoal = 2000,
                Age = 25,
                Height = 176,
                Weight = 73,
            };
            LoadMealTypesAsync();
            DateTime today = DateTime.Now;
            _date = new DateTime(today.Year, today.Month, today.Day);

        }

        #endregion

        #region Методы

        public void LoadDataAfterNavigation()
        {
            LoadDataByDateAsync();
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(DateFormat));
            OnPropertyChanged(nameof(CookingTimeString));
        }

        #endregion

        #region Внутренние методы

        private async Task CreateShoppingListByMenu()
        {
            List<IngredientModel> ingredients = await GlobalDataStore.Ingredients.GetAllItemsAsync();
            List<IngredientModel> menuIngredients = [];

            foreach (MenuStringModel dish in _dishes)
            {
                List<IngredientModel> ingredientsBuf = ingredients.Where(i => i.RecipeId == dish.RecipeId).ToList();
                menuIngredients = [.. menuIngredients, .. ingredientsBuf];
            }

            foreach (IngredientModel ingredient in menuIngredients)
            {
                ProductToBuyModel shopListItem = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FoodId = ingredient.FoodId,
                    FoodName = ingredient.Name,
                    UnitsId = ingredient.UnitsId,
                    UserId = _user.Id,
                    UnitsName = ingredient.UnitsName,
                    UnitsAmount = ingredient.UnitsAmount,
                    Date = SelectedDate,
                    IsBought = false,
                };
                await GlobalDataStore.ProductsToBuy.AddItemAsync(shopListItem);
            }
        }

        private async void LoadDataByDateAsync()
        {
            _menu = null;
            _dishes.Clear();
            DishesToShow.Clear();
            _menuCookingTime = TimeSpan.Zero;
            KcalAmount = 0;
            ProteinsAmount = 0;
            FatsAmount = 0;
            CarbohydratesAmount = 0;

            List<MenuModel> menus = await GlobalDataStore.Menus.GetAllItemsAsync();
            _menu = menus.Find(x => x.Date == Date);

            if (_menu == null)
                return;
            
            List<MenuStringModel> menuStrings = await GlobalDataStore.MenuStrings.GetAllItemsAsync();
            _dishes = menuStrings.Where(m => m.MenuId == _menu.Id).ToList();

            KcalAmount = menuStrings.Sum(m => m.Kcal);
            ProteinsAmount = menuStrings.Sum(m => m.Proteins);
            FatsAmount = menuStrings.Sum(m => m.Fats);
            CarbohydratesAmount = menuStrings.Sum(m => m.Carbohydrates);

            LoadDataByMealTypeAsync();
        }

        private async void LoadDataByMealTypeAsync()
        {
            if (_menu == null) 
                return;

            _menuCookingTime = TimeSpan.Zero;
            DishesToShow.Clear();
            List<MenuStringModel> dishesBuf = [];
            dishesBuf = _dishes.Where(m => m.MenuId == _menu.Id && m.MealTypeId == SelectedMealType.Id).ToList();
            DishesToShow = [..dishesBuf];
            foreach (MenuStringModel menuString in DishesToShow)
            {
                _menuCookingTime += menuString.CookingTime;
            }
        }

        private async void LoadMealTypesAsync()
        {
            List<MealTypeModel> mealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. mealTypes.OrderBy(m => m.Type)];
            SelectedMealType = MealTypes.Find(m => m.Type == MealType.Breakfast);
        }

        #endregion
    }
}
