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
        SpotifyClient spotifyclient; //Création d'un objet SpotifyClient pour permettre la connexion à Spotify

        public ObservableCollection<Musique> ListOfMusiques //Observable pour le top 50
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

        public ObservableCollection<Musique> ListOfResults //Observable pour les résultats de la recherche
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

        public ObservableCollection<Musique> ListOfTopTracks //Observable pour le top titre des artistes
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

        public ObservableCollection<Musique> ListOfAdds //Observable pour les ajouts d'un utilisateur
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
            connectSpotifyAPI(); //On connecte l'API à un compte Spotify
            _ = this.initAsync(); //Initialisation de la base de données
            initList(); //Remplissage du Top50
        }

        private async Task initAsync() //Récupération des ajouts de l'utilisateur en BD
        {
            SpotifyDatabase spotifyDB = await SpotifyDatabase.Instance;
            if (spotifyDB.IsSpotifyDatabaseEmptyAsync().Result == true) 
            {
               ListOfAdds = new ObservableCollection<Musique>(); //Dans le cas où il n'y a aucun élément en base
            }
            else
            {
                ListOfAdds = new ObservableCollection<Musique>();
                List<Musique> list = spotifyDB.GetMusiquesAsync().Result;
                foreach (Musique musique in list)
                {
                    ListOfAdds.Add(musique); //On remplit l'observable s'il y a des éléments en base
                }
            }
        }

        public void AddSong(Musique song) //Méthode qui permet d'ajouter une musique à la liste des ajouts de l'utilisateur
        {
            ListOfAdds.Add(song);
        }

        private async void initList() //Initialisation de la liste du Top 50 France 
        {
            ListOfMusiques = new ObservableCollection<Musique>(); 
            FullPlaylist playlist = await spotifyclient.Playlists.Get("37i9dQZEVXbIPWwFssbupI"); //On récupère le top 50
            int compteur = 1;
            foreach (PlaylistTrack<IPlayableItem> item in playlist.Tracks.Items) //Pour chaque élément du Top50, on l'ajoute dans notre liste du Top 50
            {
                if (item.Track is FullTrack track)
                {
                    string anneeChaineEntière = track.Album.ReleaseDate;
                    string ArtisteId = track.Album.Artists[0].Id;
                    ListOfMusiques.Add(new Musique() //On récupère certaines propriétés des éléments pour créer un objet de notre type Musique
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

        public async void listTopTracks(string ArtisteId) //Méthode qui permet de récupérer les top titres d'un artiste qui a son ID fourni en paramètre
        {
            ListOfTopTracks = new ObservableCollection<Musique>();
            ArtistsTopTracksRequest country_code = new ArtistsTopTracksRequest("FR");
            ArtistsTopTracksResponse items = await spotifyclient.Artists.GetTopTracks(ArtisteId, country_code); //On fait une requete via l'API
            int compteurTopTrack = 1;
            foreach (var objet in items.Tracks) //Pour chaque top titre d'un artiste, on l'ajoute dans notre liste de top titres d'un artiste
            {
                FullArtist artist = await spotifyclient.Artists.Get(ArtisteId);
                string objetAnnee = objet.Album.ReleaseDate;
                if (compteurTopTrack <= 5) //On en ajoute 5
                {
                    ListOfTopTracks.Add(new Musique() //On récupère certaines propriétés des éléments pour créer un objet de notre type Musique
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

        public string getArtisteImage(string ArtisteId) //Méthode qui permet de récupérer l'image d'un artiste avec l'ID de l'artiste fourni en paramètre
        {
            FullArtist artist = spotifyclient.Artists.Get(ArtisteId).Result;
            return artist.Images[0].Url;

        }

        public string getNbAbonnement(string ArtisteId) //Méthode qui permet de récupérer le nombre d'abonnés d'un artiste avec l'ID de l'artiste fourni en paramètre
        {
            FullArtist artist = spotifyclient.Artists.Get(ArtisteId).Result;
            return artist.Followers.Total.ToString();
        }


        public string getArtisteNom(string ArtisteId) //Méthode qui permet de récupérer le nom d'un artiste avec l'ID de l'artiste fourni en paramètre
        {
            FullArtist artist = spotifyclient.Artists.Get(ArtisteId).Result;
            return artist.Name;

        }

        public async void search(string query) //Méthode permettant de faire une recherche via l'API Spotify avec le texte pour la recherche fourni en paramètre
        {
            if(query == null || query == "") //Si il n'y a rien dans le champ de recherche, on affiche rien
            {
                ListOfResults.Clear();
                new ObservableCollection<Musique>();
            }
            else { //Sinon, on affiche les résultats de la recherche dans une liste
                ListOfResults = new ObservableCollection<Musique>();
                var search = await spotifyclient.Search.Item(new SearchRequest(SearchRequest.Types.Track, query)); //On fait une recherche via l'API Spotify
                int compteur = 1; 
                foreach (var item in search.Tracks.Items) //On récupère les résultats de la recherche puis on les met dans une liste
                {
                    string anneeChaineEntière = item.Album.ReleaseDate;
                    string ArtisteId = item.Album.Artists[0].Id;
                    ListOfResults.Add(new Musique() //On récupère certaines propriétés des éléments pour créer un objet de notre type Musique
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


        private void connectSpotifyAPI() //Méthode permettant la connexion à l'API de Spotify
        {
            var config = SpotifyClientConfig.CreateDefault().WithAuthenticator(new ClientCredentialsAuthenticator("CLIENT_ID", "CLIENT_SECRET"));
            spotifyclient = new SpotifyClient(config);
            
        }
    }
}
