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
	public partial class ListePerso : ContentPage
	{
		public ListePerso ()
		{
			InitializeComponent ();
			BindingContext = SpotifyViewModel.Instance;
		}
	}
}