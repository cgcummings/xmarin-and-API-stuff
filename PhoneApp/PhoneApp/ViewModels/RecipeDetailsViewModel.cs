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
    class RecipeDetailsViewModel : INotifyPropertyChanged
    {
      
          
            public List<Recipes> recipes;

      

            public List<Recipes> Recipes
            {
                get { return recipes; }
                set
                {
                    recipes = value;
                    OnPropertyChanged();
                }
            }

        

            public event PropertyChangedEventHandler PropertyChanged;

       

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        
    }
}
