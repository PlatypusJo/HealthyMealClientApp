using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using HealthyMeal.Views;
using Microcharts;
using SkiaSharp;
using HealthyMeal.Models;

namespace HealthyMeal.ViewModels
{
    public class DiaryPageViewModel : BaseViewModel
    {
        #region Поля и свойства

        private DonutChart _chart;
        public DonutChart Chart
        {
            get => _chart;
            set
            {
                _chart = value;
                NotifyPropertyChanged(nameof(Chart));
            }
        }

        private double _proteinsAmount = 250;
        public double ProteinsAmount
        {
            get => _proteinsAmount;
            set
            {
                _proteinsAmount = value;
            }
        }
        
        private double _fatsAmount = 150;
        public double FatsAmount
        {
            get => _fatsAmount;
            set
            {
                _fatsAmount = value;
            }
        }
        
        private double _carbohydratesAmount = 600;
        public double CarbohydratesAmount
        {
            get => _carbohydratesAmount;
            set
            {
                _carbohydratesAmount = value;
            }
        }

        public ICommand OpenMealsPageCommand { get; private set; }

        #endregion

        #region Конструкторы

        public DiaryPageViewModel() 
        {
            OpenMealsPageCommand = new Command(OnPlusButtonClick);
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
                    Color = SKColor.Parse("#F1696B"),
                    ValueLabelColor = SKColor.Parse("#F1696B"),
                    TextColor = SKColor.Parse("#000000")
                },
                new ChartEntry(fatsPercent)
                {
                    Label = "Жиры",
                    ValueLabel = fatsPercent.ToString() + "%",
                    Color = SKColor.Parse("#FFBC1F"),
                    ValueLabelColor = SKColor.Parse("#FFBC1F"),
                    TextColor = SKColor.Parse("#000000")
                },
                new ChartEntry(carbohydratesPercent)
                {
                    Label = "Углеводы",
                    ValueLabel = carbohydratesPercent.ToString() + "%",
                    Color = SKColor.Parse("#63BBB8"),
                    ValueLabelColor = SKColor.Parse("#63BBB8"),
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

        public async void OnPlusButtonClick()
        {
            await Shell.Current.GoToAsync($"//{nameof(FoodPage)}");
        }

        #endregion
    }
}
