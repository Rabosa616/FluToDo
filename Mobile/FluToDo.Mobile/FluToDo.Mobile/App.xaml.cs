using FluToDo.Mobile.Helpers;
using FluToDo.Mobile.Interfaces;
using FluToDo.Mobile.Services;
using FluToDo.Mobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace FluToDo.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitializeServiceLocator();
            MainPage = new NavigationPage(new MainPageView());
        }

        private void InitializeServiceLocator()
        {
            ServiceLocator.Instance.Add<IApiService, ApiService>();
            ServiceLocator.Instance.Add<INavigationService, NavigationService>();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
