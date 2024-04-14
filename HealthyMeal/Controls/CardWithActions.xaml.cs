using Newtonsoft.Json.Serialization;
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
    public partial class CardWithActions : ContentView
    {
        public static readonly BindableProperty PhotoProperty = BindableProperty.Create(
            nameof(Photo),
            typeof(ImageSource),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty NameProperty = BindableProperty.Create(
            nameof(Name),
            typeof(string),
            typeof(CardWithActions),
            string.Empty);
        public static readonly BindableProperty IconLeftActionProperty = BindableProperty.Create(
            nameof(IconLeftAction),
            typeof(ImageSource),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty ActionLeftCommandProperty = BindableProperty.Create(
            nameof(ActionLeftCommand),
            typeof(Command),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty ActionLeftCommandParameterProperty = BindableProperty.Create(
            nameof(ActionLeftCommandParameter),
            typeof(object),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty IconRightActionProperty = BindableProperty.Create(
            nameof(IconRightAction),
            typeof(ImageSource),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty ActionRightCommandProperty = BindableProperty.Create(
            nameof(ActionRightCommand),
            typeof(Command),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty ActionRightCommandParameterProperty = BindableProperty.Create(
            nameof(ActionRightCommandParameter),
            typeof(object),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
            nameof(TapCommand),
            typeof(Command),
            typeof(CardWithActions),
            null);
        public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
            nameof(TapCommandParameter),
            typeof(object),
            typeof(CardWithActions),
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
        public ImageSource IconLeftAction
        {
            get => (ImageSource)GetValue(IconLeftActionProperty);
            set => SetValue(IconLeftActionProperty, value);
        }
        public Command ActionLeftCommand
        {
            get => (Command)GetValue(ActionLeftCommandProperty);
            set => SetValue(ActionLeftCommandProperty, value);
        }
        public object ActionLeftCommandParameter
        {
            get => (object)GetValue(ActionLeftCommandParameterProperty);
            set => SetValue(ActionLeftCommandParameterProperty, value);
        }
        public ImageSource IconRightAction
        {
            get => (ImageSource)GetValue(IconRightActionProperty);
            set => SetValue(IconRightActionProperty, value);
        }
        public Command ActionRightCommand
        {
            get => (Command)GetValue(ActionRightCommandProperty);
            set => SetValue(ActionRightCommandProperty, value);
        }
        public object ActionRightCommandParameter
        {
            get => (object)GetValue(ActionRightCommandParameterProperty);
            set => SetValue(ActionRightCommandParameterProperty, value);
        }
        public Command TapCommand
        {
            get => (Command)GetValue(TapCommandProperty);
            set => SetValue(TapCommandProperty, value);
        }
        public object TapCommandParameter
        {
            get => (object)GetValue(TapCommandParameterProperty);
            set => SetValue(TapCommandParameterProperty, value);
        }

        public CardWithActions()
        {
            InitializeComponent();
        }
    }
}