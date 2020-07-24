using System.Collections.ObjectModel;
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

		public ObservableCollection<Order> UiOrders { get; }

		public EditOrderPage(IBusiness business, Frame frame, Order order, ObservableCollection<Order> ui_orders) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Order = order;
			UiOrders = ui_orders;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void ChangeStatus(object sender, RoutedEventArgs e) {
			try {
				Business.EditOrder(Order, order_status.SelectedItem as Order.Status);
				int i = UiOrders.IndexOf(Order);
				UiOrders.RemoveAt(i);
				UiOrders.Insert(i, Order);
			} catch (OrderStatusChangedException error) {
				MaterialDesignThemes.Wpf.DialogHost.Show(error);
			}
		}
	}
}