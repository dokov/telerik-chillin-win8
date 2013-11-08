using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telerik_Chillin.ViewModel {
	public class MenuItem {
		public string Label {
			get;
			set;
		}

		public string Image {
			get;
			set;
		}

		public Action Tapped {
			get;
			set;
		}

		public MenuItem() {
		}

		public MenuItem(string label, string image) {
			this.Label = label;
			this.Image = image;
		}
	}
}
