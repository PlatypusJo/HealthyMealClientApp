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
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class SavingFoodPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId = string.Empty;

        private NutritionalValueModel _selectedNutritionalValue = new();

        private List<NutritionalValueModel> _nutritionalValues = [];

        private UnitsModel _selectedUnits = new();

        private MealModel _meal;

        private FoodModel _food;

        private DateTime _date;

        private MealTypeModel _mealType = new();

        private double _amountEaten;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<UnitsModel> _units;

        [ObservableProperty]
        private bool _isEdit = false;

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

        public bool IsEnabledSaveBtn  => AmountEaten != 0;

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
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(_mealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string IsFromDiary = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromDiary");
            await Shell.Current.GoToAsync($"..?{mealTypeId}&{date}&{IsFromDiary}");
        }

        [RelayCommand]
        private async Task Save()
        {
            _meal.NutritionalValue = _selectedNutritionalValue;
            _meal.AmountEaten = AmountEaten;
            _meal.Date = _date;
            _meal.MealTypeId = _mealType.Id;
            _meal.FoodId = _food.Id;
            _meal.FoodName = _food.Name;
            _meal.UnitsId = SelectedUnits.Id;
            _meal.UnitsName = SelectedUnits.Name;
            _meal.UserId = _userId;

            if (!IsEdit)
            {
                _meal.Id = Guid.NewGuid().ToString();
                await GlobalDataStore.Meals.AddItemAsync(_meal);
            }
            else
            {
                await GlobalDataStore.Meals.UpdateItemAsync(_meal);
            }

            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(_mealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string isFromDiary = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromDiary");
            await Shell.Current.GoToAsync($"..?{mealTypeId}&{date}&{isFromDiary}");
        }

        [RelayCommand]
        private void TextChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                AmountEaten = 0;
        }

        #endregion

        #region Коструктор

        public SavingFoodPageViewModel()
        {

        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string mealId = string.Empty;
            string foodId = string.Empty;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("IsEdit"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsEdit"]);
                IsEdit = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            if (query.ContainsKey("FoodId"))
            {
                foodId = HttpUtility.UrlDecode(query["FoodId"]);
                foodId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(foodId);
            }

            if (query.ContainsKey("MealType"))
            {
                string mealType = HttpUtility.UrlDecode(query["MealType"]);
                _mealType = NavigationParameterConverter.ObjectFromPairKeyValue<MealTypeModel>(mealType);
            }

            if (query.ContainsKey("MealId"))
            {
                mealId = HttpUtility.UrlDecode(query["MealId"]);
                mealId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(mealId);
            }

            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
            }
            else
            {
                DateTime today = DateTime.Now;
                _date = new DateTime(today.Year, today.Month, today.Day);
            }

            LoadDataAfterNavigation(mealId, foodId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(string mealId, string foodId)
        {
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

            if (IsEdit)
            {
                _meal = await GlobalDataStore.Meals.GetItemAsync(mealId);
                AmountEaten = _meal.AmountEaten;
                SelectedUnits = Units.Find(u => u.Id == _meal.UnitsId);
            }
            else
            {
                _meal = new();
                _selectedNutritionalValue = _nutritionalValues.Find(n => n.IsDefault);
                AmountEaten = _selectedNutritionalValue.UnitsAmount;
                string unitsId = _selectedNutritionalValue.UnitsId;
                SelectedUnits = Units.Find(u => u.Id == _selectedNutritionalValue.UnitsId);
                
            }

            OnPropertyChanged(nameof(FoodName));
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
