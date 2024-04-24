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

        private NutritionalValueModel _selectedNutritionalValue;

        private List<NutritionalValueModel> _nutritionalValues;

        private UnitsModel _selectedUnits;

        private MealModel _meal;

        private FoodModel _food;

        private DateTime _date;

        private MealTypeModel _mealType;

        private string _foodId;

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
                _amountEaten = value > 0 ? value : 1;
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
            if (query.ContainsKey("Meal"))
            {
                string meal = HttpUtility.UrlDecode(query["Meal"]);
                _meal = NavigationParameterConverter.ObjectFromPairKeyValue<MealModel>(meal);
            }
            
        }

        public async void LoadDataAfterNavigation()
        {
            _food = await GlobalDataStore.Foods.GetItemAsync(_foodId);
            _nutritionalValues = await GlobalDataStore.NutritionalValues.GetAllItemsAsync();
            _nutritionalValues = _nutritionalValues.Where(n => n.FoodId == _foodId).ToList();

            List<UnitsModel> unitsBuf = [];
            foreach (NutritionalValueModel nutritionalValue in _nutritionalValues)
            {
                UnitsModel units = await GlobalDataStore.Units.GetItemAsync(nutritionalValue.UnitsId);
                unitsBuf.Add(units);
            }
            Units = unitsBuf;

            if (IsEdit)
            {
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
