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
	public partial class DafaultLine : ContentView
	{
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
           nameof(Name),
           typeof(string),
           typeof(DafaultLine),
           string.Empty);
        public static readonly BindableProperty ValueProperty = BindableProperty.Create(
            nameof(Value),
            typeof(string),
            typeof(DafaultLine),
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
        public DafaultLine()
        {
            InitializeComponent();
        }
    }
}