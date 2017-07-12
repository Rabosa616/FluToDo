using FluToDo.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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