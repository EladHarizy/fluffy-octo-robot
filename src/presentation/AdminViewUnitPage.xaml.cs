using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AdminViewUnitPage : Page {
		private IBusiness Business { get; }
		private Frame Frame { get; }
		public Unit Unit { get; }

		public IEnumerable<Order> Orders { get; }

		public AdminViewUnitPage(IBusiness business, Frame frame, Unit unit) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Unit = unit;
			Orders = Business.OrdersOf(Unit);
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void ViewOrder(object sender, RoutedEventArgs e) {
			Order order = (sender as Button).CommandParameter as Order;
			Frame.Navigate(new AdminViewOrderPage(Business, Frame, order));
		}
	}
}