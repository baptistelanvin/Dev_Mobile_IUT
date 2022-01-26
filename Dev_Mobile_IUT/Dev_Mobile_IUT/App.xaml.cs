using Dev_Mobile_IUT.Services;
using Dev_Mobile_IUT.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Dev_Mobile_IUT
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
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
