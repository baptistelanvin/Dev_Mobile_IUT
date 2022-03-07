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
        public ObservableCollection<Musique> ListOfResults
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
            search("zipette");
        }

        public void addSong(Musique song)
        {
            ListOfMusiques.Add(song);
        }

        private async void initList()
        {
            ListOfMusiques = new ObservableCollection<Musique>();
            FullPlaylist playlist = await spotifyclient.Playlists.Get("37i9dQZEVXbIPWwFssbupI");
            int compteur = 1;
            foreach (PlaylistTrack<IPlayableItem> item in playlist.Tracks.Items)
            {
                if (item.Track is FullTrack track)
                {
                    string anneeChaineEntière = track.Album.ReleaseDate;
                    ListOfMusiques.Add(new Musique()
                    {
                        ID = compteur,
                        Titre = track.Name,
                        Album = track.Album.Name,
                        Artiste = track.Album.Artists[0].Name,
                        Annee = anneeChaineEntière.Substring(0,4),
                        Pochette = track.Album.Images[0].Url,
                    });
                    compteur++;
                }
                
            }

        }

        private async void search(string query)
        {
            ListOfResults = new ObservableCollection<Musique>();
            var search = await spotifyclient.Search.Item(new SearchRequest(SearchRequest.Types.Track, query));
            int compteur = 1;
            foreach (var item in search.Tracks.Items)
            {
                string anneeChaineEntière = item.Album.ReleaseDate;
                ListOfResults.Add(new Musique()
                {
                    ID = compteur,
                    Titre = item.Name,
                    Artiste = item.Album.Artists[0].Name,
                    Annee = anneeChaineEntière.Substring(0, 4),
                    Pochette = item.Album.Images[0].Url,
                });
                compteur++;

            }
        }


        private void connectSpotifyAPI()
        {
            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("4ca27c28962d4d86b36f9e85d6f97fc0", "6eeef680576c4ddb829f8f20b4006809"));
            spotifyclient = new SpotifyClient(config);
            
        }
    }
}
