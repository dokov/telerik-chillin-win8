using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Everlive.Sdk.Core.Model.Base;
using Telerik.Everlive.Sdk.Core.Model.System;
using Telerik.Everlive.Sdk.Core.Serialization;

namespace Telerik_Chillin.Model {
	[ServerType("Places")]
	public class Place : DataItem {
		private string title;
		public string Title {
			get {
				return this.title;
			}
			set {
				if (this.title != value) {
					this.title = value;
					this.OnPropertyChanged("Title");
				}
			}
		}

		private string description;
		public string Description {
			get {
				return this.description;
			}
			set {
				if (this.description != value) {
					this.description = value;
					this.OnPropertyChanged("Description");
				}
			}
		}

		private string discount;
		public string Discount {
			get {
				return this.discount;
			}
			set {
				if (this.discount != value) {
					this.discount = value;
					this.OnPropertyChanged("Discount");
				}
			}
		}

		private string address;
		public string Address {
			get {
				return this.address;
			}
			set {
				if (this.address != value) {
					this.address = value;
					this.OnPropertyChanged("Address");
				}
			}
		}

		private GeoPoint location;
		public GeoPoint Location {
			get {
				return this.location;
			}
			set {
				if (this.location != value) {
					this.location = value;
					this.OnPropertyChanged("Location");
				}
			}
		}

		private int commentsCount;
		public int CommentsCount {
			get {
				return this.commentsCount;
			}
			set {
				if (this.commentsCount != value) {
					this.commentsCount = value;
					this.OnPropertyChanged("CommentsCount");
				}
			}
		}

		private double rating;
		public double Rating {
			get {
				return this.rating;
			}
			set {
				if (this.rating != value) {
					this.rating = value;
					this.OnPropertyChanged("Rating");
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

		private List<Guid> images;
		public List<Guid> Images {
			get {
				return this.images;
			}
			set {
				if (this.images != value) {
					this.images = value;
					this.OnPropertyChanged("Images");
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

		public IEnumerable<string> ImagePaths {
			get {
				return this.Images.Select(img => DataManager.Instance.GetFileDownloadUrl(img));
			}
		}

		public string MapImagePath {
			get {
				return string.Format("http://maps.googleapis.com/maps/api/staticmap?center={0},{1}&zoom=8&size=400x300&scale1&sensor=false&markers=color:red%7Clabel:A%7C{0},{1}", this.Location.Latitude, this.Location.Longitude);
			}
		}

		public string RatingText {
			get {
				return string.Format("{0:0.0}", this.Rating);
			}
		}

		public string DiscountText {
			get {
				if (string.IsNullOrEmpty(this.Discount) == false) {
					return this.Discount;
				} else {
					return "No";
				}
			}
		}

		#endregion
	}
}
