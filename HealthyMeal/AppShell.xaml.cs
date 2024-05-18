using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using HealthyMeal.Views;

namespace HealthyMeal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FoodPage), typeof(FoodPage));
            Routing.RegisterRoute(nameof(SavingFoodPage), typeof(SavingFoodPage));
            Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
            Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Routing.RegisterRoute(nameof(RecipeInfoPage), typeof(RecipeInfoPage));
            Routing.RegisterRoute(nameof(SavingRecipePage), typeof(SavingRecipePage));
            Routing.RegisterRoute(nameof(MealsPage), typeof(MealsPage));
            Routing.RegisterRoute(nameof(ProductsPage), typeof(ProductsPage));
            Routing.RegisterRoute(nameof(SavingToShopListPage), typeof(SavingToShopListPage));
            Routing.RegisterRoute(nameof(MenuRecipesPage), typeof(MenuRecipesPage));
            Routing.RegisterRoute(nameof(UserProductsPage), typeof(UserProductsPage));
        }
    }
}