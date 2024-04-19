using HealthyMeal.Models;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public class FoodPageViewModel : BaseViewModel
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

        public ICommand GoBackCommand { get; private set; }

        public ICommand OpenSavingFoodPageCommand { get; private set; }

        public ICommand NextPageCommand { get; private set; }

        public ICommand BackPageCommand { get; private set; }

        #endregion

        #region Конструкторы

        public FoodPageViewModel()
        {
            LoadData();
            GoBackCommand = new Command(OnGoBackButtonClick);
            OpenSavingFoodPageCommand = new Command<FoodModel>(OpenSavingFoodPage);
            _isVisible = _foods.Count > 0;
            _isVisibleToNext = true;
            _isVisibleToPrevious = true;
        }

        #endregion

        #region Методы

        public async void OnGoBackButtonClick()
        {
            await Shell.Current.GoToAsync($"..");
        }

        public async void OpenSavingFoodPage(FoodModel food)
        {
            await Shell.Current.GoToAsync($"{nameof(SavingFoodPage)}");
        }

        #endregion

        #region Внутренние методы

        private void LoadData()
        {
            _foods = new ObservableCollection<FoodModel>()
            {
                new FoodModel()
                {
                    Name = "Очень длинное название еды, чтобы проверить работу",
                    Kcal = 100,
                    Amount = 100,
                    UnitsName = "г"
                },
                new FoodModel()
                {
                    Name = "Очень длинное название еды, чтобы проверить работу и посмотреть, что будет при >2 строк",
                    Kcal = 233,
                    Amount = 1,
                    UnitsName = "ст. ложка"
                },
                new FoodModel()
                {
                    Name = "Тест",
                    Kcal = 233,
                    Amount = 1,
                    UnitsName = "шт"
                },
                new FoodModel()
                {
                    Name = "Халлоу",
                    Kcal = 199,
                    Amount = 1,
                    UnitsName = "л"
                }
            };
        }

        #endregion
    }
}
