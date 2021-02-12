using PhoneApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using PhoneApp.Services;

namespace PhoneApp.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {

        APIServices _apiServices = new APIServices();

        private string title;
        private string description;
        private string ingredients;
        private string instruction;

        public NewItemViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(title)
                && !String.IsNullOrWhiteSpace(description);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Ingredients
        {
            get => ingredients;
            set => SetProperty(ref ingredients, value);
        }

        public string Instruction
        {
            get => instruction;
            set => SetProperty(ref instruction, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
 
            Recipes newRecipe = new Recipes()
            {
                Title = Title,
                Description = Description,
                Instruction = Instruction,
                Ingredients = ingredients


            };

            //api service add
            await _apiServices.AddRecipeAsync(newRecipe);

           

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
