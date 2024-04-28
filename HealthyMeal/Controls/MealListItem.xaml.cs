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
    public partial class MealListItem : ContentView
    {
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
           nameof(Name),
           typeof(string),
           typeof(MealListItem),
           string.Empty);

        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            nameof(Amount),
            typeof(string),
            typeof(MealListItem),
            string.Empty);
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
           nameof(TapCommand),
           typeof(IRelayCommand),
           typeof(MealListItem),
           null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(MealListItem),
            null);
        public static readonly BindableProperty ButtonCommandProperty = BindableProperty.Create(
            nameof(ButtonCommand),
            typeof(IRelayCommand),
            typeof(MealListItem),
            null);
        public static readonly BindableProperty ButtonCommandParameterProperty = BindableProperty.Create(
            nameof(ButtonCommandParameter),
            typeof(object),
            typeof(MealListItem),
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
        public IRelayCommand ButtonCommand
        {
            get => (IRelayCommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }
        public object ButtonCommandParameter
        {
            get => (object)GetValue(ButtonCommandParameterProperty);
            set => SetValue(ButtonCommandParameterProperty, value);
        }


        public MealListItem()
        {
            InitializeComponent();
        }
    }
}