using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Recipes : ContentPage
    {
        APIServices _apiServices = new APIServices();
        public ViewModels.RecipesViewModel RecipesViewModel { get; set; }
        public  Recipes()
        {
           
            InitializeComponent();
         

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var Recipelist = new List<Models.Recipes>
            {

            };

            Recipelist = await _apiServices.GetRecipesAsync();
            RecipeListView.ItemsSource = Recipelist;


          
        }
    }
}