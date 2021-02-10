using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PhoneApp.Services;

namespace PhoneApp.ViewModels
{
    public class RegisterViewModel
    {
        APIServices _apiServices = new APIServices();
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public string Message { get; set; }

        public ICommand RegisterCommand
        {
            get
            {
                return new Command(async() =>
                {
                  var isSuccess = await  _apiServices.RegisterAsync(
                      Email, Password, ConfirmPassword);
                    if (isSuccess)
                    {
                        await _apiServices.LoginAsync(Email, Password);
                        App.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        Message = "Registration failed";
                    }
                });
            }
        }
           
    }
}
