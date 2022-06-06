using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TurismoApp.Views;

namespace TurismoApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Home());
            
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
