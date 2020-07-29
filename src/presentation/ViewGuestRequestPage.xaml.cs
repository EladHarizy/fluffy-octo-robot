using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class ViewGuestRequestPage : Page {
		private IBusiness Business { get; }

		private Frame Frame { get; }

		public GuestRequest GuestRequest { get; }

		public ViewGuestRequestPage(IBusiness business, Frame frame, GuestRequest guest_request) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			GuestRequest = guest_request;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}