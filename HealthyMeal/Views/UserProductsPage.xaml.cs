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
    public partial class UserProductsPage : ContentPage
    {
        private static readonly UserProductsPageViewModel _vm = new();
        public UserProductsPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }
    }
}