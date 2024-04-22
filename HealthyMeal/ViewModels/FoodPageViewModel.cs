using CommunityToolkit.Mvvm.ComponentModel;
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

        ObservableCollection<FoodModel> _foods;

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

        #endregion

        #region Свойства

        public ObservableCollection<FoodModel> Foods
        {
            get => _foods;
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
