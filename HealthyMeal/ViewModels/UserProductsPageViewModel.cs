using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Services.BLL;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class UserProductsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private readonly int _pageSize = 15;

        private string _userId = string.Empty;

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



        #endregion

        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
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

        [RelayCommand]
        private async Task OpenCreateProductPage()
        {

        }

        [RelayCommand]
        private async Task OpenEditProductPage(FoodModel food)
        {

        }

        [RelayCommand]
        private async Task RemoveProduct(FoodModel food)
        {

        }

        #endregion

        #region Конструкторы

        public UserProductsPageViewModel() { }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query is null)
                return;

            bool isFromProfile = false;

            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }

            if (query.ContainsKey("IsFromProfile"))
            {
                string strBuf = HttpUtility.UrlDecode(query["IsFromProfile"]);
                isFromProfile = NavigationParameterConverter.ObjectFromPairKeyValue<bool>(strBuf);
            }

            LoadDataAfterNavigation(isFromProfile);
        }


        #endregion

        #region Внутренние методы

        private void LoadDataAfterNavigation(bool isFromProfile)
        {
            if (isFromProfile)
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
