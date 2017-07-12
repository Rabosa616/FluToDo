namespace FluToDo.Mobile.Models
{
    public class ToDoItem : FluToDoBaseModel
    {
        #region Fields
        private string _key;
        private string _name;
        private bool _isComplete;
        #endregion

        #region Properties
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
                OnPropertyChanged("Key");
            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
            set
            {
                _isComplete = value;
                OnPropertyChanged("IsComplete");
            }
        }
        #endregion
    }
}
