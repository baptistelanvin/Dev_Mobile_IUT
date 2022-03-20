using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMobileIUT.Models;
using DevMobileIUT.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevMobileIUT.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailArtiste : ContentPage
    {
        public DetailArtiste(Musique musique) //La méthode permet d'avoir le détail d'un artiste grâce à une musique fournie en paramètres
        {
            InitializeComponent();

            ImageArtiste.Source = SpotifyViewModel.Instance.getArtisteImage(musique.IdArtiste); //On récupère l'image d'un artiste

            NomArtiste.Text = SpotifyViewModel.Instance.getArtisteNom(musique.IdArtiste); //On récupère le nom d'un artiste

            NbAbonnement.Text = SpotifyViewModel.Instance.getNbAbonnement(musique.IdArtiste); //On récupère le nombre d'abonnées d'un artiste


            SpotifyViewModel.Instance.listTopTracks(musique.IdArtiste); //On appelle la méthode pour avoir les titres les plus populaires d'un artiste
            BindingContext = SpotifyViewModel.Instance;

        }

        public void BackToList(object sender, EventArgs args) //retour à la liste
        {
            Navigation.PopAsync();
        }
    }
}