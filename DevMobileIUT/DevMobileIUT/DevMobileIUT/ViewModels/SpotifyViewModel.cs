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
                    ListOfMusiques.Add(new Musique()
                    {
                        ID = compteur,
                        Titre = track.Name,
                        Album = track.Album.Name,
                        Artiste = track.Album.Artists[0].Name,
                        Annee = track.Album.ReleaseDate,
                        Pochette = track.Album.Images[0].Url,
                    });
                    compteur++;
                }
                
            }
        }

        private async void search()
        {
            var query = "Zipette";
            ListOfResults = new ObservableCollection<Musique>();
            var search = await spotifyclient.Search.Item(new SearchRequest(SearchRequest.Types.Track, query));
            foreach (var item in search.Tracks.Items)
            {
                ListOfResults.Add(new Musique()
                {
                    Titre = item.Name,
                });
            }
        }


        private  void connectSpotifyAPI()
        {
            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("4ca27c28962d4d86b36f9e85d6f97fc0", "6eeef680576c4ddb829f8f20b4006809"));
            spotifyclient = new SpotifyClient(config);
            
        }
    }
}
