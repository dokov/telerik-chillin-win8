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
	public sealed partial class DiscountsPage : UserControl {
		public DiscountsPage() {
			this.InitializeComponent();
		}

		public async Task Initialize(string categoryId) {
			var discounts = await DataManager.Instance.GetDiscounts(categoryId);

			this.LoadingPanel.Visibility = Visibility.Collapsed;

			if (discounts.Count == 0) {
				this.NoItemsText.Visibility = Visibility.Visible;
			}

			this.Menu.ItemsSource = discounts;
		}

		private void Menu_ItemClick(object sender, ItemClickEventArgs e) {
			var discount = (Discount)e.ClickedItem;

			var spd = new SimplePageDescription() {
				Title = discount.Title,
				CanGoBack = true,
				ContentType = typeof(DiscountInfoPage),
				InitializeAction = delegate(FrameworkElement fe) {
					var real = (DiscountInfoPage)fe;
					real.Initialize(discount);
				}
			};

			Helper.NavigateTo(spd);
		}
	}
}
