using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class SavingRecipePageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId = string.Empty;

        private NutritionalValueModel _selectedNutritionalValue = new();

        private List<NutritionalValueModel> _nutritionalValues = [];

        private UnitsModel _selectedUnits = new();

        private MealModel _meal;

        private FoodModel _food;

        private double _amountEaten;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<UnitsModel> _units;

        [ObservableProperty]
        List<MealTypeModel> _mealTypes = [];

        [ObservableProperty]
        MealTypeModel _selectedMealType;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DateFormat))]
        DateTime _selectedDate;

        [ObservableProperty]
        #nullable enable
        private byte[]? _photo;

        #endregion

        #region Свойства

        public UnitsModel SelectedUnits
        {
            get => _selectedUnits;
            set
            {
                if (value is null) return;
                _selectedUnits = value;
                _selectedNutritionalValue = _nutritionalValues.Find(n => n.UnitsId == _selectedUnits.Id);
                OnPropertyChanged(nameof(SelectedUnits));
                NutritionalValueInfoUpdate();
            }
        }

        public double AmountEaten
        {
            get => _amountEaten;
            set
            {
                _amountEaten = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(AmountEaten));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
                NutritionalValueInfoUpdate();
            }
        }

        public string DateFormat => DateTime.Now.Year != SelectedDate.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public bool IsEnabledSaveBtn => AmountEaten != 0;

        public double Kcal => _selectedNutritionalValue.Kcal * AmountEaten / _selectedNutritionalValue.UnitsAmount;

        public double Proteins => _selectedNutritionalValue.Proteins * AmountEaten / _selectedNutritionalValue.UnitsAmount;

        public double Fats => _selectedNutritionalValue.Fats * AmountEaten / _selectedNutritionalValue.UnitsAmount;

        public double Carbohydrates => _selectedNutritionalValue.Carbohydrates * AmountEaten / _selectedNutritionalValue.UnitsAmount;

        public string FoodName => _food.Name;

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        private async Task Save()
        {
            _meal.Id = Guid.NewGuid().ToString();
            _meal.NutritionalValue = _selectedNutritionalValue;
            _meal.AmountEaten = AmountEaten;
            _meal.Date = SelectedDate;
            _meal.MealTypeId = SelectedMealType.Id;
            _meal.FoodId = _food.Id;
            _meal.FoodName = _food.Name;
            _meal.UnitsId = SelectedUnits.Id;
            _meal.UnitsName = SelectedUnits.Name;
            _meal.UserId = _userId;
            
            await GlobalDataStore.Meals.AddItemAsync(_meal);

            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        private void TextChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                AmountEaten = 0;
        }

        #endregion

        #region Коструктор

        public SavingRecipePageViewModel()
        {
            LoadMealTypes();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string mealTypeId = string.Empty;
            string foodId = string.Empty;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("FoodId"))
            {
                foodId = HttpUtility.UrlDecode(query["FoodId"]);
                foodId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(foodId);
            }

            if (query.ContainsKey("MealTypeId"))
            {
                string strBuf = HttpUtility.UrlDecode(query["MealTypeId"]);
                mealTypeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(strBuf);
            }

            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                SelectedDate = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
            }
            else
            {
                DateTime today = DateTime.Now;
                SelectedDate = new DateTime(today.Year, today.Month, today.Day);
            }

            LoadDataAfterNavigation(mealTypeId, foodId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(string mealTypeId, string foodId)
        {
            Photo = null;
            _food = await GlobalDataStore.Foods.GetItemAsync(foodId);
            _nutritionalValues = await GlobalDataStore.NutritionalValues.GetAllItemsAsync();
            _nutritionalValues = _nutritionalValues.Where(n => n.FoodId == _food.Id).ToList();

            List<UnitsModel> unitsBuf = [];
            foreach (NutritionalValueModel nutritionalValue in _nutritionalValues)
            {
                UnitsModel units = await GlobalDataStore.Units.GetItemAsync(nutritionalValue.UnitsId);
                unitsBuf.Add(units);
            }
            Units = unitsBuf;

            _meal = new();
            _selectedNutritionalValue = _nutritionalValues.Find(n => n.IsDefault);
            AmountEaten = _selectedNutritionalValue.UnitsAmount;
            string unitsId = _selectedNutritionalValue.UnitsId;
            SelectedUnits = Units.Find(u => u.Id == _selectedNutritionalValue.UnitsId);

            if (mealTypeId != string.Empty)
            {
                SelectedMealType = MealTypes.Find(m => m.Id == mealTypeId);
            }
            else
            {
                SelectedMealType = MealTypes.Find(m => m.Type == MealType.Breakfast);
            }

            Photo = _food.Image;
            OnPropertyChanged(nameof(FoodName));
        }

        private async void LoadMealTypes()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
        }

        private void NutritionalValueInfoUpdate()
        {
            OnPropertyChanged(nameof(Kcal));
            OnPropertyChanged(nameof(Proteins));
            OnPropertyChanged(nameof(Fats));
            OnPropertyChanged(nameof(Carbohydrates));
        }

        #endregion
    }
}
