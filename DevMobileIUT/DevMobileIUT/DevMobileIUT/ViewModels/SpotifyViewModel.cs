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

        private void initList()
        {
           for (int i = 0; i < 10; i++)
            {
                
            }
            
            var music1 = new Musique();
            music1.Titre = "zkzskzskzk";

            ListOfMusiques.Add(music1);
        }

        private void connectSpotifyAPI()
        {
            var spotify = new SpotifyClient("BQCY_96tfwifmX97Ntet35E_-rt9YQ3IvjbOfoQXzfNvSqsz87vVU7KS675_Pg0vJhhHZ4ulFUj9Ga8tuq-8TQJXt2eorfG6Xwxm6OOsOAZ_DlfGD92tOHaDbnr9ipZlMoSbSunJiaRKrJyCt3ZHN91MpyQZbX7a4MF29A");
 
        }
    }
}
