
using System.Collections.Generic;
using PhoneApp.Models;
using PhoneApp.Services;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using PropertyChangingEventArgs = System.ComponentModel.PropertyChangingEventArgs;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhoneApp.ViewModels
{
    public class RecipesViewModel : INotifyPropertyChanged
    {
        APIServices _apiServices = new APIServices();
        public List<Recipes> recipes;

        //public string AccessToken { get; set; }
        public string UserName { get; set; }
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


                    Recipes = await _apiServices.GetRecipesAsync();
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
