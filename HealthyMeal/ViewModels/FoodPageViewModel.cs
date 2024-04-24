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
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class FoodPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 15;

        private DateTime _date;

        List<FoodModel> _foods;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private bool _isVisible = true;

        [ObservableProperty]
        private bool _isVisibleToNext;

        [ObservableProperty]
        private bool _isVisibleToPrevious;

        [ObservableProperty]
        private ObservableCollection<FoodModel> _foodsToShow;

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private bool _isAdd;

        [ObservableProperty]
        private MealTypeModel _selectedMealType;

        #endregion

        #region Свойства

        public string Day
        {
            get => _date.ToString("dd.MM, ddd");
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        private async Task OpenSavingFoodPage(FoodModel food)
        {
            await Shell.Current.GoToAsync($"{nameof(SavingFoodPage)}");
        }

        [RelayCommand]
        private void NextPage()
        {
            SwitchPageAndReloadData(PageIndex + 1);
        }

        [RelayCommand]
        private void BackPage()
        {
            SwitchPageAndReloadData(PageIndex - 1);
        }

        #endregion

        #region Конструкторы

        public FoodPageViewModel()
        {
            LoadData();
            _isVisible = _foodsToShow.Count > 0;
            _isVisibleToNext = true;
            _isVisibleToPrevious = true;
            LoadMealTypesAsync();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("MealTypeId"))
            {
                string mealTypeId = HttpUtility.UrlDecode(query["MealTypeId"]);
                mealTypeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(mealTypeId);
                SelectedMealType = MealTypes.Find(x => x.Id == mealTypeId);
            }
            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
                OnPropertyChanged(nameof(Day));
            }
            if (query.ContainsKey("IsAdd"))
            {
                string isAdd = HttpUtility.UrlDecode(query["IsAdd"]);
                IsAdd = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(isAdd);
                if (IsAdd)
                    ClearSearchBarAndReloadData();
            }

            //try
            //{
                
            //}
            //catch 
            //{ 
                
            //}
        }

        #endregion

        #region Внутренние методы

        private void LoadData()
        {
            FoodsToShow =
            [
                new()
                {
                    Name = "Очень длинное название еды, чтобы проверить работу",
                    Kcal = 100,
                    DefaultUnitsAmount = 100,
                    DefaultUnitsName = "г"
                },
                new()
                {
                    Name = "Очень длинное название еды, чтобы проверить работу и посмотреть, что будет при >2 строк",
                    Kcal = 233,
                    DefaultUnitsAmount = 1,
                    DefaultUnitsName = "ст. ложка"
                },
                new()
                {
                    Name = "Тест",
                    Kcal = 233,
                    DefaultUnitsAmount = 1,
                    DefaultUnitsName = "шт"
                },
                new()
                {
                    Name = "Халлоу",
                    Kcal = 199,
                    DefaultUnitsAmount = 1,
                    DefaultUnitsName = "л"
                }
            ];
        }

        private async void LoadMealTypesAsync()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
        }

        private void SwitchPageAndReloadData(int pageNumber)
        {
            
        }

        private void ClearSearchBarAndReloadData()
        {

        }

        #endregion
    }
}
