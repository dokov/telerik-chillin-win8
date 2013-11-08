﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Telerik_Chillin.Flyouts;
using Telerik_Chillin.Pages;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace Telerik_Chillin
{
	/// <summary>
	/// Provides application-specific behavior to supplement the default Application class.
	/// </summary>
	sealed partial class App : Application
	{
		/// <summary>
		/// Initializes the singleton application object.  This is the first line of authored code
		/// executed, and as such is the logical equivalent of main() or WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		/// <summary>
		/// Invoked when the application is launched normally by the end user.  Other entry points
		/// will be used when the application is launched to open a specific file, to display
		/// search results, and so forth.
		/// </summary>
		/// <param name="args">Details about the launch request and process.</param>
		protected override void OnLaunched(LaunchActivatedEventArgs args)
		{
			Frame rootFrame = Window.Current.Content as Frame;

			// Do not repeat app initialization when the Window already has content,
			// just ensure that the window is active
			if (rootFrame == null)
			{
				// Create a Frame to act as the navigation context and navigate to the first page
				rootFrame = new Frame();

				// Set the application background Image
				rootFrame.Background = new ImageBrush {
					Stretch = Windows.UI.Xaml.Media.Stretch.UniformToFill,
					ImageSource = new BitmapImage { UriSource = new Uri("ms-appx:///Images/background.jpg") }
				};

				if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
				{
					//TODO: Load state from previously suspended application
				}

				// Place the frame in the current Window
				Window.Current.Content = rootFrame;
			}

			if (rootFrame.Content == null)
			{
				// When the navigation stack isn't restored navigate to the first page,
				// configuring the new page by passing required information as a navigation
				// parameter
				var initialPageDescription = new SimplePageDescription() {
					Title = "Telerik Chillin'",
					CanGoBack = false,
					ContentType = typeof(HomePage),
				};

				if (!rootFrame.Navigate(typeof(SimplePage), initialPageDescription))
				{
					throw new Exception("Failed to create initial page");
				}
			}
			// Ensure the current window is active
			Window.Current.Activate();

			var settingsPane = SettingsPane.GetForCurrentView();
			settingsPane.CommandsRequested += settingsPane_CommandsRequested;

			bool isLoggedIn = DataManager.Instance.IsLoggedIn;
		}


		void settingsPane_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args) {
			var commandHandler = new UICommandInvokedHandler(this.OnSettingsCommand);

			var generalSettingsCommand = new SettingsCommand("acc", "Account", commandHandler);
			args.Request.ApplicationCommands.Add(generalSettingsCommand);
		}

		private void OnSettingsCommand(IUICommand command) {
			var commandId = (string)command.Id;
			this.OnSettingsCommand(commandId);
		}

		private void OnSettingsCommand(string commandId) {
			if (commandId == "acc") {
				this.ShowFlyout(new AccountSettingsFlyout());
			}
		}

		public void ShowFlyout(UserControl control) {
			var popup = new Popup();
			popup.IsLightDismissEnabled = true;
			popup.Width = 346;
			popup.Height = Window.Current.Bounds.Height;

			control.Width = 346;
			control.Height = Window.Current.Bounds.Height;

			popup.Child = control;
			popup.SetValue(Canvas.LeftProperty, Window.Current.Bounds.Width - 346);
			popup.SetValue(Canvas.TopProperty, 0);
			popup.IsOpen = true;

			((IFlyout)control).Popup = popup;
		}

		/// <summary>
		/// Invoked when application execution is being suspended.  Application state is saved
		/// without knowing whether the application will be terminated or resumed with the contents
		/// of memory still intact.
		/// </summary>
		/// <param name="sender">The source of the suspend request.</param>
		/// <param name="e">Details about the suspend request.</param>
		private void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			//TODO: Save application state and stop any background activity
			deferral.Complete();
		}
	}
}
