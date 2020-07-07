using System.Windows;
using System.Windows.Controls;
using business;

namespace presentation {
	public partial class AddHostingUnitPage : Page {
		private IBusiness Business { get; }
		private Frame Frame { get; }

		public AddHostingUnitPage(IBusiness business, Frame frame) {
			Business = business;
			Frame = frame;
		}
		private void AddHostingUnit(object sender, RoutedEventArgs e) {

		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}