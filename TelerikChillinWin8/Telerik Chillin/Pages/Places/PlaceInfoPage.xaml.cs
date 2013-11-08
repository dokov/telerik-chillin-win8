using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik_Chillin.Model;
using Telerik_Chillin.Pages.Places;
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
	public sealed partial class PlaceInfoPage : UserControl {
		public PlaceInfoPage() {
			this.InitializeComponent();
		}

		public async Task Initialize(Place place) {
			this.LayoutRoot.DataContext = place;

			var comments = await DataManager.Instance.GetCommentsForPlace(place.Id);

			this.LoadingPanel.Visibility = Visibility.Collapsed;

			if (comments.Count == 0) {
				this.NoItemsText.Visibility = Visibility.Visible;
			}

			this.CommentsList.ItemsSource = comments;
		}

		private void Image_Tapped(object sender, TappedRoutedEventArgs e) {
			var originalSender = (Image)sender;
			var comment = (Comment)originalSender.DataContext;

			var bounds = Window.Current.Bounds;

			var popup = new Popup();

			var imagePopup = new ImagePopup();
			imagePopup.DataContext = comment.ImagePath;
			imagePopup.Width = bounds.Width;
			imagePopup.Height = bounds.Height;

			popup.Child = imagePopup;
			popup.IsLightDismissEnabled = true;
			popup.IsOpen = true;
		}
	}
}
