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
        private ICommand _addNewTodoItemCommand;
        private Command _editTodoItemCommand;
        private Command _deleteTodoItemCommand;
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
        public ICommand AddNewTodoItemCommand { get { return _addNewTodoItemCommand = _addNewTodoItemCommand ?? new Command(onAddNewTodoItemCommand); } }
        public ICommand EditTodoItemCommand { get { return _editTodoItemCommand = _editTodoItemCommand ?? new Command(async () => await onEditTodoItemCommand()); } }
        public ICommand DeleteTodoItemCommand { get { return _deleteTodoItemCommand = _deleteTodoItemCommand ?? new Command(async (object obj) => await onDeleteTodoItemCommand(obj)); } }
        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            Title = "Todo List";
            _service = ServiceLocator.Instance.Resolve<IApiService>();
            _navigationService = ServiceLocator.Instance.Resolve<INavigationService>();
            // Subscribe to events from the Task Detail Page
            MessagingCenter.Subscribe<ToDoViewModel>(this, "ItemChanged", async (sender) =>
            {
                await onFetchFieldsCommand();
            });
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

        private void onAddNewTodoItemCommand(object obj)
        {
            _navigationService.NavigateTo<ToDoViewModel>();
        }

        async Task onEditTodoItemCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (_selectedItem == null) return;
                _selectedItem.IsComplete = !_selectedItem.IsComplete;
                await _service.UpdateItem(_selectedItem);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Item Not Updated", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                FetchItemsCommand.Execute(null);
            }
        }

        async Task onDeleteTodoItemCommand(object obj)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                if (obj == null) return;
                ToDoItem itemToDelete = (ToDoItem)obj;
                await _service.DeleteItem(itemToDelete.Key);
                await Application.Current.MainPage.DisplayAlert("Delete OK",$"ToDo item {itemToDelete.Name} has been deleted correctly", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Item Not Deleted", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                FetchItemsCommand.Execute(null);
            }
        }
        #endregion
    }
}
