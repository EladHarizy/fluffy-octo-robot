using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class AddOrderPage : Page {
		private IBusiness Business { get; }

		private Frame Frame { get; }

		public GuestRequest GuestRequest { get; }

		public IEnumerable<Unit> Units { get; }

		// If an order is added, it must be reflected in the UI
		private ObservableCollection<Order> Orders { get; }

		public AddOrderPage(IBusiness business, Frame frame, GuestRequest guest_request, IEnumerable<Unit> units, ObservableCollection<Order> orders) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			GuestRequest = guest_request;
			Units = units;
			Orders = orders;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void Send(object sender, RoutedEventArgs e) {
			Unit unit = unit_combo_box.SelectedItem as Unit;
			Order order = new Order(unit, GuestRequest, message.Text);
			Business.AddOrder(order);
			Business.EditOrder(order, "Sent mail");
			Orders.Add(order); // Update the UI Orders list
			Frame.GoBack();
		}
	}
}