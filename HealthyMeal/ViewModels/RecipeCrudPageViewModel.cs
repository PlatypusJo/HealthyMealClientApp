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
    public partial class RecipeCrudPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId;

        private UserModel _user;

        private UnitsModel _selectedUnitsDefault;

        private NutritionalValueModel _nutritionalValueDefault;

        private string _foodId;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private string _recipeName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private ObservableCollection<UnitsModel> _units;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private ObservableCollection<IngredientModel> _ingredients;

        [ObservableProperty]
        private List<NutritionalValueModel> _nutritionalValues;

        [ObservableProperty]
        private bool _isFromRecipes = false;

        [ObservableProperty]
        private bool _isEdit = false;

        [ObservableProperty]
        private string _description = string.Empty;

        [ObservableProperty]
        private double _hours = 0;

        [ObservableProperty]
        private double _minutes = 0;

        #endregion

        #region Свойства

        public bool IsEnabledSaveBtn =>
            RecipeName != string.Empty
            && Units.Count > 0;

        public double PortionKcal => _nutritionalValueDefault.Kcal;

        public double PortionProteins => _nutritionalValueDefault.Proteins;

        public double PortionFats => _nutritionalValueDefault.Fats;

        public double PortionCarbohydrates => _nutritionalValueDefault.Carbohydrates;

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
            string isFromProduct = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromProduct");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(_foodId, "FoodId");
            await Shell.Current.GoToAsync($"{nameof(AddingUnitsPage)}?{nutritionalValues}&{units}&{isEdit}&{isFromProduct}&{foodId}");
        }

        [RelayCommand]
        private void NameChanged(string text)
        {
            if (text == string.Empty)
                RecipeName = string.Empty;
        }

        [RelayCommand]
        private void TextChanged(string text)
        {
            Description = Regex.Replace(text, @"[\(\!@\#\$%\^&\*\(\)_\+=\-'\\:\|/`~\.\{}\)]", "");
        }

        [RelayCommand]
        private void HoursChanged(double value)
        {
            Hours = value;
        }

        [RelayCommand]
        private void MinutesChanged(double value) 
        { 
            Minutes = value; 
        }

        [RelayCommand]
        private async Task OpenIngredientsPage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string isFromRecipeCrud = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromRecipeCrud");
            string ingredients = NavigationParameterConverter.ObjectToPairKeyValue(Ingredients, nameof(Ingredients));
            await Shell.Current.GoToAsync($"{nameof(IngredientsPage)}?{userId}&{isFromRecipeCrud}&{ingredients}");
        }

        #endregion

        #region Конструкторы

        public RecipeCrudPageViewModel() { }

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

            if (query.ContainsKey("IsFromRecipes"))
            {
                IsFromRecipes = NavigationParameterConverter.ObjectFromUrl<bool>(query["IsFromRecipes"]);
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

            if (query.ContainsKey("Ingredients"))
            {
                List<IngredientModel> ingredientsBuf = NavigationParameterConverter.ObjectFromUrl<List<IngredientModel>>(query["Ingredients"]);
                Ingredients = new(ingredientsBuf);
            }

            if (query.ContainsKey("FoodId"))
            {
                foodId = NavigationParameterConverter.ObjectFromUrl<string>(query["FoodId"]);
            }

            LoadDataAfterNavigation(foodId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(string foodId)
        {
            _user = await GlobalDataStore.Users.GetItemAsync(_userId);
            string unitsDefaultId = string.Empty;

            if (IsFromRecipes)
            {
                List<UnitsModel> units = await GlobalDataStore.Units.GetAllItemsAsync();
                _foodId = Guid.NewGuid().ToString();
                RecipeName = string.Empty;
                Units = [];
                NutritionalValues = [];
                Ingredients = [];
                Hours = 0;
                Minutes = 1;
                _nutritionalValueDefault = new() { IsDefault = true };
                _selectedUnitsDefault = units.Find(x => x.Name == "порция");
                OnPropertyChanged(nameof(PortionKcal));
                OnPropertyChanged(nameof(PortionProteins));
                OnPropertyChanged(nameof(PortionFats));
                OnPropertyChanged(nameof(PortionCarbohydrates));
            }

            if (IsEdit)
            {
                FoodModel food = await GlobalDataStore.Foods.GetItemAsync(foodId);
                _foodId = food.Id;
                RecipeName = food.Name;

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
        }

        #endregion

    }
}
