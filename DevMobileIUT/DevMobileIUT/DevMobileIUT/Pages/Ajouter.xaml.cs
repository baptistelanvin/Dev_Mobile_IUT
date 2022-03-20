using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using DevMobileIUT.ViewModels;
using DevMobileIUT.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;

namespace DevMobileIUT.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ajouter : ContentPage
    {
        private int compteur;

        private MediaFile Image { get; set; }

        public Ajouter()  //L'initialisation des éléments de l'ajout
        {
            InitializeComponent();
            songName.Text = String.Empty;
            albumName.Text = String.Empty;
            artistName.Text = String.Empty;
            songDate.Date = DateTime.Now;
            compteur = 0;
           
        }


        private async void OnPickImageClick(object sender, EventArgs args) //Méthode permettant de sélectionner une image dans la galerie du téléphone.
        {

            this.Image = await CrossMedia.Current.PickPhotoAsync();

            if (this.Image == null)
            {
                return;
            }

            pochettePicker.Source = ImageSource.FromStream(() =>
            {
                return this.Image.GetStream();
            });

        }

        public async void OnButtonClicked(object sender, EventArgs args) //Méthode qui permet d'ajouter un élément dans la liste personnelle. 
        {

            bool isFormValid = true; //booléen permettant de vérifier que l'ajout est valide

            if (songName.Text == String.Empty)
            {
                isFormValid = false;
            }

            if (albumName.Text == String.Empty)
            {
                isFormValid = false;
            }

            if (artistName.Text == String.Empty)
            {
                isFormValid = false;
            }

            if (songDate.Date == null)
            {
                isFormValid = false;
            }

            if (isFormValid) //on ajoute seulement si tous les champs sont remplis
            {
                var spotifyViewModel = SpotifyViewModel.Instance;
                SpotifyDatabase spotifyDB = await SpotifyDatabase.Instance;
                Musique song = new Musique  //On crée l'objet à ajouter ici
                { 
                    Pochette = Image.Path,
                    Titre = songName.Text,
                    Album = albumName.Text,
                    Artiste = artistName.Text,
                    Annee = songDate.Date.Year.ToString(),
                    ID = compteur + 1,

                };

                spotifyViewModel.AddSong(song); //On ajoute la musique à la liste personnelle
                await spotifyDB.AddMusiqueAsync(song); //On ajoute la musique en base de données
                pochettePicker.Source = ""; //On réinitialise les éléments
                compteur += 1;
                songDate.Date = DateTime.Now;
                songName.Text = String.Empty;
                albumName.Text = String.Empty;
                artistName.Text = String.Empty;
            }
        }


    }
}