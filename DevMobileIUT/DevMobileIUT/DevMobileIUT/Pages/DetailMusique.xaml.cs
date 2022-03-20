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
    public partial class DetailMusique : ContentPage
    {
        public DetailMusique(Musique musique)
        {
            InitializeComponent();

            ImageArtiste.Source = SpotifyViewModel.Instance.getArtisteImage(musique.IdArtiste);

            NomArtiste.Text = SpotifyViewModel.Instance.getArtisteNom(musique.IdArtiste);

            NbAbonnement.Text = SpotifyViewModel.Instance.getNbAbonnement(musique.IdArtiste);


            SpotifyViewModel.Instance.listTopTracks(musique.IdArtiste);
            BindingContext = SpotifyViewModel.Instance;

        }

        public void BackToList(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
    }
}