using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HealthyMeal.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        #region Поля

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

        }

        #endregion

        #region Конструкторы

        public ProfilePageViewModel() 
        { 
            _user = new()
            {
                Id = "1",
                Name = "Иван",
                Login = "LoVan",
                Rdc = 2500,
                KcalAmountGoal = 2000,
                Age = 25,
                Height = 176,
                Weight = 73,
                PhysicalActivityId = "2",
                SexId = "1",
            };
        }

        #endregion

        #region Методы

        public async void LoadDataAfterNavigation()
        {
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
