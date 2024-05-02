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
    public partial class ProductsPage : ContentPage
    {
        private static readonly ProductsPageViewModel _vm = new();
        public ProductsPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}