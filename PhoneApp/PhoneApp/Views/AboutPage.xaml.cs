using FFImageLoading.Forms;
using Plugin.Share;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PhoneApp.Views
{
    public partial class AboutPage : ContentPage
    {
  

        public AboutPage()
        {


            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
            {
                Url = "play.google.com"
            }); 
        }
    }
}