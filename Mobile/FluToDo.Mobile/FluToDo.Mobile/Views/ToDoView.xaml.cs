using FluToDo.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FluToDo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoView : ContentPage
    {
        public ToDoView()
        {
            InitializeComponent();
            BindingContext = new ToDoViewModel();
        }
    }
}