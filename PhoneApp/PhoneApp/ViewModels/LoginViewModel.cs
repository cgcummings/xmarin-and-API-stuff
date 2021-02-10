using PhoneApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

using PhoneApp.Services;
using Android.Content.Res;

namespace PhoneApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        APIServices _apiServices = new APIServices();

        public string Email { get; set; }

        public string Password { get; set; }


        //public Command LoginCommand { get; }

        //public LoginViewModel()
        //{
        //    LoginCommand = new Command(OnLoginClicked);
        //}

        public ICommand LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _apiServices.LoginAsync(Email, Password);
                    //Shell.PushAsync(new Recipes());
                    if (Models.Globals.Token != null)
                    {
                        App.Current.MainPage = new AppShell();
                    }

                });
            }
        }

        public ICommand GoToRegistration
        {
            get
            {
                return new Command(async () =>
                {
                    await Shell.Current.GoToAsync("//Register");
                    


                });
            }
        }
        //private async void OnLoginClicked(object obj)
        //{
        //    // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
        //    await _apiServices.LoginAsync(Email, Password);

        //    if(Models.Globals.Token != null)
        //    {
               
        //    }
           
        //}
    }
}
