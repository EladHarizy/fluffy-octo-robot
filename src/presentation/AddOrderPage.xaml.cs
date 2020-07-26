using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Mail;
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

		// If a unit is added, it must be reflected in the UI
		private ObservableCollection<Unit> UiUnits { get; }

		// If an order is added, it must be reflected in the UI
		private ObservableCollection<Order> UiOrders { get; }

		public AddOrderPage(IBusiness business, Frame frame, GuestRequest guest_request, IEnumerable<Unit> units, ObservableCollection<Unit> ui_units, ObservableCollection<Order> ui_orders) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			GuestRequest = guest_request;
			Units = units;
			UiUnits = ui_units;
			UiOrders = ui_orders;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void Send(object sender, RoutedEventArgs e) {
			Unit unit = unit_combo_box.SelectedItem as Unit;
			Order order = new Order(unit, GuestRequest, message.Text);
			Business.AddOrder(order);
			Business.EditOrder(order, "Sent email");
			UiOrders.Add(order); // Update the UI Orders list
			Frame.Navigate(new EditOrderPage(Business, Frame, order, UiUnits, new List<ObservableCollection<Order>> { UiOrders }));
		}
	}
}