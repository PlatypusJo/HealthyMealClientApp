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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class ProductCrudPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId;

        private UserModel _user;

        private UnitsModel _selectedUnitsDefault;

        private string _foodId;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private string _foodName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private ObservableCollection<UnitsModel> _units;

        [ObservableProperty]
        private List<NutritionalValueModel> _nutritionalValues;

        [ObservableProperty]
        private bool _isFromProducts = false;

        [ObservableProperty]
        private bool _isEdit = false;

        #endregion

        #region Свойства

        public bool IsEnabledSaveBtn => 
            FoodName != string.Empty
            && Units.Count > 0;

        public UnitsModel SelectedUnitsDefault
        {
            get => _selectedUnitsDefault;
            set
            {
                if (value is null)
                    return;

                _selectedUnitsDefault = value;
                OnPropertyChanged(nameof(SelectedUnitsDefault));
                SetDefaultUnits();
            }
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task Save()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void AddPhoto()
        {

        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task OpenAddingUnitsPage()
        {
            string nutritionalValues = NavigationParameterConverter.ObjectToPairKeyValue(NutritionalValues, nameof(NutritionalValues));
            string units = NavigationParameterConverter.ObjectToPairKeyValue(Units.ToList(), nameof(Units));
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsEdit");
            string isFromProduct = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromProduct");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(_foodId, "FoodId");
            await Shell.Current.GoToAsync($"{nameof(AddingUnitsPage)}?{nutritionalValues}&{units}&{isEdit}&{isFromProduct}&{foodId}");
        }

        [RelayCommand]
        private void NameChanged(string text)
        {
            if (text == string.Empty)
                FoodName = string.Empty;
        }

        #endregion

        #region Конструкторы

        public ProductCrudPageViewModel()
        {
            
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            string foodId = string.Empty;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("IsFromProducts"))
            {
                IsFromProducts = NavigationParameterConverter.ObjectFromUrl<bool>(query["IsFromProducts"]);
            }

            if (query.ContainsKey("IsEdit"))
            {
                IsEdit = NavigationParameterConverter.ObjectFromUrl<bool>(query["IsEdit"]);
            }

            if (query.ContainsKey("Units"))
            {
                List<UnitsModel> unitsBuf = NavigationParameterConverter.ObjectFromUrl<List<UnitsModel>>(query["Units"]);
                Units = new(unitsBuf);
            }

            if (query.ContainsKey("NutritionalValues"))
            {
                List<NutritionalValueModel> nutritionalValuesBuf = NavigationParameterConverter.ObjectFromUrl<List<NutritionalValueModel>>(query["NutritionalValues"]);
                NutritionalValues = new(nutritionalValuesBuf);
            }

            if (query.ContainsKey("FoodId"))
            {
                foodId = NavigationParameterConverter.ObjectFromUrl<string>(query["FoodId"]);
            }

            LoadDataAfterNavigation(foodId);
        }

        public async void LoadDataAfterNavigation(string foodId)
        {
            _user = await GlobalDataStore.Users.GetItemAsync(_userId);
            string unitsDefaultId = string.Empty;

            if (IsFromProducts)
            {
                _foodId = Guid.NewGuid().ToString();
                FoodName = string.Empty;
                Units = [];
                NutritionalValues = [];
                SelectedUnitsDefault = new();
            }

            if (IsEdit)
            {
                FoodModel food = await GlobalDataStore.Foods.GetItemAsync(foodId);
                _foodId = food.Id;
                FoodName = food.Name;

                List<NutritionalValueModel> nutritionalValues = await GlobalDataStore.NutritionalValues.GetAllItemsAsync();
                NutritionalValues = new(nutritionalValues.Where(n => n.FoodId == food.Id).ToList());

                List<UnitsModel> unitsBuf = [];
                foreach (NutritionalValueModel nutritionalValue in NutritionalValues)
                {
                    UnitsModel units = await GlobalDataStore.Units.GetItemAsync(nutritionalValue.UnitsId);
                    unitsBuf.Add(units);
                }
                Units = new(unitsBuf);
            }
            
            if (NutritionalValues.Count > 0)
            {
                NutritionalValueModel nutritionalValueDef = NutritionalValues.Find(n => n.IsDefault);
                if (nutritionalValueDef is not null)
                {
                    unitsDefaultId = nutritionalValueDef.UnitsId;
                    SelectedUnitsDefault = Units.ToList().Find(u => u.Id == unitsDefaultId);
                }
            }
        }

        #endregion

        #region Внутренние методы

        private void SetDefaultUnits()
        {
            foreach (NutritionalValueModel nutritionalValue in NutritionalValues)
            {
                nutritionalValue.IsDefault = nutritionalValue.UnitsId == SelectedUnitsDefault.Id;
            }
        }

        #endregion
    }
}
