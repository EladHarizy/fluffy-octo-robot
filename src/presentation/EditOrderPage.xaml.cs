using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class EditOrderPage : Page {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		public Order Order { get; }

		public EditOrderPage(IBusiness business, Frame frame, Order order) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Order = order;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void ChangeStatus(object sender, RoutedEventArgs e) {}
	}
}