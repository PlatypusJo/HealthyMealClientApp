using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class SavingFoodPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId;

        private string _foodId;

        private string _mealId;

        private NutritionalValueModel _selectedNutritionalValue;

        private List<NutritionalValueModel> _nutritionalValues;

        private UnitsModel _selectedUnits;

        private MealModel _meal;

        private FoodModel _food;

        private DateTime _date;

        private MealTypeModel _mealType;

        private double _amountEaten;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private bool _isEdit;

        [ObservableProperty]
        private List<UnitsModel> _units;

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
                NutritionalValueInfoUpdate();
            }
        }


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
            string isAdd = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsAdd");
            await Shell.Current.GoToAsync($"..?{mealTypeId}&{date}&{isAdd}");
        }

        [RelayCommand]
        private async Task Save()
        {
            _meal.Id = _mealId;
            _meal.NutritionalValue = _selectedNutritionalValue;
            _meal.AmountEaten = AmountEaten;
            _meal.Date = _date;
            _meal.MealTypeId = _mealType.Id;
            _meal.FoodId = _foodId;
            _meal.FoodName = _food.Name;
            _meal.UnitsId = SelectedUnits.Id;
            _meal.UnitsName = SelectedUnits.Name;
            _meal.UserId = _userId;
        }

        [RelayCommand]
        private void TextChanged(string text)
        {
            if (text == string.Empty)
                AmountEaten = 0;
        }

        #endregion

        #region Коструктор

        public SavingFoodPageViewModel()
        {
            _nutritionalValues = [];
            SelectedUnits = new();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }
            if (query.ContainsKey("IsEdit"))
            {
                string isEdit = HttpUtility.UrlDecode(query["IsEdit"]);
                IsEdit = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(isEdit);
            }
            if (query.ContainsKey("FoodId"))
            {
                string foodId = HttpUtility.UrlDecode(query["FoodId"]);
                _foodId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(foodId);
            }
            if (query.ContainsKey("MealType"))
            {
                string mealType = HttpUtility.UrlDecode(query["MealType"]);
                _mealType = NavigationParameterConverter.ObjectFromPairKeyValue<MealTypeModel>(mealType);
            }
            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
            }
            if (query.ContainsKey("MealId"))
            {
                string mealId = HttpUtility.UrlDecode(query["MealId"]);
                _mealId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(mealId);
            }
            
        }

        public async void LoadDataAfterNavigation()
        {
            _food = await GlobalDataStore.Foods.GetItemAsync(_foodId);
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
                _meal = await GlobalDataStore.Meals.GetItemAsync(_mealId);
                AmountEaten = _meal.AmountEaten;
                SelectedUnits = Units.Find(u => u.Id == _meal.UnitsId);
            }
            else
            {
                _mealId = Guid.NewGuid().ToString();
                _meal = new();
                _selectedNutritionalValue = _nutritionalValues.Find(n => n.IsDefault);
                AmountEaten = _selectedNutritionalValue.UnitsAmount;
                string unitsId = _selectedNutritionalValue.UnitsId;
                SelectedUnits = Units.Find(u => u.Id == _selectedNutritionalValue.UnitsId);
                
            }
            OnPropertyChanged(nameof(FoodName));
        }

        #endregion

        #region Внутренние методы

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
