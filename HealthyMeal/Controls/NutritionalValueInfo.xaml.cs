using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyMeal.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NutritionalValueInfo : ContentView
    {
        public static readonly BindableProperty KcalProperty = BindableProperty.Create(
            nameof(Kcal),
            typeof(string),
            typeof(NutritionalValueInfo),
            string.Empty);
        public static readonly BindableProperty ProteinsProperty = BindableProperty.Create(
            nameof(Proteins),
            typeof(string),
            typeof(NutritionalValueInfo),
            string.Empty);
        public static readonly BindableProperty FatsProperty = BindableProperty.Create(
            nameof(Fats),
            typeof(string),
            typeof(NutritionalValueInfo),
            string.Empty);
        public static readonly BindableProperty CarbohydratesProperty = BindableProperty.Create(
            nameof(Carbohydrates),
            typeof(string),
            typeof(NutritionalValueInfo),
            string.Empty);

        public string Kcal
        {
            get => (string)GetValue(KcalProperty);
            set => SetValue(KcalProperty, value);
        }
        public string Proteins
        {
            get => (string)GetValue(ProteinsProperty);
            set => SetValue(ProteinsProperty, value);
        }
        public string Fats
        {
            get => (string)GetValue(FatsProperty);
            set => SetValue(FatsProperty, value);
        }
        public string Carbohydrates
        {
            get => (string)GetValue(CarbohydratesProperty);
            set => SetValue(CarbohydratesProperty, value);
        }

        public NutritionalValueInfo()
        {
            InitializeComponent();
        }
    }
}