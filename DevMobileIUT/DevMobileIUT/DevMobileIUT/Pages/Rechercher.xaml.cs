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

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e) //Méthode pour afficher le détail d'un artiste lorsque l'on clique sur un titre
        {
            Musique current = (e.CurrentSelection.FirstOrDefault() as Musique); 
            if (current == null)
            {
                return;
            }
         (sender as CollectionView).SelectedItem = null; 
            await Navigation.PushAsync(new DetailArtiste(current)); //On affiche la page de detail
        }

        void onTextChange(object sender, TextChangedEventArgs e) //On appelle ici la fonction search() qui permet de faire une recherche via l'API Spotify
        {
            SearchBar searchBar = (SearchBar)sender;

            SpotifyViewModel.Instance.search(searchBar.Text); //Appel de la fonction 
        }
    }
}