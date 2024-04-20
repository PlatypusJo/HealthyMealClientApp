using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class FoodPageViewModel : BaseViewModel
    {
        #region Поля

        private int _pageIndex = 1;

        private bool _isVisible = true;

        private bool _isVisibleToNext;

        private bool _isVisibleToPrevious;

        ObservableCollection<FoodModel> _foods;

        #endregion

        #region Свойства

        public int PageIndex
        {
            get => _pageIndex;
            set
            {
                _pageIndex = value;
                NotifyPropertyChanged(nameof(PageIndex));
            }
        }

        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsVisibleToNext
        {
            get => _isVisibleToNext;
            set
            {
                _isVisibleToNext = value;
                NotifyPropertyChanged(nameof(IsVisibleToNext));
            }
        }

        public bool IsVisibleToPrevious
        {
            get => _isVisibleToPrevious;
            set
            {
                _isVisibleToPrevious = value;
                NotifyPropertyChanged(nameof(IsVisibleToPrevious));
            }
        }

        public ObservableCollection<FoodModel> Foods
        {
            get => _foods;
        }

        #endregion

        #region Команды

        public ICommand NextPageCommand { get; private set; }

        public ICommand BackPageCommand { get; private set; }

        #endregion

        #region Конструкторы

        public FoodPageViewModel()
        {
            LoadData();
            _isVisible = _foods.Count > 0;
            _isVisibleToNext = true;
            _isVisibleToPrevious = true;
        }

        #endregion

        #region Методы

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

        #endregion

        #region Внутренние методы

        private void LoadData()
        {
            _foods =
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

        #endregion
    }
}
