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
    public partial class Card : ContentView
    {
        public static readonly BindableProperty PhotoProperty = BindableProperty.Create(
            nameof(Photo),
            typeof(ImageSource),
            typeof(Card),
            null);
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            nameof(Name),
            typeof(string),
            typeof(Card),
            string.Empty);
        public static readonly BindableProperty KcalValueProperty = BindableProperty.Create(
            nameof(KcalValue),
            typeof(string),
            typeof(Card),
            string.Empty);
        public static readonly BindableProperty CookingTimeProperty = BindableProperty.Create(
            nameof(CookingTime),
            typeof(string),
            typeof(Card),
            string.Empty);
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(IRelayCommand),
            typeof(Card),
            null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(Card),
            null);

        public ImageSource Photo
        {
            get => (ImageSource)GetValue(PhotoProperty);
            set => SetValue(PhotoProperty, value);
        }
        public string Name
        {
            get => (string)GetValue(NameProperty);
            set => SetValue(NameProperty, value);
        }
        public string KcalValue
        {
            get => (string)GetValue(KcalValueProperty);
            set => SetValue(KcalValueProperty, value);
        }
        public string CookingTime
        {
            get => (string)GetValue(CookingTimeProperty);
            set => SetValue(CookingTimeProperty, value);
        }
        public IRelayCommand TapCommand
        {
            get => (IRelayCommand)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }
        public object TapCommandParameter
        {
            get => (object)GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        public Card()
        {
            InitializeComponent();
        }
    }
}