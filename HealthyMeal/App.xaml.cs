using HealthyMeal.Utils;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyMeal
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MealTypesProvider.RegisterAll();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
