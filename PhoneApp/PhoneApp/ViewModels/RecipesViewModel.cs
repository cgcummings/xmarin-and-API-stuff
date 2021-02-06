using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using PhoneApp.Models;
using Xamarin.Forms;
using PhoneApp.Services;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PropertyChangingEventArgs = System.ComponentModel.PropertyChangingEventArgs;

namespace PhoneApp.ViewModels
{
    public class RecipesViewModel : INotifyPropertyChanged
    {
        APIServices _apiServices = new APIServices();
        public List<Recipes> recipes;

        public string AccessToken { get; set; }

        public List<Recipes> Recipes { 
         get { return recipes; }
            set
            {
                recipes = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetRecipesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Recipes = await _apiServices.GetRecipesAsync(AccessToken);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //public event PropertyChangingEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
