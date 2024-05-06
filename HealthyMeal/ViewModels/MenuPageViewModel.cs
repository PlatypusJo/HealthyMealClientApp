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

        private List<MenuStringModel> _dishes;

        //ObservableCollection<RecipeModel> _recipes;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private ObservableCollection<MenuStringModel> _dishesToShow;

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
            }
        }

        //public ObservableCollection<RecipeModel> Recipes 
        //{
        //    get => _recipes; 
        //}

        #endregion

        #region Команды

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
        private void OpenEditPopup()
        {
            SelectedDate = _date;
            IsPopupEditVisible = true;
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            // Если на выбранную дату нет других меню, то изменяем
            // Иначе выдаём сообщение, что невозможно перенести.

            bool result = true;
            IsPopupEditVisible = false;
            if (result)
            {
                IsNextPopupVisible = true;
                NextPopupText = "На эту дату уже есть меню";
            }
            else
            {
                IsNextPopupVisible = true;
                NextPopupText = "Меню успешно перенесено";
            }
            
        }

        [RelayCommand]
        private void OpenDeletePopup()
        {
            IsPopupDeleteVisible = true;
        }

        [RelayCommand]
        private async Task DeleteMenu()
        {
            IsPopupDeleteVisible = false;
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
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataByDateAsync()
        {
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

            List<MenuStringModel> dishesBuf = [];
            dishesBuf = _dishes.Where(m => m.MenuId == _menu.Id && m.MealTypeId == SelectedMealType.Id).ToList();
            DishesToShow = [..dishesBuf];
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
