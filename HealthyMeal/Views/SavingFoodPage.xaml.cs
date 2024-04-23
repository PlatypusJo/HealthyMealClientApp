using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthyMeal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyMeal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SavingFoodPage : ContentPage
    {
        private static SavingFoodPageViewModel _vm = new();
        public SavingFoodPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}