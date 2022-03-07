using System;
using SpotifyAPI.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevMobileIUT.ViewModels;
using DevMobileIUT.Models;

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

        void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Musique current = (e.CurrentSelection.FirstOrDefault() as Musique);
            if (current == null)
            {
                return;
            }
            
        }

        void onTextChange(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;

            SpotifyViewModel.Instance.search(searchBar.Text);
        }
    }
}