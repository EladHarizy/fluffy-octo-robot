using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;
using Lib.Exceptions;

namespace presentation {
	public partial class EditOrderPage : Page {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		public Order Order { get; }

		public ObservableCollection<Unit> UiUnits { get; }

		public IEnumerable<ObservableCollection<Order>> UiOrders { get; }

		public EditOrderPage(IBusiness business, Frame frame, Order order, ObservableCollection<Unit> ui_units, IEnumerable<ObservableCollection<Order>> ui_orders) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Order = order;
			UiUnits = ui_units;
			UiOrders = ui_orders;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void ChangeStatus(object sender, RoutedEventArgs e) {
			try {
				IEnumerable<Order> affected_orders = Business.EditOrder(Order, order_status.SelectedItem as Order.Status).Where(order => order.Unit.Host.ID == Order.Unit.Host.ID);
				foreach (Order order in affected_orders) {
					foreach (ObservableCollection<Order> orders in UiOrders) {
						int i = orders.IndexOf(order);
						orders.RemoveAt(i);
						orders.Insert(i, order);
					}
				}
				if (Order.OrderStatus == "Confirmed") {
					int i = UiUnits.IndexOf(Order.Unit);
					UiUnits.RemoveAt(i);
					UiUnits.Insert(i, Order.Unit);
				}
				Frame.GoBack();
			} catch (OrderStatusChangedException error) {
				MaterialDesignThemes.Wpf.DialogHost.Show(error);
			}
		}
	}
}