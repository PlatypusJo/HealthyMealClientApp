using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class AuthorizationPageViewModel : BaseViewModel
    {
        #region Поля



        #endregion

        #region ObservableProperties

        [ObservableProperty]
        private string _login = string.Empty;

        [ObservableProperty]
        private string _password = string.Empty;

        #endregion

        #region Свойства




        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenDiary()
        {
            if (Login == "admin")
            {
                await Shell.Current.GoToAsync($"{nameof(AdminPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }            
        }

        #endregion

        #region Конструкторы

        public AuthorizationPageViewModel() 
        { 
            
        }

        #endregion

        #region Методы



        #endregion

        #region Внутренние методы



        #endregion
    }
}
