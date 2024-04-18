using HealthyMeal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyMeal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodPage : ContentPage
    {
        private FoodPageViewModel _vm;
        public FoodPage()
        {
            InitializeComponent();
            BindingContext = _vm = new FoodPageViewModel();
        }
    }
}