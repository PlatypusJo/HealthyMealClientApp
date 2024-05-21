using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Intefaces;
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
    public partial class AddingUnitsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _foodId = string.Empty;

        private int _unitsIndex = 0;

        private double _unitsAmount;

        private double _amountKcal;

        private double _amountProteins;

        private double _amountFats;

        private double _amountCarbohydrates;

        private List<UnitsModel> _unitsList = [];

        private List<NutritionalValueModel> _nutritionalValuesList = [];

        private string _userId = string.Empty;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<UnitsModel> _units;

        [ObservableProperty]
        private UnitsModel _selectedUnits;

        [ObservableProperty]
        private bool _isEdit = false;

        [ObservableProperty]
        private bool _isFromProduct = false;

        #endregion

        #region Свойства

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

        public bool IsEnabledSaveBtn => UnitsAmount != 0 
            && Kcal != 0 
            && (Proteins != 0 || Fats != 0 || Carbohydrates != 0);

        public double Kcal
        {
            get => _amountKcal;
            set
            {
                _amountKcal = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(Kcal));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
            }
        }

        public double Proteins
        {
            get => _amountProteins;
            set
            {
                _amountProteins = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(Proteins));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
            }
        }

        public double Fats
        {
            get => _amountFats;
            set
            {
                _amountFats = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(Fats));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
            }
        }

        public double Carbohydrates
        {
            get => _amountCarbohydrates;
            set
            {
                _amountCarbohydrates = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(Carbohydrates));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
            }
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            string nutritionalValues = NavigationParameterConverter.ObjectToPairKeyValue(_nutritionalValuesList, "NutritionalValues");
            string units = NavigationParameterConverter.ObjectToPairKeyValue(_unitsList, "Units");
            string isFromProducts = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromProducts");
            await Shell.Current.GoToAsync($"..?{nutritionalValues}&{units}&{isFromProducts}");
        }

        [RelayCommand]
        private async Task Save()
        {
            NutritionalValueModel nutritionalValue;
            
            if (!IsEdit)
            {
                _unitsList.Add(SelectedUnits);
                nutritionalValue = new()
                {
                    Id = Guid.NewGuid().ToString(),
                    FoodId = _foodId,
                    UnitsId = SelectedUnits.Id,
                    UnitsAmount = UnitsAmount,
                    Kcal = Kcal,
                    Proteins = Proteins,
                    Fats = Fats,
                    Carbohydrates = Carbohydrates,
                    IsDefault = false
                };
                _nutritionalValuesList.Add(nutritionalValue);
            }
            else
            {
                _unitsList.Insert(_unitsIndex, SelectedUnits);
                nutritionalValue = _nutritionalValuesList.Find(n => n.UnitsId == SelectedUnits.Id);
                nutritionalValue.Kcal = Kcal;
                nutritionalValue.Proteins = Proteins;
                nutritionalValue.Fats = Fats;
                nutritionalValue.Carbohydrates = Carbohydrates;

                int index = _nutritionalValuesList.FindIndex(n => n.Id == nutritionalValue.Id);
                _nutritionalValuesList[index] = nutritionalValue;
            }

            string nutritionalValues = NavigationParameterConverter.ObjectToPairKeyValue(_nutritionalValuesList, "NutritionalValues");
            string units = NavigationParameterConverter.ObjectToPairKeyValue(_unitsList, "Units");
            string isFromProducts = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromProducts");
            await Shell.Current.GoToAsync($"..?{nutritionalValues}&{units}&{isFromProducts}");
        }

        [RelayCommand]
        private void UnitsAmountChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                UnitsAmount = 0;
        }

        [RelayCommand]
        private void KcalChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                Kcal = 0;
        }

        [RelayCommand]
        private void ProteinsChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                Proteins = 0;
        }

        [RelayCommand]
        private void FatsChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                Fats = 0;
        }

        [RelayCommand]
        private void CarbohydtratesChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                Carbohydrates = 0;
        }

        #endregion

        #region Конструктор

        public AddingUnitsPageViewModel()
        {

        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string unitsId = string.Empty;

            if (query.ContainsKey("Units"))
            {
                _unitsList = NavigationParameterConverter.ObjectFromUrl<List<UnitsModel>>(query["Units"]);
            }

            if (query.ContainsKey("NutritionalValues"))
            {
                _nutritionalValuesList = NavigationParameterConverter.ObjectFromUrl<List<NutritionalValueModel>>(query["NutritionalValues"]);
            }

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

            if (query.ContainsKey("IsFromProduct"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsFromProduct"]);
                IsFromProduct = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            if (query.ContainsKey("UnitsId"))
            {
                unitsId = HttpUtility.UrlDecode(query["UnitsId"]);
                unitsId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(unitsId);
            }

            if (query.ContainsKey("UnitsIndex"))
            {
                _unitsIndex = NavigationParameterConverter.ObjectFromUrl<int>(query["UnitsIndex"]);
            }

            if (query.ContainsKey("FoodId"))
            {
                _foodId = NavigationParameterConverter.ObjectFromUrl<string>(query["FoodId"]);
            }

            LoadDataAfterNavigation(unitsId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(string unitsId)
        {
            List<UnitsModel> unitsBuf = await GlobalDataStore.Units.GetAllItemsAsync();
            Units = [];

            foreach (UnitsModel units in unitsBuf)
            {
                if (!_unitsList.Exists(u => u.Id == units.Id))
                    Units.Add(units);
            }

            if (!IsFromProduct)
                Units = Units.Where(u => u.Name != "порция").ToList();

            Units = [.. Units.OrderBy(u => u.Name)];

            if (IsEdit)
            {
                SelectedUnits = Units.Find(u => u.Id == unitsId);
                NutritionalValueModel nutritionalValue = _nutritionalValuesList.Find(n => n.UnitsId == unitsId);
                Kcal = nutritionalValue.Kcal;
                Proteins = nutritionalValue.Proteins;
                Fats = nutritionalValue.Fats;
                Carbohydrates = nutritionalValue.Carbohydrates;
            }
            else
            {
                SelectedUnits = Units.FirstOrDefault();
                UnitsAmount = 1;
                Kcal = 100;
                Proteins = 100;
                Fats = 100;
                Carbohydrates = 100;
            }
        }

        #endregion
    }
}
