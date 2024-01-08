using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static System.Net.Mime.MediaTypeNames;

namespace HealthyMeal.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ElementWithFlagAndButton : ContentView
    {
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            nameof(Name),
            typeof(string),
            typeof(ElementWithFlagAndButton),
            string.Empty);
        public static readonly BindableProperty AmountProperty = BindableProperty.Create(
            nameof(Amount),
            typeof(string),
            typeof(ElementWithFlagAndButton),
            string.Empty);
        public static readonly BindableProperty UnitsNameProperty = BindableProperty.Create(
            nameof(UnitsName),
            typeof(string),
            typeof(ElementWithFlagAndButton),
            string.Empty);
        public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
            nameof(IsChecked),
            typeof(bool),
            typeof(ElementWithFlagAndButton),
            false);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(Command),
            typeof(ElementWithFlagAndButton),
            null);
        public static readonly BindableProperty CheckBoxCommandProperty = BindableProperty.Create(
            nameof(CheckBoxCommand),
            typeof(Command),
            typeof(ElementWithFlagAndButton),
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
        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }
        public Command Command
        {
            get
            {
                return (Command)GetValue(CommandProperty);
            }
            set => SetValue(CommandProperty, value);
        }
        public Command CheckBoxCommand
        {
            get
            {
                return (Command)GetValue(CheckBoxCommandProperty);
            }
            set => SetValue(CheckBoxCommandProperty, value);
        }

        public ElementWithFlagAndButton()
        {
            InitializeComponent();
        }
    }
}