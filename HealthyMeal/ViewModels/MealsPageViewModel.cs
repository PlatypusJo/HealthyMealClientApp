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
using Xamarin.Forms;
using static Xamarin.Essentials.Permissions;

namespace HealthyMeal.ViewModels
{
    public partial class MealsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 15;

        private string _userId = string.Empty;

        private DateTime _date;

        List<MealModel> _meals = [];

        private MealTypeModel _selectedMealType;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private int _pageIndex = 1;

        [ObservableProperty]
        private bool _isVisible = false;

        [ObservableProperty]
        private bool _isVisibleToNext = false;

        [ObservableProperty]
        private bool _isVisibleToPrevious = false;

        [ObservableProperty]
        private ObservableCollection<MealModel> _mealsToShow = [];

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private bool _isFromDiary = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCollectionVisible))]
        private bool _isEmptyCollection = true;

        #endregion

        #region Свойства

        public string Day
        {
            get => _date.ToString("dd.MM, ddd").ToUpper();
        }

        public bool IsCollectionVisible => !IsEmptyCollection;

        public MealTypeModel SelectedMealType
        {
            get => _selectedMealType;
            set
            {
                _selectedMealType = value;
                LoadDataByMealTypeAsync();
                OnPropertyChanged(nameof(SelectedMealType));
            }
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        private async Task OpenSavingFoodPage(MealModel meal)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(SelectedMealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string mealId = NavigationParameterConverter.ObjectToPairKeyValue(meal.Id, "MealId");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(meal.FoodId, "FoodId");
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsEdit");
            await Shell.Current.GoToAsync($"{nameof(SavingFoodPage)}?{userId}&{mealTypeId}&{date}&{foodId}&{isEdit}&{mealId}");
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

        [RelayCommand]
        private async Task RemoveMeal(MealModel meal)
        {
            await GlobalDataStore.Meals.DeleteItemAsync(meal.Id);
            _meals = await GlobalDataStore.Meals.GetAllItemsAsync();
            _meals = _meals.Where(x => x.Date == _date && x.MealTypeId == SelectedMealType.Id).ToList();

            IsEmptyCollection = _meals.Count == 0;
            SwitchPageAndReloadData(_meals.Count / _pageSize + 1);
        }

        #endregion

        #region Конструкторы

        public MealsPageViewModel()
        {
            LoadMealTypesAsync();
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            string mealTypeId = string.Empty;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("MealTypeId"))
            {
                string strBuf = HttpUtility.UrlDecode(query["MealTypeId"]);
                mealTypeId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(strBuf);
            }

            if (query.ContainsKey("IsFromDiary"))
            {
                string isAddStr = HttpUtility.UrlDecode(query["IsFromDiary"]);
                IsFromDiary = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(isAddStr);
            }

            if (query.ContainsKey("Date"))
            {
                string date = HttpUtility.UrlDecode(query["Date"]);
                _date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
            }
            else
            {
                DateTime today = DateTime.Now;
                _date = new(today.Year, today.Month, today.Day);
            }

            OnPropertyChanged(nameof(Day));

            if (mealTypeId != string.Empty)
            {
                SelectedMealType = MealTypes.Find(x => x.Id == mealTypeId);
            }
            else
            {
                SelectedMealType = MealTypes.Find(x => x.Type == MealType.Breakfast);
            }
        }


        #endregion

        #region Внутренние методы

        private async void LoadDataByMealTypeAsync()
        {
            List<MealModel> meals = await GlobalDataStore.Meals.GetAllItemsAsync();
            meals = meals.Where(x => x.Date == _date && x.MealTypeId == SelectedMealType.Id).ToList();
            _meals = meals;

            IsVisible = _meals.Count > _pageSize;
            IsEmptyCollection = _meals.Count == 0;

            if (IsFromDiary)
            {
                PageIndex = 1;
                SwitchPageAndReloadData(PageIndex);
            }
            else
            {
                SwitchPageAndReloadData(_meals.Count / _pageSize + 1);
            }
            
        }

        private async void LoadMealTypesAsync()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
        }

        private void SwitchPageAndReloadData(int pageNumber)
        {
            int index = pageNumber - 1;
            if (index < 0 || index > _meals.Count / _pageSize)
                return;

            LoadDataToShow(index);

            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == _meals.Count / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private void LoadDataToShow(int curIndexPage)
        {
            MealsToShow.Clear();
            int startIndex = curIndexPage * _pageSize;
            for (int i = startIndex; i < _meals.Count && i < startIndex + _pageSize; i++)
            {
                MealsToShow.Add(_meals[i]);
            }
        }

        #endregion
    }
}
