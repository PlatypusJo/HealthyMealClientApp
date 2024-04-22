using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class SavingFoodPageViewModel : BaseViewModel
    {
        #region Команды

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        #endregion

        #region Коструктор

        public SavingFoodPageViewModel()
        {
            
        }

        #endregion

        #region Методы

        

        #endregion
    }
}
