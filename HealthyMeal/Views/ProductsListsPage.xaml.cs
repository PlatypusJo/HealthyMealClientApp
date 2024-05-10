using HealthyMeal.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HealthyMeal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsListsPage : ContentPage
    {
        private static readonly ProductsListsPageViewModel _vm = new();

        public ProductsListsPage()
        {
            InitializeComponent();
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.LoadDataAfterNavigation();
        }

        public static void ReloadData()
        {
            _vm.LoadDataAfterNavigation();
        }
    }
}