using CommunityToolkit.Mvvm.Input;
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
    public partial class FoodListItem : ContentView
    {
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
           nameof(Name),
           typeof(string),
           typeof(FoodListItem),
           string.Empty);

        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            nameof(Amount),
            typeof(string),
            typeof(FoodListItem),
            string.Empty);
        public static readonly BindableProperty UnitsNameProperty = BindableProperty.Create(
            nameof(UnitsName),
            typeof(string),
            typeof(FoodListItem),
            string.Empty);
        public static readonly BindableProperty KcalValueProperty = BindableProperty.Create(
            nameof(KcalValue),
            typeof(string),
            typeof(FoodListItem),
            string.Empty);
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(IRelayCommand),
            typeof(FoodListItem),
            null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(FoodListItem),
            null);

        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public string Amount
        {
            get => (string)GetValue(AmountProperty);
            set => SetValue(AmountProperty, value);
        }
        public string UnitsName
        {
            get => (string)GetValue(UnitsNameProperty);
            set => SetValue(UnitsNameProperty, value);
        }
        public string KcalValue
        {
            get => (string)GetValue(KcalValueProperty);
            set => SetValue(KcalValueProperty, value);
        }
        public IRelayCommand TapCommand
        {
            get
            {
                return (IRelayCommand)GetValue(TapCommandProperty);
            }
            set => SetValue(TapCommandProperty, value);
        }
        public object TapCommandParameter
        {
            get => (object)GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        public FoodListItem()
        {
            InitializeComponent();
        }
    }
}