using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik_Chillin.Model {
	public class DiscountCategory {
		public string Name {
			get;
			set;
		}

		public string Id {
			get;
			set;
		}

		public string ImageName {
			get;
			set;
		}

		public string IconPath {
			get {
				return string.Format("ms-appx:///Icons/Discounts/{0}", this.ImageName);
			}
		}

		public string MenuImagePath {
			get {
				return string.Format("ms-appx:///Images/Discounts/{0}", this.ImageName);
			}
		}

		public DiscountCategory(string name, string id, string imageName) {
			this.Name = name;
			this.Id = id;
			this.ImageName = imageName;
		}

		public DiscountCategory(string name, string imageName)
			: this(name, name, imageName) {

		}
	}
}
