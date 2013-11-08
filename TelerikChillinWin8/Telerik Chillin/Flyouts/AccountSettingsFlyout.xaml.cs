using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Telerik_Chillin.Flyouts {
	public sealed partial class AccountSettingsFlyout : UserControl, IFlyout {
		public Popup Popup {
			get;
			set;
		}

		public AccountSettingsFlyout() {
			this.InitializeComponent();
			this.RefreshUI();
		}

		private async void LoginButton_Click(object sender, RoutedEventArgs e) {
			this.ChangeLightDismissEnabled(false);
			await DataManager.Instance.Login(this.Username.Text, this.Password.Password);
			this.ChangeLightDismissEnabled(true);
			this.RefreshUI();
		}

		private async void LogoutButton_Click(object sender, RoutedEventArgs e) {
			await DataManager.Instance.Logout();
			this.RefreshUI();
		}

		private void RefreshUI() {
			if (DataManager.Instance.IsLoggedIn == true) {
				this.LogInPanel.Visibility = Visibility.Collapsed;
				this.LoggedInPanel.Visibility = Visibility.Visible;
			} else {
				this.LogInPanel.Visibility = Visibility.Visible;
				this.LoggedInPanel.Visibility = Visibility.Collapsed;
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e) {
			this.Popup.IsOpen = false;
			SettingsPane.Show();
		}

		private void ChangeLightDismissEnabled(bool value) {
			this.Popup.IsOpen = false;
			this.Popup.IsLightDismissEnabled = value;
			this.Popup.IsOpen = true;
		}
	}

	public interface IFlyout {
		Popup Popup {
			get;
			set;
		}
	}
}
