using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telerik.Everlive.Sdk.Core;
using Telerik.Everlive.Sdk.Core.Model.System;
using Telerik_Chillin.Model;
using Telerik_Chillin.ViewModel;
using Windows.Storage;
using Windows.UI.Popups;

namespace Telerik_Chillin {
	public class DataManager {

		#region Singleton

		private static DataManager instance;
		public static DataManager Instance {
			get {
				if (instance == null) {
					instance = new DataManager();
				}
				return instance;
			}
		}

		#endregion

		private EverliveApp Everlive {
			get;
			set;
		}

		private DataManager() {
			this.Everlive = new EverliveApp("E9ksyDFaxbPWtyZV");
			Task.Factory.StartNew(this.InitializeUsers, TaskCreationOptions.LongRunning);
			this.TryLoadAccessToken();
		}

		public bool IsLoggedIn {
			get;
			private set;
		}

		#region Discounts

		private List<DiscountCategory> discountCatetgories;
		public Task<List<DiscountCategory>> GetDiscountCategories() {
			if (this.discountCatetgories == null) {
				this.discountCatetgories = new List<DiscountCategory>() {
					new DiscountCategory("Hair Studios & Cosmetics", "hair.jpg"),
					new DiscountCategory("Rest & Relaxation", "rest.png"),
					new DiscountCategory("Optical", "optical.jpg"),
					new DiscountCategory("Health & Fitness", "health.png"),
					new DiscountCategory("Jewelry and Watches", "jewelry.jpg"),
					new DiscountCategory("Home Services", "home.jpg"),
					new DiscountCategory("Finances", "finances.jpg"),
					new DiscountCategory("Children & Education", "children.jpg"),
					new DiscountCategory("Eating out", "eating.jpg"),
					new DiscountCategory("Entertainment", "entertainment.png"),
					new DiscountCategory("Automotive Services", "automotive.png"),
					new DiscountCategory("Travel and Leisure", "travel.png"),
					new DiscountCategory("Computers and Gadgets", "computers.png"),
					new DiscountCategory("Internet, Telephone and TV Providers", "internet.png"),
				};
			}
			return Task.FromResult(this.discountCatetgories);
		}

		internal DiscountCategory GetDiscountCategory(string id) {
			return this.discountCatetgories.Where(cat => cat.Id == id).Single();
		}

		public async Task<List<Discount>> GetDiscounts(string categoryId) {
			var result = new List<Discount>();
			
			var queryResult = await this.Everlive.WorkWith().Data<Discount>().Get().Where(dsc => dsc.CategoryId == categoryId).TryExecuteAsync();
			if (queryResult.Success == true) {
				result.AddRange(queryResult.Value);
			}

			return result;
		}

		public async Task<List<FileInfo>> GetFileInfo(List<Guid> files) {
			var result = new List<FileInfo>();

			foreach (var f in files) {
				var queryResult = await this.Everlive.WorkWith().Files().GetById(f).TryExecuteAsync();
				if (queryResult.Success == true) {
					result.Add(new FileInfo() {
						Name = queryResult.Value.Filename,
						Url = this.Everlive.Files().GetFileDownloadUrl(f),
					});
				}
			}

			return result;
		}

		#endregion

		#region Cafeteria Menu

		public Task<CafeteriaMenu> GetCafeteriaMenuToday() {
			return this.GetCafeteriaMenuByDate(DateTime.Now);
		}

		public Task<CafeteriaMenu> GetCafeteriaMenuTomorrow() {
			return this.GetCafeteriaMenuByDate(DateTime.Now.AddDays(1));
		}

		private async Task<CafeteriaMenu> GetCafeteriaMenuByDate(DateTime date) {
			CafeteriaMenu result = null;

			var minDate = DateTime.SpecifyKind(date.Date, DateTimeKind.Local);
			var maxDate = minDate.AddDays(1);

			var queryResult = await this.Everlive.WorkWith().Data<CafeteriaMenu>().Get().Where(menu => menu.MenuDate >= minDate && menu.MenuDate < maxDate).TryExecuteAsync();
			if (queryResult.Success == true) {
				result = queryResult.Value.FirstOrDefault();
			}

			return result;
		}

