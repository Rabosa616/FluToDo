using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluToDo.Mobile.Models;
using System.Windows.Input;
using Xamarin.Forms;
using FluToDo.Mobile.Helpers;
using FluToDo.Mobile.Interfaces;

namespace FluToDo.Mobile.ViewModels
{
    public class ToDoViewModel : BaseViewModel
    {
        private ICommand _createCommand;
        private string _name;
        IApiService _service;
        private INavigationService _navigation;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public ICommand CreateCommand { get { return _createCommand ?? (_createCommand = new Command(async () => await onCreateCommand())); } }

        async Task onCreateCommand()
        {
            ToDoItem todoItem = new ToDoItem
            {
                Name = _name
            };
            await _service.UpdateItem(todoItem);
            MessagingCenter.Send<ToDoViewModel>(this, "ItemChanged");
            _navigation.NavigateBack();

        }

        public ToDoViewModel()
        {
            _service = ServiceLocator.Instance.Resolve<IApiService>();
            _navigation = ServiceLocator.Instance.Resolve<INavigationService>();
        }
    }
}
