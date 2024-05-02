using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
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

        private DateTime _date;

        private readonly int _pageSize = 11;
        
        private List<ProductToBuyModel> _productsToBuy;

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
        private bool _isPopupEditVisible;

        [ObservableProperty]
        private bool _isPopupDeleteVisible;

        #endregion

        #region Свойства

        public ObservableCollection<ProductToBuyModel> ProductsToBuy { get; set; }

        public string DateFormat => DateTime.Now.Year != Date.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(DateFormat));
                OnPropertyChanged(nameof(Date));
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
                ProductToBuyModel changedItem = ProductsToBuy.ToList().Find(p => p.Id == id);
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
        }

        [RelayCommand]
        private async Task SaveChanges()
        {
            IsPopupEditVisible = false;
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
            LoadProducts();
            IsVisible = _productsToBuy.Count > 0;
            IsVisibleToPrevious = !(_pageIndex == 1);
            IsVisibleToNext = !(_pageIndex == _productsToBuy.Count / _pageSize + 1);
            DateTime today = DateTime.Now;
            Date = new DateTime(today.Year, today.Month, today.Day);
            IsPopupEditVisible = false;
            IsPopupDeleteVisible = false;
        }

        #endregion

        #region Методы

       

        #endregion

        #region Внутренние методы

        private void SwitchPageAndReloadData(int pageNumber)
        {
            int index = pageNumber - 1;
            if (index < 0 || index > _productsToBuy.Count / _pageSize)
                return;

            ProductsToBuy.Clear();
            int startIndex = index * _pageSize;
            for (int i = startIndex; i < _productsToBuy.Count && i < startIndex + _pageSize; i++)
            {
                ProductsToBuy.Add(_productsToBuy[i]);
            }

            IsVisibleToPrevious = !(pageNumber == 1);
            IsVisibleToNext = !(pageNumber == _productsToBuy.Count / _pageSize + 1);
            PageIndex = pageNumber;
        }

        private void LoadProducts()
        {
            int count = 22;
            _productsToBuy = [];

            for (int i = 0; i < count; i++)
            {
                _productsToBuy.Add(new ProductToBuyModel()
                {
                    Id = i.ToString(),
                    FoodId = i.ToString(),
                    UnitsId = i.ToString(),
                    UnitsName = "у.е.",
                    FoodName = "Test" + i.ToString(),
                    UnitsAmount = 100,
                    IsBought = false,
                });
            }
            _productsToBuy.Add(new ProductToBuyModel()
            {
                Id = "1234",
                FoodId = "1234",
                UnitsId = "12123",
                UnitsName = "у.е.",
                FoodName = "Очень длинное название продукта чтобы все с ума посходили от этого приложения",
                UnitsAmount = 100,
                IsBought = false,
            });

            ProductsToBuy = [];
            for (int i = 0; i < _productsToBuy.Count && i < _pageSize; i++)
            {
                ProductsToBuy.Add(_productsToBuy[i]);
            }
        }

        #endregion
    }
}
