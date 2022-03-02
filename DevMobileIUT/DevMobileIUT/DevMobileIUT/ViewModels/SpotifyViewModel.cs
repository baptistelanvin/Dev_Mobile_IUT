using DevMobileIUT.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DevMobileIUT.ViewModels
{
    public class SpotifyViewModel : BaseViewModel
    {
        private static SpotifyViewModel _instance = new SpotifyViewModel();
        public static SpotifyViewModel Instance { get { return _instance; } }
        SpotifyClient spotifyclient;

        public ObservableCollection<Musique> ListOfMusiques
        {
            get
            {
                return GetValue<ObservableCollection<Musique>>();
            }
            set
            {
                SetValue(value);
            }
        }

        public SpotifyViewModel()
        {
            connectSpotifyAPI();
            initList();
        }

        private async void initList()
        {
            ListOfMusiques = new ObservableCollection<Musique>();
            FullPlaylist playlist = await spotifyclient.Playlists.Get("37i9dQZEVXbIPWwFssbupI");
            foreach (PlaylistTrack<IPlayableItem> item in playlist.Tracks.Items)
            {
                if (item.Track is FullTrack track)
                {
                    Console.WriteLine(track.Name);
                    ListOfMusiques.Add(new Musique()
                    {
                        Titre = track.Name,
                        Album = track.Album.Name,
                        Artiste = track.Album.Artists[0].Name,
                        Annee = track.Album.ReleaseDate,
                        Pochette = track.Album.Images[0].Url,

                    });
                }
            }
        }

        private  void connectSpotifyAPI()
        {
            spotifyclient = new SpotifyClient("BQAWm8-R-CV2tyrTkBQ1KH-H3NtKWBJAWV_1duOPaZDJVndEDdsqb-oJMFMVgIioWzZ-I_Qk18_1qmMmt-r5-0tPhjm32J8Ie6OhpJNj1-izL16dqqib-6ZwKZqtsMdBrMC7SNMFtMwq9JHBj908Milkwp5Xq61ikoRmfg");
            
        }
    }
}
