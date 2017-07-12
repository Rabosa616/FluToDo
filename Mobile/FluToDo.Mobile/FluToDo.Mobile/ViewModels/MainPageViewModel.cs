﻿using FluToDo.Mobile.Helpers;
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
        #endregion

        #region Constructor
        public MainPageViewModel()
        {
            Title = "Todo List";
            InitializeServices();

            // Subscribe to events from the Task Detail Page
            MessagingCenter.Subscribe<ToDoViewModel>(this, "ItemChanged", async (sender) =>
            {
                await onFetchFieldsCommand();
            });

            FetchItemsCommand.Execute(null);
        }

        private void InitializeServices()
        {
            _service = ServiceLocator.Instance.Resolve<IApiService>();
            _navigationService = ServiceLocator.Instance.Resolve<INavigationService>();
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
        #endregion
    }
}
