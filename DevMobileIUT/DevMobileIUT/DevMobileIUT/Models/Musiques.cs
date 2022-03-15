using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace DevMobileIUT.Models
{
    public class Musique
    {
        public int ID { get; set; }
        public string Titre { get; set; }
        public string Artiste { get; set; }
        public string Album { get; set; }
        public string Annee { get; set; }
        public string Pochette { get; set; }
        public string ImageArtiste { get; set; }

    }
}
