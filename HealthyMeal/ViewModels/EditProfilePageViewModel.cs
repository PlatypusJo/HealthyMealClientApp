using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HealthyMeal.Models;
using HealthyMeal.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace HealthyMeal.ViewModels
{
    public partial class EditProfilePageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Поля

        private string _userId;

        private UserModel _user;

        private double _userKcalAmountGoal;

        private int _userAge;

        private double _userHeight;

        private double _userWeight;

        #endregion

        #region ObservableProperties

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private string _userName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private string _userLogin;

        [ObservableProperty]
        private List<SexModel> _sexes;

        [ObservableProperty]
        private List<PhysicalActivityModel> _physicalActivities;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserRdc))]
        [NotifyPropertyChangedFor(nameof(IsVisibleWarning))]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private PhysicalActivityModel _selectedPhysicalActivity;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserRdc))]
        [NotifyPropertyChangedFor(nameof(IsVisibleWarning))]
        [NotifyPropertyChangedFor(nameof(IsEnabledSaveBtn))]
        private SexModel _selectedSex;

        #endregion

        #region Свойства

        public bool IsVisibleWarning => UserRdc < 0;

        public bool IsEnabledSaveBtn => 
            UserAge != 0 && 
            UserHeight !=0 && 
            UserWeight != 0 && 
            UserLogin != string.Empty && 
            UserName != string.Empty && 
            UserKcalAmountGoal != 0 &&
            UserRdc > 0;

        public double UserRdc => MathTools.CalcRdc(UserWeight, UserHeight, UserAge, SelectedSex.Coeff, SelectedPhysicalActivity.FactorActivity);

        public double UserKcalAmountGoal
        {
            get => _userKcalAmountGoal;
            set
            {
                _userKcalAmountGoal = value < 0 ? 0 : Math.Round(value, 1, MidpointRounding.AwayFromZero);
                OnPropertyChanged(nameof(UserKcalAmountGoal));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
            }
        }

        public double UserWeight
        {
            get => _userWeight;
            set
            {
                _userWeight = value < 0 ? 0 : Math.Round(value, 1, MidpointRounding.AwayFromZero);
                OnPropertyChanged(nameof(UserWeight));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
                OnPropertyChanged(nameof(IsVisibleWarning));
                OnPropertyChanged(nameof(UserRdc));
            }
        }

        public double UserHeight
        {
            get => _userHeight;
            set
            {
                _userHeight = value < 0 ? 0 : Math.Round(value, 1, MidpointRounding.AwayFromZero);
                OnPropertyChanged(nameof(UserHeight));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
                OnPropertyChanged(nameof(IsVisibleWarning));
                OnPropertyChanged(nameof(UserRdc));
            }
        }

        public int UserAge
        {
            get => _userAge;
            set
            {
                _userAge = value < 0 ? 0 : value; 
                OnPropertyChanged(nameof(UserAge));
                OnPropertyChanged(nameof(IsEnabledSaveBtn));
                OnPropertyChanged(nameof(IsVisibleWarning));
                OnPropertyChanged(nameof(UserRdc));
            }
        }

        #endregion

        #region Команды

        [RelayCommand]
        private async Task Save()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void AutoSetGoal()
        {
            UserKcalAmountGoal = UserRdc;
        }

        [RelayCommand]
        private void AddPhoto()
        {

        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private void LoginChanged(string text)
        {
            if (text == string.Empty)
                UserLogin = string.Empty;
        }

        [RelayCommand]
        private void NameChanged(string text)
        {
            if (text == string.Empty)
                UserName = string.Empty;
        }

        [RelayCommand]
        private void AgeChanged(string text)
        {
            if (!new Regex(@"^\d+$", RegexOptions.Compiled).IsMatch(text))
                UserAge = text == string.Empty ? 0 : UserAge;
        }

        [RelayCommand]
        private void WeightChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                UserWeight = 0;
        }

        [RelayCommand]
        private void HeightChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                UserHeight = 0;
        }

        [RelayCommand]
        private void KcalAmountGoalChanged(string text)
        {
            if (!new Regex(@"^\d+[,.]?\d*$", RegexOptions.Compiled).IsMatch(text))
                UserKcalAmountGoal = 0;
        }

        #endregion

        #region Конструкторы

        public EditProfilePageViewModel()
        {
            SelectedPhysicalActivity = new();
            SelectedSex = new();
            LoadCollections();
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

        public async void LoadDataAfterNavigation()
        {
            _user = await GlobalDataStore.Users.GetItemAsync(_userId);

            UserName = _user.Name;
            UserLogin = _user.Login;
            UserKcalAmountGoal = _user.KcalAmountGoal;
            UserAge = _user.Age;
            UserWeight = _user.Weight;
            UserHeight = _user.Height;

            SelectedSex = Sexes.Find(s => s.Id == _user.SexId);
            SelectedPhysicalActivity = PhysicalActivities.Find(p => p.Id == _user.PhysicalActivityId);
        }

        #endregion

        #region Внутренние методы

        private async void LoadCollections()
        {
            Sexes = await GlobalDataStore.Sexes.GetAllItemsAsync();
            PhysicalActivities = await GlobalDataStore.PhysicalActivities.GetAllItemsAsync();

            SelectedPhysicalActivity = PhysicalActivities.FirstOrDefault();
            SelectedSex = Sexes.FirstOrDefault();
        }

        #endregion
    }
}
