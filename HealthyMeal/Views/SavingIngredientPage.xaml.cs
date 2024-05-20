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
    public partial class SavingIngredientPage : ContentPage
    {
        private readonly SavingIngredientPageViewModel _vm;
        public SavingIngredientPage()
        {
            InitializeComponent();
            BindingContext = _vm = new();
        }
    }
}