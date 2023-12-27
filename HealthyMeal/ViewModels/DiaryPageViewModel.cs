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

        private int _proteinsAmount = 250;
        public int ProteinsAmount
        {
            get => _proteinsAmount;
            set
            {
                _proteinsAmount = value;
            }
        }
        
        private int _fatsAmount = 150;
        public int FatsAmount
        {
            get => _fatsAmount;
            set
            {
                _fatsAmount = value;
            }
        }
        
        private int _carbohydratesAmount = 600;
        public int CarbohydratesAmount
        {
            get => _carbohydratesAmount;
            set
            {
                _carbohydratesAmount = value;
            }
        }

        public ICommand OpenMealsPageCommand { get; private set; }

        public DiaryPageViewModel() 
        {
            OpenMealsPageCommand = new Command(OnPlusButtonClick);
            LoadDiagramData();
        }

        private void LoadDiagramData()
        {
            double nutrientsTotalAmount = ProteinsAmount + FatsAmount + CarbohydratesAmount;
            double proteinsPercent = CalculatorNutritionalValue.CalcPercentOfNutrient(ProteinsAmount, nutrientsTotalAmount);
            double fatsPercent = CalculatorNutritionalValue.CalcPercentOfNutrient(FatsAmount, nutrientsTotalAmount);
            double carbohydratesPercent = CalculatorNutritionalValue.CalcPercentOfNutrient(CarbohydratesAmount, nutrientsTotalAmount);
            List<ChartEntry> chartEntries = new List<ChartEntry> 
            { 
                new ChartEntry((float)proteinsPercent)
                {
                    Label = "Белок",
                    ValueLabel = proteinsPercent.ToString() + "%",
                    Color = SKColor.Parse("#F1696B"),
                    ValueLabelColor = SKColor.Parse("#F1696B"),
                    TextColor = SKColor.Parse("#000000")
                },
                new ChartEntry((float)fatsPercent)
                {
                    Label = "Жиры",
                    ValueLabel = fatsPercent.ToString() + "%",
                    Color = SKColor.Parse("#FFBC1F"),
                    ValueLabelColor = SKColor.Parse("#FFBC1F"),
                    TextColor = SKColor.Parse("#000000")
                },
                new ChartEntry((float)carbohydratesPercent)
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
    }
}
