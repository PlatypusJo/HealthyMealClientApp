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
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(Command),
            typeof(ElementWithFlagAndButton),
            null);

        public string Name
        {
            get => (string)GetValue(ElementWithFlagAndButton.NameProperty);
            set => SetValue(ElementWithFlagAndButton.NameProperty, value);
        }
        public string Amount
        {
            get => (string)GetValue(ElementWithFlagAndButton.AmountProperty);
            set => SetValue(ElementWithFlagAndButton.AmountProperty, value);
        }
        public string UnitsName
        {
            get => (string)GetValue(ElementWithFlagAndButton.UnitsNameProperty);
            set => SetValue(ElementWithFlagAndButton.UnitsNameProperty, value);
        }
        public Command Command
        {
            get
            {
                return (Command)GetValue(ElementWithFlagAndButton.CommandProperty);
            }
            set => SetValue(ElementWithFlagAndButton.CommandProperty, value);
        }

        public ElementWithFlagAndButton()
        {
            InitializeComponent();
        }
    }
}