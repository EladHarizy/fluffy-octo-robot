using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AdminPage : Page {
		private IBusiness Business { get; }

		private Frame Frame { get; }

		public AdminPage(IBusiness business, Frame frame) {
			InitializeComponent();
			Business = business;
			Frame = frame;
		}

		private void ViewGuestRequests(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AdminViewGuestRequestsPage(Business, Frame));
		}

		private void ViewUnits(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AdminViewUnitsPage(Business, Frame));
		}

		private void ViewOrders(object sender, RoutedEventArgs e) {
			// Frame.Navigate(new ViewOrdersPage(Business, Frame));
		}
	}
}