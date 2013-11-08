using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Everlive.Sdk.Core.Model.Base;
using Telerik.Everlive.Sdk.Core.Serialization;

namespace Telerik_Chillin.Model {
	[ServerType("Menu")]
	public class CafeteriaMenu : DataItem {
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

		private DateTime menuDate;
		public DateTime MenuDate {
			get {
				return this.menuDate;
			}
			set {
				if (this.menuDate != value) {
					this.menuDate = value;
					this.OnPropertyChanged("MenuDate");
				}
			}
		}
	}
}
