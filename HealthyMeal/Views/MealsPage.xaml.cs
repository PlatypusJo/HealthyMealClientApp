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
    public partial class MealsPage : ContentPage
    {
        private static readonly MealsPageViewModel _vm = new();
        public MealsPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}