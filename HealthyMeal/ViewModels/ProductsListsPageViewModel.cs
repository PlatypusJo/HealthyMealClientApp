using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Services.BLL;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class ProductsListsPageViewModel : BaseViewModel
    {
        #region Поля

        private UserModel _user;

        private DateTime _date;

        private readonly int _pageSize = 15;

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
        private bool _isPopupEditVisible = false;

        [ObservableProperty]
        private bool _isPopupDeleteVisible = false;

        [ObservableProperty]
        private bool _isNextPopupVisible = false;

        [ObservableProperty]
        private string _nextPopupText = string.Empty;

        [ObservableProperty]
        private DateTime _selectedDate;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedDateFormat))]
        private ObservableCollection<ProductToBuyModel> _shopListToShow = [];

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsCollectionVisible))]
        private bool _isEmptyCollection = true;

        #endregion

        #region Свойства

        public string SelectedDateFormat => DateTime.Now.Year != SelectedDate.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public bool IsCollectionVisible => !IsEmptyCollection;

        public string DateFormat => DateTime.Now.Year != Date.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                PageIndex = 1;
                SwitchPageAndReloadData(PageIndex);
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(DateFormat));
                
            }
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenProductsPage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue("1", "UserId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(Date, nameof(Date));
            string isFromShopList = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromShopList");
            await Shell.Current.GoToAsync($"{nameof(ProductsPage)}?{userId}&{date}&{isFromShopList}");
        }

        [RelayCommand]
        private void CheckBoxChanged(object arg)
        {
            if (arg is string id)
            {
                ProductToBuyModel changedItem = ShopListToShow.ToList().Find(p => p.Id == id);
                if (changedItem != null) 
                    Debug.WriteLine($"{changedItem.FoodName} is {changedItem.IsBought}");
            }
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
        private void ItemTapped(ProductToBuyModel item)
        {
            if (item == null)
                return;
        }

        [RelayCommand]
        private void OpenEditPopup()
        {
            SelectedDate = Date;
            IsPopupEditVisible = true;
        }

        [RelayCommand]
        private void OpenDeletePopup()
        {
            IsPopupDeleteVisible = true;
        }

        [RelayCommand]
        private void ClosePopup()
        {
            IsPopupEditVisible = false;
            IsPopupDeleteVisible = false;
            IsNextPopupVisible = false;
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            // Если на выбранную дату нет других списков, то изменяем
            // Иначе выдаём сообщение, что невозможно перенести.

            bool result = true;
            IsPopupEditVisible = false;
            if (result)
            {
                IsNextPopupVisible = true;
                NextPopupText = "На эту дату уже есть список";
            }
            else
            {
                IsNextPopupVisible = true;
                NextPopupText = "Список успешно перенесён";
            }

        }

        [RelayCommand]
        private async Task DeleteShoppingList()
        {
            IsPopupDeleteVisible = false;
        }

        #endregion
        
        #region Конструкторы

        public ProductsListsPageViewModel()
        {
            _user = new()
            {
                Id = "1",
                Name = "Иван",
                Login = "LoVan",
                Rdc = 2500,
                KcalAmountGoal = 2000,
                Age = 25,
                Height = 176,
                Weight = 73,
            };
            DateTime today = DateTime.Now;
            _date = new DateTime(today.Year, today.Month, today.Day);
            IsPopupEditVisible = false;
            IsPopupDeleteVisible = false;
            IsNextPopupVisible = false;
        }

        #endregion

        #region Методы

        public void LoadDataAfterNavigation()
        {
            IsPopupEditVisible = false;
            IsPopupDeleteVisible = false;
            IsNextPopupVisible = false;
            PageIndex = 1;
            SwitchPageAndReloadData(PageIndex);
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(DateFormat));
        }

        #endregion

        #region Внутренние методы

        private async void SwitchPageAndReloadData(int pageNumber)
        {
            if (pageNumber < 0)
                return;

            int productsCount = await LoadDataToShow(pageNumber);

            IsEmptyCollection = productsCount == 0;
            IsVisible = productsCount > _pageSize;
            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == productsCount / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private async Task<int> LoadDataToShow(int curPage)
        {
            ShopListToShow.Clear();

            GetShopListPageResponseModel response = await BlService.GetShopListPage(_user.Id, _pageSize, curPage, _date);
            foreach (ProductToBuyModel product in response.ShopList)
            {
                ShopListToShow.Add(product);
            }

            return response.Count;
        }

        #endregion
    }
}
