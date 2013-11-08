using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
	public sealed partial class HomePage : UserControl {
		public HomePage() {
			this.InitializeComponent();

			var menuItems = new List<MenuItem>();

			menuItems.Add(new NavigationMenuItem() {
				Label = "Cafeteria menu",
				Image = "ms-appx:///Images/Menu/cafeteria.jpg",
				PageDescription = new SimplePageDescription() {
					Title = "Cafeteria menu",
					CanGoBack = true,
					ContentType = typeof(CafeteriaMenuPage),
				},
			});

			menuItems.Add(new NavigationMenuItem() {
				Label = "Eating Outside",
				Image = "ms-appx:///Images/Menu/lunch.jpg",
				PageDescription = new SimplePageDescription() {
					Title = "Eating Outside",
					CanGoBack = true,
					ContentType = typeof(PlacesListPage),
				},
			});

			menuItems.Add(new NavigationMenuItem() {
				Label = "Company Discounts",
				Image = "ms-appx:///Images/Menu/discounts.jpg",
				PageDescription = new SimplePageDescription() {
					Title = "Discounts",
					CanGoBack = true,
					ContentType = typeof(DiscountCategoriesPage),
				},
			});

			menuItems.Add(new NavigationMenuItem() {
				Label = "Sport Activities",
				Image = "ms-appx:///Images/Menu/sports.jpg",
				PageDescription = new SimplePageDescription() {
					Title = "Sport Activities",
					CanGoBack = true,
					ContentType = typeof(UnderConstructionPage),
				},
			});

			menuItems.Add(new NavigationMenuItem() {
				Label = "News & Messages",
				Image = "ms-appx:///Images/Menu/messages.jpg",
				PageDescription = new SimplePageDescription() {
					Title = "News & Messages",
					CanGoBack = true,
					ContentType = typeof(UnderConstructionPage),
				},
			});

			this.Menu.ItemsSource = menuItems;
		}

		private void Menu_ItemClick(object sender, ItemClickEventArgs e) {
			var item = (MenuItem)e.ClickedItem;
			item.Tapped();
		}
	}
}
