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
	public sealed partial class DiscountInfoPage : UserControl {
		public DiscountInfoPage() {
			this.InitializeComponent();
		}

		internal async Task Initialize(Discount discount) {
			this.LayoutRoot.DataContext = discount;

			if (string.IsNullOrEmpty(discount.Link) == false) {
				this.MoreInfoButton.Visibility = Visibility.Visible;
			}

			var files = await DataManager.Instance.GetFileInfo(discount.InfoFiles);
			if (files.Count > 0) {
				this.RelatedFilesControl.ItemsSource = files;
				this.FilesPanel.Visibility = Visibility.Visible;
			}
		}
	}
}
