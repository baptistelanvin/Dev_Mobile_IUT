using Dev_Mobile_IUT.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Dev_Mobile_IUT.Views
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