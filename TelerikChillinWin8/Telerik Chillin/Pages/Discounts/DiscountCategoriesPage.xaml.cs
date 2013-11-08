using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Telerik_Chillin.ViewModel;
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
	public sealed partial class DiscountCategoriesPage : UserControl {
		public DiscountCategoriesPage() {
			this.InitializeComponent();

			this.Initialize();
		}

		private async Task Initialize() {
			var categories = await DataManager.Instance.GetDiscountCategories();

			var menuItems = new List<MenuItem>();

			foreach (var category in categories) {
				menuItems.Add(new NavigationMenuItem() {
					Label = category.Name,
					Image = category.MenuImagePath,
					PageDescription = new SimplePageDescription() {
						Title = category.Name,
						CanGoBack = true,
						ContentType = typeof(DiscountsPage),
						InitializeAction = delegate(FrameworkElement fe) {
							var real = (DiscountsPage)fe;
							real.Initialize(category.Id);
						},
					},
				});
			}

			this.Menu.ItemsSource = menuItems;
		}

		private void Menu_ItemClick(object sender, ItemClickEventArgs e) {
			var item = (MenuItem)e.ClickedItem;
			item.Tapped();
		}
	}
}
