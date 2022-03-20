using DevMobileIUT.Models;
using DevMobileIUT.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DevMobileIUT.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TopFrance : ContentPage
    {
        public TopFrance()
        {
            InitializeComponent();
            BindingContext = SpotifyViewModel.Instance;
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Musique current = (e.CurrentSelection.FirstOrDefault() as Musique);
            if (current == null)
            {
                return;
            }

            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new DetailMusique(current));
        }
    }
}