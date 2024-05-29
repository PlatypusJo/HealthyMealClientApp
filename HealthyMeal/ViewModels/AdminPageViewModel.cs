using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class AdminPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId;

        #endregion

        #region ObservableProperties



        #endregion

        #region Свойства




        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenUserProductsPage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string isFromProfile = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromProfile");
            await Shell.Current.GoToAsync($"{nameof(UserProductsPage)}?{userId}&{isFromProfile}");
        }

        [RelayCommand]
        private async Task OpenUserRecipesPage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            string isFromProfile = NavigationParameterConverter.ObjectToPairKeyValue(true, "IsFromProfile");
            await Shell.Current.GoToAsync($"{nameof(UserRecipesPage)}?{userId}&{isFromProfile}");
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        #endregion

        #region Конструкторы

        public AdminPageViewModel() 
        {
            _userId = "1";
        }

        #endregion

        #region Методы

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.ContainsKey("UserId"))
            {
                string userId = HttpUtility.UrlDecode(query["UserId"]);
                _userId = NavigationParameterConverter.ObjectFromPairKeyValue<string>(userId);
            }
        }

        #endregion

        #region Внутренние методы



        #endregion
    }
}
