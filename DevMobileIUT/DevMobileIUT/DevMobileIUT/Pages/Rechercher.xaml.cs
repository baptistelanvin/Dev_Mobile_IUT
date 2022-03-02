using System;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevMobileIUT.ViewModels;

namespace DevMobileIUT.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Rechercher : ContentPage
    {
        public Rechercher()
        {
            InitializeComponent();
            BindingContext = SpotifyViewModel.Instance;
        }
    }
}