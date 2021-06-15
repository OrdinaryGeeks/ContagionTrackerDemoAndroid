using CovidTrackerForms.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace CovidTrackerForms.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}