using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using HealthyMeal.Views;
using Microcharts;
using SkiaSharp;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using HealthyMeal.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using HealthyMeal.Models;
using System.Linq;
using HealthyMeal.Intefaces;
using System.Web;
using System.Collections.ObjectModel;

namespace HealthyMeal.ViewModels
{
    public partial class DiaryPageViewModel : BaseViewModel
    {
        #region Поля

        private UserModel _user;

        private DateTime _date;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private ObservableCollection<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private DonutChart _chart;

        [ObservableProperty]
        private double _proteinsAmount;

        [ObservableProperty]
        private double _fatsAmount;

        [ObservableProperty]
        private double _carbohydratesAmount;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RdcPercent))]
        [NotifyPropertyChangedFor(nameof(KcalRemainder))]
        private double _kcalConsumed;

        #endregion

        #region Свойства

        public string DateFormat => DateTime.Now.Year != Date.Year ? "dd MMM yyyy" : "MMM dd, dddd";

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                LoadDataByDateAsync();
                OnPropertyChanged(nameof(Date));
                OnPropertyChanged(nameof(DateFormat));
            }
        }

        public double KcalAmountGoal => _user.KcalAmountGoal;

        public double KcalRemainder => Math.Round(_user.KcalAmountGoal - KcalConsumed, 1, MidpointRounding.AwayFromZero);

        public double RdcPercent => Math.Round(KcalConsumed * 100 / _user.Rdc, 1, MidpointRounding.AwayFromZero);

        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenFoodPage(MealTypeModel mealType)
        {
            mealType.ResetKcalCount();
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_user.Id, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(mealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(Date, nameof(Date));
            string isFromDiary = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromDiary");
            await Shell.Current.GoToAsync($"{nameof(FoodPage)}?{userId}&{mealTypeId}&{date}&{isFromDiary}");
        }

        [RelayCommand]
        private async Task OpenDaysPage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_user.Id, "UserId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(Date, nameof(Date));
            await Shell.Current.GoToAsync($"{nameof(SchedulePage)}?{userId}&{date}");
        }

        [RelayCommand]
        private async Task ItemTapped(MealTypeModel mealType)
        {
            mealType.ResetKcalCount();
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_user.Id, "UserId");
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(mealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(Date, nameof(Date));
            string isFromDiary = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromDiary");
            await Shell.Current.GoToAsync($"{nameof(MealsPage)}?{userId}&{mealTypeId}&{date}&{isFromDiary}");
        }

        #endregion

        #region Конструкторы

        public DiaryPageViewModel() 
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
            LoadMealTypesAsync();
            DateTime today = DateTime.Now;
            _date = new DateTime(today.Year, today.Month, today.Day);
        }

        #endregion

        #region Методы

        public void LoadDataAfterNavigation()
        {
            LoadDataByDateAsync();
            OnPropertyChanged(nameof(Date));
            OnPropertyChanged(nameof(DateFormat));
        }

        #endregion

        #region Внутренние методы

        private void LoadChartData(IReadOnlyList<INutritionalValue> nutritionalValues)
        {
            ProteinsAmount = nutritionalValues.Sum(n => n.Proteins);
            FatsAmount = nutritionalValues.Sum(n => n.Fats);
            CarbohydratesAmount = nutritionalValues.Sum(n => n.Carbohydrates);

            double nutrientsTotalAmount = ProteinsAmount + FatsAmount + CarbohydratesAmount;
            float proteinsPercent = (float)Math.Round(ProteinsAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            float fatsPercent = (float)Math.Round(FatsAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            float carbohydratesPercent = (float)Math.Round(CarbohydratesAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);

            List<ChartEntry> chartEntries =
            [
                new(proteinsPercent)
                {
                    Label = "Белки",
                    ValueLabel = proteinsPercent.ToString() + "%",
                    Color = SKColor.Parse("#E30956"),
                    ValueLabelColor = SKColor.Parse("#E30956"),
                    TextColor = SKColor.Parse("#000000")
                },
                new(fatsPercent)
                {
                    Label = "Жиры",
                    ValueLabel = fatsPercent.ToString() + "%",
                    Color = SKColor.Parse("#FFD40B"),
                    ValueLabelColor = SKColor.Parse("#FFD40B"),
                    TextColor = SKColor.Parse("#000000")
                },
                new(carbohydratesPercent)
                {
                    Label = "Углеводы",
                    ValueLabel = carbohydratesPercent.ToString() + "%",
                    Color = SKColor.Parse("#1753B1"),
                    ValueLabelColor = SKColor.Parse("#1753B1"),
                    TextColor = SKColor.Parse("#000000")
                },
            ];

            Chart = new DonutChart 
            { 
                Entries = chartEntries, 
                LabelTextSize = 40,
                HoleRadius = 0.7f,
                BackgroundColor = SKColor.Empty
            };
        }

        private async void LoadDataByDateAsync()
        {
            List<MealModel> meals = await GlobalDataStore.Meals.GetAllItemsAsync();
            meals = meals.Where(x => x.Date == Date).ToList();

            List<NutritionalValueModel> nutritionalValues = await GlobalDataStore.NutritionalValues.GetAllItemsAsync();
            foreach (MealModel meal in meals)
            {
                meal.NutritionalValue = nutritionalValues.Find(n => n.FoodId == meal.FoodId && n.UnitsId == meal.UnitsId);
            }

            ObservableCollection<MealTypeModel> mealTypesBuf = [];
            foreach (MealTypeModel mealType in MealTypes)
            {
                mealType.CalcKcalCount(meals);
                mealTypesBuf.Add(mealType);
            }

            MealTypes = mealTypesBuf;
            KcalConsumed = meals.Sum(m => m.Kcal);

            LoadChartData(meals);
        }

        private async void LoadMealTypesAsync()
        {
            List<MealTypeModel> mealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. mealTypes.OrderBy(m => m.Type)];
        }

        #endregion
    }
}
