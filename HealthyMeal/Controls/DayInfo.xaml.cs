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
    public partial class DayInfo : ContentView
    {
        public static readonly BindableProperty DayNumberProperty = BindableProperty.Create(
            nameof(DayNumber),
            typeof(string),
            typeof(DayInfo),
            string.Empty);
        public static readonly BindableProperty DayOfWeekProperty = BindableProperty.Create(
            nameof(DayOfWeek),
            typeof(string),
            typeof(DayInfo),
            string.Empty);
        public static readonly BindableProperty KcalAmountProperty = BindableProperty.Create(
            nameof(KcalAmount),
            typeof(string),
            typeof(DayInfo),
            string.Empty);
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(IRelayCommand),
            typeof(DayInfo),
            null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(DayInfo),
            null);

        public string DayNumber
        {
            get => (string)GetValue(DayNumberProperty);
            set => SetValue(DayNumberProperty, value);
        }
        public string DayOfWeek
        {
            get => (string)GetValue(DayOfWeekProperty);
            set => SetValue(DayOfWeekProperty, value);
        }
        public string KcalAmount
        {
            get => (string)GetValue(KcalAmountProperty);
            set => SetValue(KcalAmountProperty, value);
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

        public DayInfo()
        {
            InitializeComponent();
        }
    }
}