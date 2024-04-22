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
	public partial class PageNavigator : ContentView
	{
        public static readonly BindableProperty PageNumberProperty = BindableProperty.Create(
            nameof(PageNumber),
            typeof(string),
            typeof(PageNavigator),
            string.Empty);
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(PageNavigator),
            null);
        public static readonly BindableProperty IsVisibleToNextProperty = BindableProperty.Create(
            nameof(IsVisibleToNext),
            typeof(bool),
            typeof(PageNavigator),
            false);
        public static readonly BindableProperty IsVisibleToPreviousProperty = BindableProperty.Create(
            nameof(IsVisibleToPrevious),
            typeof(bool),
            typeof(PageNavigator),
            false);
        public static readonly BindableProperty NextPageCommandProperty = BindableProperty.Create(
            nameof(NextPageCommand),
            typeof(IRelayCommand),
            typeof(PageNavigator),
            null);
        public static readonly BindableProperty BackPageCommandProperty = BindableProperty.Create(
            nameof(BackPageCommand),
            typeof(IRelayCommand),
            typeof(PageNavigator),
            null);

        public string PageNumber
        {
            get => (string)GetValue(PageNumberProperty);
            set => SetValue(PageNumberProperty, value);
        }
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public bool IsVisibleToNext
        {
            get => (bool)GetValue(IsVisibleToNextProperty);
            set => SetValue(IsVisibleToNextProperty, value);
        }
        public bool IsVisibleToPrevious
        {
            get => (bool)GetValue(IsVisibleToPreviousProperty);
            set => SetValue(IsVisibleToPreviousProperty, value);
        }
        public IRelayCommand NextPageCommand
        {
            get
            {
                return (IRelayCommand)GetValue(NextPageCommandProperty);
            }
            set => SetValue(NextPageCommandProperty, value);
        }
        public IRelayCommand BackPageCommand
        {
            get
            {
                return (IRelayCommand)GetValue(BackPageCommandProperty);
            }
            set => SetValue(BackPageCommandProperty, value);
        }
        public PageNavigator ()
		{
			InitializeComponent ();
		}
	}
}