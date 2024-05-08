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
    public partial class SavingToShopListPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId = string.Empty;

        private UnitsModel _selectedUnits = new();

        private ProductToBuyModel _productToBuy;

        private FoodModel _food;

        private DateTime _date;

        private double _unitsAmount;

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
                OnPropertyChanged(nameof(SelectedUnits));
            }
        }

        public double UnitsAmount
        {
            get => _unitsAmount;
            set
            {
                _unitsAmount = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(UnitsAmount));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
            }
        }

        public string SelectedDate => DateTime.Now.Year != _date.Year ? _date.ToString("dd MMM yyyy") : _date.ToString("MMM dd, dddd");

        public bool IsEnabledSaveBtn => UnitsAmount != 0;

        public string FoodName => _food.Name;

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string isFromShopList = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromShopList");
            await Shell.Current.GoToAsync($"..?{date}&{isFromShopList}&{userId}");
        }

        [RelayCommand]
        private async Task Save()
        {
            _productToBuy.UserId = _userId;
            _productToBuy.FoodId = _food.Id;
            _productToBuy.UnitsId = SelectedUnits.Id;
            _productToBuy.UnitsName = SelectedUnits.Name;
            _productToBuy.FoodName = _food.Name;
            _productToBuy.UnitsAmount = UnitsAmount;
            _productToBuy.Date = _date;
            
            if (!IsEdit)
            {
                _productToBuy.IsBought = false;
                _productToBuy.Id = Guid.NewGuid().ToString();
                await GlobalDataStore.ProductsToBuy.AddItemAsync(_productToBuy);
            }
            else
            {
                await GlobalDataStore.ProductsToBuy.UpdateItemAsync(_productToBuy);
            }
            
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string isFromShopList = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromShopList");
            await Shell.Current.GoToAsync($"..?{date}&{isFromShopList}&{userId}");
        }

        [RelayCommand]
        private void TextChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                UnitsAmount = 0;
        }

        #endregion

        #region Коструктор

        public SavingToShopListPageViewModel()
        {

        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string productToBuyId = string.Empty;
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

            if (query.ContainsKey("ProductToBuyId"))
            {
                productToBuyId = HttpUtility.UrlDecode(query["ProductToBuyId"]);
                productToBuyId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(productToBuyId);
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

            OnPropertyChanged(nameof(SelectedDate));
            LoadDataAfterNavigation(productToBuyId, foodId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(string productToBuyId, string foodId)
        {
            _food = await GlobalDataStore.Foods.GetItemAsync(foodId);
            List<NutritionalValueModel> nutritionalValues = await GlobalDataStore.NutritionalValues.GetAllItemsAsync();
            nutritionalValues = nutritionalValues.Where(n => n.FoodId == _food.Id).ToList();

            List<UnitsModel> unitsBuf = [];
            foreach (NutritionalValueModel nutritionalValue in nutritionalValues)
            {
                UnitsModel units = await GlobalDataStore.Units.GetItemAsync(nutritionalValue.UnitsId);
                unitsBuf.Add(units);
            }
            Units = unitsBuf;

            if (IsEdit)
            {
                _productToBuy = await GlobalDataStore.ProductsToBuy.GetItemAsync(productToBuyId);
                UnitsAmount = _productToBuy.UnitsAmount;
                SelectedUnits = Units.Find(u => u.Id == _productToBuy.UnitsId);
            }
            else
            {
                _productToBuy = new();
                NutritionalValueModel nutritionalValue = nutritionalValues.Find(n => n.IsDefault);
                UnitsAmount = nutritionalValue.UnitsAmount;
                string unitsId = nutritionalValue.UnitsId;
                SelectedUnits = Units.Find(u => u.Id == nutritionalValue.UnitsId);

            }

            OnPropertyChanged(nameof(FoodName));
        }

        #endregion
    }
}
