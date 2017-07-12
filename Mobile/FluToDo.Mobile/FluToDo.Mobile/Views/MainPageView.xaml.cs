using FluToDo.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FluToDo.Mobile.Views
{
    public partial class MainPageView : ContentPage
    {
        public MainPageView()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        void OnDelete(object sender, EventArgs e)
        {
            var item = (MenuItem)sender;
            ((MainPageViewModel)BindingContext).DeleteTodoItemCommand.Execute(item.CommandParameter);
        }
    }
}
