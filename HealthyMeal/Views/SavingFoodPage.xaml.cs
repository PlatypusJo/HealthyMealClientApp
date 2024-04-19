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
        SavingFoodPageViewModel _vm;
        public SavingFoodPage()
        {
            InitializeComponent();
            BindingContext = _vm = new SavingFoodPageViewModel();
        }
    }
}