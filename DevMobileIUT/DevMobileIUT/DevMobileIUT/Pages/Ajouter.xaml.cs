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

namespace DevMobileIUT.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ajouter : ContentPage
    {
        private int compteur;

        private MediaFile Image { get; set; }
        public Ajouter()
        {
            InitializeComponent();
            songName.Text = String.Empty;
            albumName.Text = String.Empty;
            artistName.Text = String.Empty;
            songDate.Date = DateTime.Now;
            compteur = 50;
        }


        private async void OnPickImageClick(object sender, EventArgs args)
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

        public async void OnButtonClicked(object sender, EventArgs args)
        {

            bool isFormValid = true;

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

            if (isFormValid)
            {
                var spotifyViewModel = SpotifyViewModel.Instance;

                Musique song = new Musique();
                {
                    song.Titre = songName.Text;
                    song.Album = albumName.Text;
                    song.Artiste = artistName.Text;
                    song.Annee = songDate.Date.Year.ToString();
                    song.ID = compteur + 1;

                };

                spotifyViewModel.addSong(song);
                pochettePicker.Source = "";
                compteur += 1;
                songName.Text = String.Empty;
                albumName.Text = String.Empty;
                artistName.Text = String.Empty;

                await Shell.Current.GoToAsync($"//Top", true);
            }
        }


    }
}