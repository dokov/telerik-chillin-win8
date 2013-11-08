using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Telerik_Chillin {
	public static class Helper {
		public static void NavigateTo(SimplePageDescription pageDescription) {
			Frame rootFrame = Window.Current.Content as Frame;
			rootFrame.Navigate(typeof(SimplePage), pageDescription);
		}
	}
}
