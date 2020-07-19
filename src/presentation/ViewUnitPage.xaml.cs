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

		public ObservableCollection<Order> Orders { get; }

		public ViewUnitPage(IBusiness business, Frame frame, Unit unit) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Unit = unit;
			Orders = new ObservableCollection<Order>(Business.Orders(unit));
			DataContext = this;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void SearchRequests(object sender, RoutedEventArgs e) {
			// TODO
		}

		private void ViewOrder(object sender, RoutedEventArgs e) {
			// TODO
		}

		private void EditOrder(object sender, RoutedEventArgs e) {
			// TODO
		}

		private void DeleteOrder(object sender, RoutedEventArgs e) {
			// TODO
		}
	}
}