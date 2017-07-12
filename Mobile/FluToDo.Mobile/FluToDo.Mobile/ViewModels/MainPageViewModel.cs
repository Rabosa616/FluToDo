using FluToDo.Mobile.Helpers;
using FluToDo.Mobile.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using FluToDo.Mobile.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Collections.Generic;

namespace FluToDo.Mobile.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Fields
        private ToDoItem _selectedItem;
        IApiService _service;
        INavigationService _navigationService;
        ObservableCollection<ToDoItem> _items = new ObservableCollection<ToDoItem>();
        private ICommand _fetchItemsCommand;
        #endregion

        #region Properties
        public ToDoItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public ObservableCollection<ToDoItem> Items
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }
        public ICommand FetchItemsCommand { get { return _fetchItemsCommand ?? (_fetchItemsCommand = new Command(async () => await onFetchFieldsCommand())); } }        
        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            Title = "Todo List";
            _service = ServiceLocator.Instance.Resolve<IApiService>();
            _navigationService = ServiceLocator.Instance.Resolve<INavigationService>();
            FetchItemsCommand.Execute(null);
        }
        #endregion

        #region Private Methods
        async Task onFetchFieldsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var items = await _service.GetItems();
                if (items != null)
                {
                    Items = new ObservableCollection<ToDoItem>(items);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Items Not Loaded", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
