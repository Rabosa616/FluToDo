using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FluToDo.Mobile.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields
        string _propTitle = string.Empty;
        bool _propIsBusy;
        #endregion

        #region Properties
        public string Title
        {
            get { return _propTitle; }
            set { SetProperty(ref _propTitle, value, "Title"); }
        }

        public bool IsBusy
        {
            get { return _propIsBusy; }
            set { SetProperty(ref _propIsBusy, value, "IsBusy"); }
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Methods
        public void OnPropertyChanged(string propName)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Protected Methods
        protected void SetProperty<T>(ref T store, T value, string propName, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(store, value))
                return;
            store = value;
            if (onChanged != null)
                onChanged();
            OnPropertyChanged(propName);
        } 
        #endregion
    }
}