		#endregion

		#region Places

		public async Task<List<Place>> GetPlaces() {
			var result = new List<Place>();

			var queryResult = await this.Everlive.WorkWith().Data<Place>().GetAll().TryExecuteAsync();
			if (queryResult.Success == true) {
				result.AddRange(queryResult.Value.OrderByDescending(pl => pl.Rating));
			}

			return result;
		}

		public async Task<List<Comment>> GetCommentsForPlace(Guid placeId) {
			var result = new List<Comment>();

			var queryResult = await this.Everlive.WorkWith().Data<Comment>().Get()
				.Where(comment => comment.CommentedItem == placeId)
				.OrderByDescending(comment => comment.CreatedAt)
				.Take(10)
				.TryExecuteAsync();
			if (queryResult.Success == true) {
				result.AddRange(queryResult.Value);
			}

			return result;
		}

		#endregion

		#region Users

		private static readonly string AccessTokenPath = "access_token.json";
		private JsonSerializer serializer = new JsonSerializer();


		private Dictionary<Guid, User> users;
		private void InitializeUsers() {
			this.users = this.Everlive.WorkWith().Users().GetAll().IncludeFields(user => new { user.Id, user.DisplayName }).ExecuteSync().ToDictionary(user => user.Id);
		}

		internal User GetUser(Guid id) {
			if (this.users.ContainsKey(id) == false) {
				var userQuery = this.Everlive.WorkWith().Users().GetById(id).TryExecuteSync();
				if (userQuery.Success == true) {
					this.users[id] = userQuery.Value;
				} else {
					return null;
				}
			}
			return this.users[id];
		}

		public async Task<bool> Login(string username, string password) {
			var loginResult = await this.Everlive.WorkWith().Authentication().Login(username, password).TryExecuteAsync();
			if (loginResult.Success == true) {
				this.SaveAccessToken(loginResult.Value);
				this.IsLoggedIn = true;
			} else {
				MessageDialog md = new MessageDialog(loginResult.Error.Message, "Sign in error");
				md.Commands.Add(new UICommand("Ok"));
				await md.ShowAsync();
			}
			return loginResult.Success;
		}

		public async Task Logout() {
			var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
			var fullPath = Path.Combine(storageFolder.Path, AccessTokenPath);
			var storageFile = await StorageFile.GetFileFromPathAsync(fullPath);
			await storageFile.DeleteAsync();
			await this.Everlive.WorkWith().Authentication().Logout().TryExecuteAsync();
			this.IsLoggedIn = false;
		}

		private async Task SaveAccessToken(AccessToken token) {
			var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
			StorageFile storageFile = await storageFolder.CreateFileAsync(AccessTokenPath, CreationCollisionOption.ReplaceExisting);
			using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite)) {
				var managedStream = stream.AsStream();
				using (var textWriter = new StreamWriter(managedStream)) {
					this.serializer.Serialize(textWriter, token);
				}
				await managedStream.FlushAsync();
			}
		}

		private async Task<AccessToken> LoadAccessToken() {
			var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
			var fullPath = Path.Combine(storageFolder.Path, AccessTokenPath);
			var storageFile = await StorageFile.GetFileFromPathAsync(fullPath);
			using (var stream = await storageFile.OpenAsync(FileAccessMode.ReadWrite)) {
				var managedStream = stream.AsStream();
				using (var textReader = new StreamReader(managedStream)) {
					return (AccessToken)this.serializer.Deserialize(textReader, typeof(AccessToken));
				}
			}
		}

		private async Task TryLoadAccessToken() {
			try {
				var accessToken = await this.LoadAccessToken();
				this.Everlive.AccessToken = accessToken;
				this.IsLoggedIn = true;
			} catch {
			}
		}

		#endregion

		#region Common

		public string GetFileDownloadUrl(Guid fileId) {
			return this.Everlive.Files().GetFileDownloadUrl(fileId);
		}

		#endregion
	}
}
