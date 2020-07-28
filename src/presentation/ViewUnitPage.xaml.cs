using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class ViewUnitPage : Page {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		public Unit Unit { get; }

		private ObservableCollection<Unit> UiUnits { get; }

		public ObservableCollection<Order> UiOrders { get; }

		private ObservableCollection<Order> HostPageUiOrders { get; }

		public ViewUnitPage(IBusiness business, Frame frame, Unit unit, ObservableCollection<Unit> ui_units, ObservableCollection<Order> host_page_ui_orders) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Unit = unit;
			UiOrders = new ObservableCollection<Order>(Business.OrdersOf(unit));
			DataContext = this;
			UiUnits = ui_units;
			HostPageUiOrders = host_page_ui_orders;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void EditOrder(object sender, RoutedEventArgs e) {
			Order order = (sender as Button).CommandParameter as Order;
			Frame.Navigate(new EditOrderPage(Business, Frame, order, UiUnits, new List<ObservableCollection<Order>> { UiOrders, HostPageUiOrders }));
		}

		private void DeleteOrder(object sender, RoutedEventArgs e) {
			Order order = (sender as Button).CommandParameter as Order;
			Business.DeleteOrder(order);
			UiOrders.Remove(order);
			HostPageUiOrders.Remove(order);
		}
	}
}