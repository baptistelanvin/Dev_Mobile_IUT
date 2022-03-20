using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace DevMobileIUT.Models
{
    public class Musique
    {
        public int ID { get; set; } //ID de la musique
        public string Titre { get; set; } //Titre de la musique
        public string Artiste { get; set; } //Artiste du titre
        public string Album { get; set; } //Album 
        public string Annee { get; set; } //Année de sortie de l'album
        public string Pochette { get; set; } //Image qui est la pochette de l'album
        public string ImageArtiste { get; set; } //Image qui est la photo de l'artiste
        public int NbAbonnementsArtiste { get; set; } //Le nombre d'abonnés de l'artiste
        public string IdArtiste { get; set; } //l'ID de l'artiste
    }
}
