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
    public partial class SavingIngredientPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _recipeId = string.Empty;

        private NutritionalValueModel _selectedNutritionalValue = new();

        private List<NutritionalValueModel> _nutritionalValues = [];

        private UnitsModel _selectedUnits = new();

        private IngredientModel _ingredient;

        private FoodModel _food;

        private double _unitsAmount;

        private List<IngredientModel> _ingredients;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<UnitsModel> _units;

        [ObservableProperty]
        private bool _isEdit = false;

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

        public double UnitsAmount
        {
            get => _unitsAmount;
            set
            {
                _unitsAmount = value < 0 ? 0 : value;
                OnPropertyChanged(nameof(UnitsAmount));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
                NutritionalValueInfoUpdate();
            }
        }

        public bool IsEnabledSaveBtn => UnitsAmount != 0;

        public double Kcal => _selectedNutritionalValue.Kcal * UnitsAmount / _selectedNutritionalValue.UnitsAmount;

        public double Proteins => _selectedNutritionalValue.Proteins * UnitsAmount / _selectedNutritionalValue.UnitsAmount;

        public double Fats => _selectedNutritionalValue.Fats * UnitsAmount / _selectedNutritionalValue.UnitsAmount;

        public double Carbohydrates => _selectedNutritionalValue.Carbohydrates * UnitsAmount / _selectedNutritionalValue.UnitsAmount;

        public string FoodName => _food.Name;

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            string isFromRecipeCrud = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromRecipeCrud");
            await Shell.Current.GoToAsync($"..?{isFromRecipeCrud}");
        }

        [RelayCommand]
        private async Task Save()
        {
            _ingredient.NutritionalValue = _selectedNutritionalValue;
            _ingredient.UnitsAmount = UnitsAmount;
            _ingredient.FoodId = _food.Id;
            _ingredient.Name = _food.Name;
            _ingredient.UnitsId = SelectedUnits.Id;
            _ingredient.UnitsName = SelectedUnits.Name;
            _ingredient.RecipeId = _recipeId;

            if (!IsEdit)
            {
                _ingredient.Id = Guid.NewGuid().ToString();
                _ingredients.Add( _ingredient );
            }
            else
            {
                int index = _ingredients.FindIndex(i => i.Id == _ingredient.Id);
                _ingredients[index] = _ingredient;
            }

            string isFromRecipeCrud = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsFromRecipeCrud");
            await Shell.Current.GoToAsync($"..?{isFromRecipeCrud}");
        }

        [RelayCommand]
        private void TextChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                UnitsAmount = 0;
        }

        #endregion

        #region Коструктор

        public SavingIngredientPageViewModel()
        {

        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string ingredientId = string.Empty;
            string foodId = string.Empty;

            if (query.ContainsKey("RecipeId"))
            {
                string recipeId = HttpUtility.UrlDecode(query["RecipeId"]);
                _recipeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(recipeId);
            }

            if (query.ContainsKey("IsEdit"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsEdit"]);
                IsEdit = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            if (query.ContainsKey("Ingredients"))
            {
                string strBuf = HttpUtility.UrlDecode(query["Ingredients"]);
                _ingredients = NavigationParameterConverter.ObjectFromPairKeyValue<List<IngredientModel>>(strBuf);
            }

            if (query.ContainsKey("FoodId"))
            {
                foodId = HttpUtility.UrlDecode(query["FoodId"]);
                foodId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(foodId);
            }

            if (query.ContainsKey("IngredientId"))
            {
                ingredientId = HttpUtility.UrlDecode(query["IngredientId"]);
                ingredientId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(ingredientId);
            }

            LoadDataAfterNavigation(ingredientId, foodId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(string ingredientId, string foodId)
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

            if (IsEdit)
            {
                _ingredient = _ingredients.Find(i => i.Id == ingredientId);
                UnitsAmount = _ingredient.UnitsAmount;
                SelectedUnits = Units.Find(u => u.Id == _ingredient.UnitsId);
            }
            else
            {
                _ingredient = new();
                _selectedNutritionalValue = _nutritionalValues.Find(n => n.IsDefault);
                UnitsAmount = _selectedNutritionalValue.UnitsAmount;
                string unitsId = _selectedNutritionalValue.UnitsId;
                SelectedUnits = Units.Find(u => u.Id == _selectedNutritionalValue.UnitsId);

            }

            Photo = _food.Image;
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
