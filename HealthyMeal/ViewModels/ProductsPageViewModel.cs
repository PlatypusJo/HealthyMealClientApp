using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Services.BLL;
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

namespace HealthyMeal.ViewModels
{
    public partial class ProductsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 15;

        private string _userId = string.Empty;

        private DateTime _date;

        private string _searchBarText = string.Empty;

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
        private ObservableCollection<FoodModel> _foodsToShow = [];

        #endregion

        #region Свойства

        public string Day
        {
            get => _date.ToString("dd.MM, ddd").ToUpper();
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        [RelayCommand]
        private async Task OpenSavingToShopListPage(FoodModel food)
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(_date, "Date");
            string foodId = NavigationParameterConverter.ObjectToPairKeyValue(food.Id, "FoodId");
            string isEdit = NavigationParameterConverter.ObjectToPairKeyValue(false, "IsEdit");
            await Shell.Current.GoToAsync($"{nameof(SavingToShopListPage)}?{userId}&{date}&{foodId}&{isEdit}");
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
        private void Search(string searchText)
        {
            _searchBarText = searchText;
            PageIndex = 1;
            SwitchPageAndReloadData(PageIndex);
        }

        #endregion

        #region Конструкторы

        public ProductsPageViewModel() { }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            bool isFromShopList = false;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("IsFromShopList"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsFromShopList"]);
                isFromShopList = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
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
            LoadDataAfterNavigation(isFromShopList);
        }


        #endregion

        #region Внутренние методы

        private async void LoadDataAfterNavigation(bool isFromShopList)
        {
            if (isFromShopList)
            {
                _searchBarText = string.Empty;
                PageIndex = 1;
                SwitchPageAndReloadData(PageIndex);
            }
        }

        private async void SwitchPageAndReloadData(int pageNumber)
        {
            if (pageNumber < 0)
                return;

            int foodsCount = await LoadDataToShow(pageNumber);

            IsVisible = foodsCount > _pageSize;
            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == foodsCount / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private async Task<int> LoadDataToShow(int curPage)
        {
            FoodsToShow.Clear();

            GetFoodPageResponseModel response = await BlService.GetFoodPage(_userId, _pageSize, curPage, _searchBarText);
            foreach (FoodModel food in response.Foods)
            {
                FoodsToShow.Add(food);
            }

            return response.Count;
        }

        #endregion
    }
}
