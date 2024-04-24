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

namespace HealthyMeal.ViewModels
{
    public partial class DiaryPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private UserModel _user;

        private DateTime _date;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private List<MealTypeModel> _mealTypes;

        [ObservableProperty]
        private DonutChart _chart;

        [ObservableProperty]
        private double _proteinsAmount;

        [ObservableProperty]
        private double _fatsAmount;

        [ObservableProperty]
        private double _carbohydratesAmount;

        [ObservableProperty]
        private string _dateFormat;

        [ObservableProperty]
        private double _kcalConsumed;

        #endregion

        #region Свойства

        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                DateFormat = DateTime.Now.Year != _date.Year ? "dd MMM yyyy" : "MMM dd, dddd";
                LoadDataByDateAsync();
                OnPropertyChanged(nameof(Date));
            }
        }

        public double KcalAmountGoal => _user.KcalAmountGoal;

        public double KcalRemainder => _user.KcalAmountGoal - KcalConsumed;

        public double RdcPercent => KcalConsumed * 100 / _user.Rdc;

        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenFoodPage(MealTypeModel mealType)
        {
            string mealTypeId = NavigationParameterConverter.ObjectToPairKeyValue(mealType.Id, "MealTypeId");
            string date = NavigationParameterConverter.ObjectToPairKeyValue(Date, nameof(Date));
            string isAdd = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsAdd");
            await Shell.Current.GoToAsync($"{nameof(FoodPage)}?{mealTypeId}&{date}&{isAdd}");
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
            Date = new DateTime(today.Year, today.Month, today.Day);
        }

        #endregion

        #region Методы

        public void LoadDataAfterNavigation()
        {
            LoadDataByDateAsync();
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string date = HttpUtility.UrlDecode(query["date"]);
            Date = NavigationParameterConverter.ObjectFromPairKeyValue<DateTime>(date);
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

            foreach (MealTypeModel mealType in MealTypes)
            {
                mealType.CalcKcalCount(meals);
            }

            KcalConsumed = meals.Sum(m => m.Kcal);

            LoadChartData(meals);
        }

        private async void LoadMealTypesAsync()
        {
            MealTypes = await GlobalDataStore.MealTypes.GetAllItemsAsync();
            MealTypes = [.. MealTypes.OrderBy(x => x.Type)];
        }

        #endregion
    }
}
