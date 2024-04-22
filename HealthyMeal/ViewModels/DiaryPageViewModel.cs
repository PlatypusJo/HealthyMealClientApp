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

namespace HealthyMeal.ViewModels
{
    public partial class DiaryPageViewModel : BaseViewModel
    {
        #region Поля



        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private DonutChart _chart;

        [ObservableProperty]
        private double _proteinsAmount = 250;

        [ObservableProperty]
        private double _fatsAmount = 150;

        [ObservableProperty]
        private double _carbohydratesAmount = 600;

        #endregion

        #region Свойства

        

        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenFoodPage()
        {
            await Shell.Current.GoToAsync($"{nameof(FoodPage)}");
        }

        #endregion

        #region Конструкторы

        public DiaryPageViewModel() 
        {
            LoadDiagramData();
        }

        #endregion

        #region Методы



        #endregion

        #region Внутренние методы

        private void LoadDiagramData()
        {
            double nutrientsTotalAmount = ProteinsAmount + FatsAmount + CarbohydratesAmount;
            float proteinsPercent = (float)Math.Round(ProteinsAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            float fatsPercent = (float)Math.Round(FatsAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            float carbohydratesPercent = (float)Math.Round(CarbohydratesAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            List<ChartEntry> chartEntries = new List<ChartEntry> 
            { 
                new ChartEntry(proteinsPercent)
                {
                    Label = "Белки",
                    ValueLabel = proteinsPercent.ToString() + "%",
                    Color = SKColor.Parse("#E30956"),
                    ValueLabelColor = SKColor.Parse("#E30956"),
                    TextColor = SKColor.Parse("#000000")
                },
                new ChartEntry(fatsPercent)
                {
                    Label = "Жиры",
                    ValueLabel = fatsPercent.ToString() + "%",
                    Color = SKColor.Parse("#FFD40B"),
                    ValueLabelColor = SKColor.Parse("#FFD40B"),
                    TextColor = SKColor.Parse("#000000")
                },
                new ChartEntry(carbohydratesPercent)
                {
                    Label = "Углеводы",
                    ValueLabel = carbohydratesPercent.ToString() + "%",
                    Color = SKColor.Parse("#1753B1"),
                    ValueLabelColor = SKColor.Parse("#1753B1"),
                    TextColor = SKColor.Parse("#000000")
                },
            };

            Chart = new DonutChart 
            { 
                Entries = chartEntries, 
                LabelTextSize = 40,
                HoleRadius = 0.7f,
                BackgroundColor = SKColor.Empty
            };
        }

        

        #endregion
    }
}
