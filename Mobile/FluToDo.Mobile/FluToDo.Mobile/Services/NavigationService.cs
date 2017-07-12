using FluToDo.Mobile.Interfaces;
using FluToDo.Mobile.ViewModels;
using FluToDo.Mobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FluToDo.Mobile.Services
{
    public class NavigationService : INavigationService
    {
        private static NavigationService _instance;
        private Dictionary<Type, Type> _viewModelRouting = new Dictionary<Type, Type>()
        {
            { typeof(MainPageViewModel), typeof(MainPageView)},
        };

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NavigationService();
                return _instance;
            }
        }

        public void NavigateTo<TDestinationViewModel>(object navigationContext = null)
        {
            Type pageType = _viewModelRouting[typeof(TDestinationViewModel)];

            Page page;
            if (navigationContext == null)
                page = Activator.CreateInstance(pageType) as Page;
            else
                page = Activator.CreateInstance(pageType, new[] { navigationContext }) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateTo(Type destinationType, object navigationContext = null)
        {
            Type pageType = _viewModelRouting[destinationType];
            var page = Activator.CreateInstance(pageType, new[] { navigationContext }) as Page;

            if (page != null)
                Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public void NavigateBack()
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
