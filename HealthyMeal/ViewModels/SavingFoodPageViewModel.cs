using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public class SavingFoodPageViewModel : BaseViewModel
    {
        #region Команды

        public ICommand GoBackCommand { get; private set; }

        #endregion

        #region Коструктор

        public SavingFoodPageViewModel()
        {
            GoBackCommand = new Command(GoBack);
        }

        #endregion

        #region Методы

        public async void GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        #endregion
    }
}
