using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
	public sealed partial class CafeteriaMenuPage : UserControl {
		public CafeteriaMenuPage() {
			this.InitializeComponent();

			this.Initialize();
		}

		public async Task Initialize() {
			var menus = new List<CafeteriaMenuItem>();

			var menuToday = await DataManager.Instance.GetCafeteriaMenuToday();
			if (menuToday != null) {
				menus.Add(new CafeteriaMenuItem() {
					Day = "Today",
					Menu = menuToday.Content,
				});
			}

			var menuTomorrow = await DataManager.Instance.GetCafeteriaMenuTomorrow();
			if (menuTomorrow != null) {
				menus.Add(new CafeteriaMenuItem() {
					Day = "Tomorrow",
					Menu = menuTomorrow.Content,
				});
			}

			
			this.LoadingPanel.Visibility = Visibility.Collapsed;

			if (menus.Count == 0) {
				this.NoItemsText.Visibility = Visibility.Visible;
			}

			this.Menu.ItemsSource = menus;
		}
	}

	public class CafeteriaMenuItem {
		public string Day {
			get;
			set;
		}

		public string Menu {
			get;
			set;
		}
	}
}
