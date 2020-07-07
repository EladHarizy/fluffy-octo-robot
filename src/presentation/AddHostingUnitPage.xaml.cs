using System.Windows.Controls;
using business;

namespace presentation {
	internal class AddHostingUnitPage : Page {
		private IBusiness business;
		private Frame frame;

		public AddHostingUnitPage(IBusiness business, Frame frame) {
			this.business = business;
			this.frame = frame;
		}
	}
}