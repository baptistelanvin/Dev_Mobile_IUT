using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevMobileIUT.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevMobileIUT.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailMusique : ContentPage
    {
        public DetailMusique(Musique musique)
        {
            InitializeComponent();
            BindingContext = musique;

        }

        public void BackToList(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }
    }
}