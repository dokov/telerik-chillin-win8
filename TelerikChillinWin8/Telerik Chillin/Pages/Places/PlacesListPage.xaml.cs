using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik_Chillin.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Telerik_Chillin.Pages {
	public sealed partial class PlacesListPage : UserControl {
		public PlacesListPage() {
			this.InitializeComponent();

			Initialize();
		}

		public async Task Initialize() {
			var places = await DataManager.Instance.GetPlaces();

			this.LoadingPanel.Visibility = Visibility.Collapsed;

			if (places.Count == 0) {
				this.NoItemsText.Visibility = Visibility.Visible;
			}

			this.Menu.ItemsSource = places;
		}

		private void Menu_ItemClick(object sender, ItemClickEventArgs e) {
			var place = (Place)e.ClickedItem;

			var spd = new SimplePageDescription() {
				Title = place.Title,
				CanGoBack = true,
				ContentType = typeof(PlaceInfoPage),
				InitializeAction = delegate(FrameworkElement fe) {
					var real = (PlaceInfoPage)fe;
					real.Initialize(place);
				}
			};

			Helper.NavigateTo(spd);
		}
	}
}
