using System.ComponentModel;
using System.Windows.Input;
using MmaFIghter.MVVM.Models;

namespace MmaFIghter.MVVM.ViewModels
{
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        private UserModel _user;

        public LoginPageViewModel()
        {
            _user = new UserModel();
        }

        public string Username
        {
            get { return _user.Username; }
            set
            {
                if (_user.Username != value)
                {
                    _user.Username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }

        public string Password
        {
            get { return _user.Password; }
            set
            {
                if (_user.Password != value)
                {
                    _user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public ICommand SignupCommand { get; set; }
        public ICommand BackToLoginCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
