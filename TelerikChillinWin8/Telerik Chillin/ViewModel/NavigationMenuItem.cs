using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Telerik_Chillin.ViewModel {
	public class NavigationMenuItem : MenuItem {
		public SimplePageDescription PageDescription {
			get;
			set;
		}

		public NavigationMenuItem() {
			this.Tapped = Navigate;
		}

		private void Navigate() {
			Helper.NavigateTo(this.PageDescription);
		}
	}
}
