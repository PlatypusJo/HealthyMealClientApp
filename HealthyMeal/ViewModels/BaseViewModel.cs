using CommunityToolkit.Mvvm.ComponentModel;
using HealthyMeal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected static IGlobalDataStore GlobalDataStore => DependencyService.Get<IGlobalDataStore>();
    }
}
