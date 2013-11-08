using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Everlive.Sdk.Core.Model.Base;
using Telerik.Everlive.Sdk.Core.Serialization;

namespace Telerik_Chillin.Model {
	[ServerType("Discounts")]
	public class Discount : DataItem {
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

		private string info;
		public string Info {
			get {
				return this.info;
			}
			set {
				if (this.info != value) {
					this.info = value;
					this.OnPropertyChanged("Info");
				}
			}
		}
		
		private string link;
		public string Link {
			get {
				return this.link;
			}
			set {
				if (this.link != value) {
					this.link = value;
					this.OnPropertyChanged("Link");
				}
			}
		}
		
		private string discountValue;
		[ServerProperty("Discount")]
		public string DiscountValue {
			get {
				return this.discountValue;
			}
			set {
				if (this.discountValue != value) {
					this.discountValue = value;
					this.OnPropertyChanged("DiscountValue");
				}
			}
		}
		
		private string condition;
		public string Condition {
			get {
				return this.condition;
			}
			set {
				if (this.condition != value) {
					this.condition = value;
					this.OnPropertyChanged("Condition");
				}
			}
		}
		
		private List<Guid> infoFiles;
		public List<Guid> InfoFiles {
			get {
				return this.infoFiles;
			}
			set {
				if (this.infoFiles != value) {
					this.infoFiles = value;
					this.OnPropertyChanged("InfoFiles");
				}
			}
		}

		private string categoryId;
		[ServerProperty("Category")]
		public string CategoryId {
			get {
				return this.categoryId;
			}
			set {
				if (this.categoryId != value) {
					this.categoryId = value;
					this.OnPropertyChanged("CategoryId");
				}
			}
		}

		public string DiscountText {
			get {
				return string.Format("Discount: {0}", this.DiscountValue);
			}
		}
	}
}
