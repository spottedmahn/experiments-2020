using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using StyleInheritance.Services;
using StyleInheritance.Views;

namespace StyleInheritance
{
    public partial class App : Application
    {

        public App()
        {
            Device.SetFlags(new string[] { "AppTheme_Experimental" });

            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
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
