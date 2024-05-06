using CommunityToolkit.Mvvm.ComponentModel;
using HealthyMeal.Services.BLL;
using HealthyMeal.Services.Interfaces;
using HealthyMeal.Services.MockDataStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected static IGlobalDataStore GlobalDataStore = new GlobalDataStore();
        protected static BlService BlService = new(GlobalDataStore);
    }
}
