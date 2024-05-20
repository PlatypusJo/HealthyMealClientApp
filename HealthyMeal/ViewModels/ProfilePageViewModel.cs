using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        #region Поля

        private string _userId;

        private UserModel _user;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private string _userName;

        [ObservableProperty]
        private string _userLogin;

        [ObservableProperty]
        private double _userKcalAmountGoal;

        [ObservableProperty]
        private int _userAge;

        [ObservableProperty]
        private double _userHeight;

        [ObservableProperty]
        private double _userWeight;

        [ObservableProperty]
        private double _userRdc;

        #endregion

        #region Свойства



        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenEditProfilePage()
        {
            string userId = NavigationParameterConverter.ObjectToPairKeyValue(_userId, "UserId");
            await Shell.Current.GoToAsync($"{nameof(EditProfilePage)}?{userId}");
        }

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

        #endregion

        #region Конструкторы

        public ProfilePageViewModel() 
        {
            _userId = "1";
        }

        #endregion

        #region Методы

        public async void LoadDataAfterNavigation()
        {
            _user = await GlobalDataStore.Users.GetItemAsync(_userId);
            UserName = _user.Name;
            UserLogin = _user.Login;
            UserKcalAmountGoal = _user.KcalAmountGoal;
            UserRdc = _user.Rdc;
            UserAge = _user.Age;
            UserWeight = _user.Weight;
            UserHeight = _user.Height;
        }

        #endregion

        #region Внутренние методы



        #endregion
    }
}
