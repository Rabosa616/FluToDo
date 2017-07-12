using System;

namespace FluToDo.Mobile.Interfaces
{
    public interface INavigationService
    {
        void NavigateBack();
        void NavigateTo(Type destinationType, object navigationContext = null);
        void NavigateTo<TDestinationViewModel>(object navigationContext = null);
    }
}
