using PhoneApp.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using PhoneApp.Services;

namespace PhoneApp.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        APIServices _apiServices = new APIServices();
        public ItemDetailViewModel()
        {
            SaveCommand = new Command(OnSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
               (_, __) => SaveCommand.ChangeCanExecute();
        }


        private string itemId;
        private string text;
        private string description;
        private string ingredients;
        private string instruction;
        public string Id { get; set; }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
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


        public Command CancelCommand { get; }
        public Command SaveCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await _apiServices.GetRecipesByIDAsync(Int32.Parse(itemId));
            
                Id = item.ID.ToString();
                Text = item.Title;
                Description = item.Description;
                Instruction = item.Instruction;
                Ingredients = item.Ingredients;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }


        private async void OnSave()
        {

            Recipes UpdateRecipes = new Recipes()
            {
                Title = Text,
                Description = Description,
                Instruction = Instruction,
                Ingredients = ingredients


            };

            //api service Update
            await _apiServices.UpdateRecipeAsync(Id, UpdateRecipes);



            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
