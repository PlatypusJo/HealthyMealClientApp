using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public class ProductsListsPageViewModel : BaseViewModel
    {
        #region Поля и свойства

        private readonly int _pageSize = 5;

        private int _pageIndex = 1;
        public int PageIndex
        { 
            get => _pageIndex;
            set 
            { 
                _pageIndex = value; 
                NotifyPropertyChanged(nameof(PageIndex));
            }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }

        private List<ProductToBuyModel> _productsToBuy;

        public ObservableCollection<ProductToBuyModel> ProductsToBuy { get; set; }

        public ICommand NextPageCommand { get; private set; }
        public ICommand BackPageCommand { get; private set; }
        public ICommand CheckBoxChangedCommand { get; private set; }

        #endregion

        #region Конструкторы

        public ProductsListsPageViewModel()
        {
            NextPageCommand = new Command(NextPage);
            BackPageCommand = new Command(BackPage);
            CheckBoxChangedCommand = new Command(FindChangedItemAndUpdate);
            LoadProducts();
            _isVisible = _productsToBuy.Count > 0;
        }

        #endregion

        #region Методы

        private void FindChangedItemAndUpdate(object arg)
        {
            if (arg is string id)
            {
                ProductToBuyModel changedItem = _productsToBuy.Find(p => p.Id == id);
                Debug.WriteLine($"{changedItem.Name} is {changedItem.IsBought}");
            }
        }

        public void NextPage()
        {
            SwitchPageAndReloadData(_pageIndex + 1);
        }

        public void BackPage()
        {
            SwitchPageAndReloadData(_pageIndex - 1);
        }

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
            PageIndex = pageNumber;
        }

        private void LoadProducts()
        {
            int count = 13;
            _productsToBuy = new List<ProductToBuyModel>();

            for (int i = 0; i < count; i++)
            {
                _productsToBuy.Add(new ProductToBuyModel()
                {
                    Id = i.ToString(),
                    ProductId = i,
                    UnitsId = i,
                    UnitsName = "у.е.",
                    Name = "Test" + i.ToString(),
                    Amount = 100,
                    IsBought = false,
                });
            }

            ProductsToBuy = new ObservableCollection<ProductToBuyModel>();
            for (int i = 0; i < _productsToBuy.Count && i < _pageSize; i++)
            {
                ProductsToBuy.Add(_productsToBuy[i]);
            }
        }

        #endregion
    }
}
