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
	public partial class StatisticsLine : ContentView
	{
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            nameof(Name),
            typeof(string),
            typeof(StatisticsLine),
            string.Empty);
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value),
            typeof(string),
            typeof(StatisticsLine),
            string.Empty);
        public static readonly BindableProperty UnitsNameProperty = BindableProperty.Create(
            nameof(UnitsName),
            typeof(string),
            typeof(StatisticsLine),
            string.Empty);

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        public string UnitsName
        {
            get => (string)GetValue(UnitsNameProperty);
            set => SetValue(UnitsNameProperty, value);
        }
        public StatisticsLine()
		{
			InitializeComponent();
		}
	}
}