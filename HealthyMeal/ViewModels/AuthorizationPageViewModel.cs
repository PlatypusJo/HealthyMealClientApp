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



        #endregion

        #region Свойства




        #endregion

        #region Команды

        [RelayCommand]
        private async Task OpenDiary()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
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
