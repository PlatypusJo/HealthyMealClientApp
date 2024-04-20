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

namespace HealthyMeal.ViewModels
{
    public partial class DiaryPageViewModel : BaseViewModel
    {
        #region Поля

        private DonutChart _chart;

        private double _proteinsAmount = 250;

        private double _fatsAmount = 150;

        private double _carbohydratesAmount = 600;

        #endregion

        #region Свойства

        public DonutChart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                NotifyPropertyChanged(nameof(Chart));
            }
        }

        public double ProteinsAmount
        {
            get => _proteinsAmount;
            set
            {
                _proteinsAmount = value;
            }
        }
        
        public double FatsAmount
        {
            get => _fatsAmount;
            set
            {
                _fatsAmount = value;
            }
        }
        
        public double CarbohydratesAmount
        {
            get => _carbohydratesAmount;
            set
            {
                _carbohydratesAmount = value;
            }
        }

        #endregion

        #region Конструкторы

        public DiaryPageViewModel() 
        {
            LoadDiagramData();
        }

        #endregion

        #region Методы

        private void LoadDiagramData()
        {
            double nutrientsTotalAmount = ProteinsAmount + FatsAmount + CarbohydratesAmount;
            float proteinsPercent = (float)Math.Round(_proteinsAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            float fatsPercent = (float)Math.Round(_fatsAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
            float carbohydratesPercent = (float)Math.Round(_carbohydratesAmount.ToPercentage(nutrientsTotalAmount), 1, MidpointRounding.AwayFromZero);
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

        [RelayCommand]
        private async Task OpenFoodPage()
        {
            await Shell.Current.GoToAsync($"{nameof(FoodPage)}");
        }

        #endregion
    }
}
