using DevMobileIUT.Models;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

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

        public ObservableCollection<Musique> ListOfTopTracks
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

        public ObservableCollection<Musique> ListOfAdds
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
            _ = this.initAsync();
            initList();
        }

        private async Task initAsync()
        {
            SpotifyDatabase spotifyDB = await SpotifyDatabase.Instance;
            if (spotifyDB.IsSpotifyDatabaseEmptyAsync().Result == true)
            {
               ListOfAdds = new ObservableCollection<Musique>();
            }
            else
            {
                ListOfAdds = new ObservableCollection<Musique>();
                List<Musique> list = spotifyDB.GetMusiquesAsync().Result;
                foreach (Musique musique in list)
                {
                    ListOfAdds.Add(musique);
                }
            }
        }

        public void AddSong(Musique song)
        {
            ListOfAdds.Add(song);
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
                    string ArtisteId = track.Album.Artists[0].Id;
                    ListOfMusiques.Add(new Musique()
                    {
                        ID = compteur,
                        Titre = track.Name,
                        Album = track.Album.Name,
                        Artiste = track.Album.Artists[0].Name,
                        Annee = anneeChaineEntière.Substring(0, 4),
                        Pochette = track.Album.Images[0].Url,
                        IdArtiste = ArtisteId,
                    });
                    compteur++;
                }
            }
        }

        public async void listTopTracks(string ArtisteId)
        {
            if (ArtisteId == null || ArtisteId == "")
            {
                ListOfTopTracks.Clear();
                new ObservableCollection<Musique>();
            }
            else
            {
                ListOfTopTracks = new ObservableCollection<Musique>();
                ArtistsTopTracksRequest country_code = new ArtistsTopTracksRequest("FR");
                ArtistsTopTracksResponse items = await spotifyclient.Artists.GetTopTracks(ArtisteId, country_code);
                int compteurTopTrack = 1;
                foreach (var objet in items.Tracks)
                {
                    FullArtist artist = await spotifyclient.Artists.Get(ArtisteId);
                    string objetAnnee = objet.Album.ReleaseDate;
                    if (compteurTopTrack <= 5)
                    {
                        ListOfTopTracks.Add(new Musique()
                        {
                            ID = compteurTopTrack,
                            Titre = objet.Name,
                            Artiste = objet.Album.Artists[0].Name,
                            Annee = objetAnnee.Substring(0, 4),
                            Pochette = objet.Album.Images[0].Url,
                            IdArtiste = ArtisteId,
                        });
                        compteurTopTrack++;
                    }
                }
            }
        }

        public string getArtisteImage(string ArtisteId)
        {
            FullArtist artist = spotifyclient.Artists.Get(ArtisteId).Result;
            return artist.Images[0].Url;

        }

        public string getNbAbonnement(string ArtisteId)
        {
            FullArtist artist = spotifyclient.Artists.Get(ArtisteId).Result;
            return artist.Followers.Total.ToString();
        }


        public string getArtisteNom(string ArtisteId)
        {
            FullArtist artist = spotifyclient.Artists.Get(ArtisteId).Result;
            return artist.Name;

        }

        public async void search(string query)
        {
            if(query == null || query == "")
            {
                ListOfResults.Clear();
                new ObservableCollection<Musique>();
            }
            else { 
                ListOfResults = new ObservableCollection<Musique>();
                var search = await spotifyclient.Search.Item(new SearchRequest(SearchRequest.Types.Track, query));
                int compteur = 1;
                foreach (var item in search.Tracks.Items)
                {
                    string anneeChaineEntière = item.Album.ReleaseDate;
                    string ArtisteId = item.Album.Artists[0].Id;
                    ListOfResults.Add(new Musique()
                    {
                        ID = compteur,
                        Titre = item.Name,
                        Artiste = item.Album.Artists[0].Name,
                        Annee = anneeChaineEntière.Substring(0, 4),
                        Pochette = item.Album.Images[0].Url,
                        IdArtiste = ArtisteId,
                    });
                    compteur++;
                }
            }
        }


        private void connectSpotifyAPI()
        {
            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("4ca27c28962d4d86b36f9e85d6f97fc0", "6eeef680576c4ddb829f8f20b4006809"));
            spotifyclient = new SpotifyClient(config);
            
        }
    }
}
