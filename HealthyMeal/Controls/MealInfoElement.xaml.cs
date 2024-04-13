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
    public partial class MealInfoElement : ContentView
    {
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
            nameof(Icon),
            typeof(ImageSource),
            typeof(MealInfoElement),
            null); 
        public static readonly BindableProperty MealTypeNameProperty = BindableProperty.Create(
            nameof(MealTypeName),
            typeof(string),
            typeof(MealInfoElement),
            string.Empty);
        public static readonly BindableProperty KcalValueProperty = BindableProperty.Create(
            nameof(KcalValue),
            typeof(string),
            typeof(MealInfoElement),
            string.Empty);
        public static readonly BindableProperty FoodAmountProperty = BindableProperty.Create(
            nameof(FoodAmount),
            typeof(string),
            typeof(MealInfoElement),
            string.Empty);
        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
            nameof(ButtonCommand),
            typeof(Command),
            typeof(MealInfoElement),
            null);
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(Command),
            typeof(MealInfoElement),
            null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(MealInfoElement),
            null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(ButtonCommandParameter),
            typeof(object),
            typeof(MealInfoElement),
            null);

        public ImageSource Icon
        {
            get => (ImageSource)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }
        public string MealTypeName
        {
            get => (string)GetValue(MealTypeNameProperty);
            set => SetValue(MealTypeNameProperty, value);
        }
        public string KcalValue
        {
            get => (string)GetValue(KcalValueProperty);
            set => SetValue(KcalValueProperty, value);
        }
        public string FoodAmount
        {
            get => (string)GetValue(FoodAmountProperty);
            set => SetValue(FoodAmountProperty, value);
        }
        public Command ButtonCommand
        {
            get
            {
                return (Command)GetValue(ButtonCommandProperty);
            }
            set => SetValue(ButtonCommandProperty, value);
        }
        public object ButtonCommandParameter
        {
            get => (object)GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        public Command TapCommand
        {
            get
            {
                return (Command)GetValue(TapCommandProperty);
            }
            set => SetValue(TapCommandProperty, value);
        }
        public object TapCommandParameter
        {
            get => (object)GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        public MealInfoElement()
        {
            InitializeComponent();
        }
    }
}