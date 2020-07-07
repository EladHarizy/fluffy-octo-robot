using System.Windows.Controls;
using business;

namespace presentation {
	public partial class AddHostingUnitPage : Page {
		private IBusiness business;
		private Frame frame;

		public AddHostingUnitPage(IBusiness business, Frame frame) {
			this.business = business;
			this.frame = frame;
		}
	}
}