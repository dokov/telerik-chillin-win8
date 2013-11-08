using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Everlive.Sdk.Core.Model.Base;
using Telerik.Everlive.Sdk.Core.Model.System;
using Telerik.Everlive.Sdk.Core.Serialization;
using Windows.UI.Xaml;

namespace Telerik_Chillin.Model {
	[ServerType("Comment")]
	public class Comment : DataItem {
		private string content;
		public string Content {
			get {
				return this.content;
			}
			set {
				if (this.content != value) {
					this.content = value;
					this.OnPropertyChanged("Content");
				}
			}
		}

		private Guid commentedItem;
		public Guid CommentedItem {
			get {
				return this.commentedItem;
			}
			set {
				if (this.commentedItem != value) {
					this.commentedItem = value;
					this.OnPropertyChanged("CommentedItem");
				}
			}
		}

		private Guid image;
		public Guid Image {
			get {
				return this.image;
			}
			set {
				if (this.image != value) {
					this.image = value;
					this.OnPropertyChanged("Image");
				}
			}
		}

		#region UI helpers

		public string ImagePath {
			get {
				if (this.Image != Guid.Empty) {
					return DataManager.Instance.GetFileDownloadUrl(this.Image);
				} else {
					return string.Empty;
				}
			}
		}

		public User User {
			get {
				return DataManager.Instance.GetUser(this.CreatedBy);
			}
		}

		public string UserDisplayName {
			get {
				var user = this.User;
				if (user != null) {
					return user.DisplayName;
				} else {
					return "Unknown";
				}
			}
		}

		public string DatePostedText {
			get {
				if (this.CreatedAt.ToUniversalTime().Date == DateTime.UtcNow.Date) {
					return this.CreatedAt.ToString("HH:mm"); //string.Format("today, {0}", this.CreatedAt.ToString("HH:mm"));
				} else {
					return this.CreatedAt.ToString("dd.MM.yyyy");
				}
			}
		}

		public Visibility ImageVisibility {
			get {
				if (this.Image != Guid.Empty) {
					return Visibility.Visible;
				} else {
					return Visibility.Collapsed;
				}
			}
		}

		#endregion
	}
}
