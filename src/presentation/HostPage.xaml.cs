using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostPage : Page {
		private IBusiness Business { get; }

		private Session<Host> HostSession { get; }

		private Frame Frame { get; }

		public Host Host {
			get => HostSession.Person;
		}

		public ObservableCollection<Unit> Units { get; }

		public ObservableCollection<Order> Orders { get; }

		public HostPage(IBusiness business, Session<Host> host_session, Frame frame) {
			InitializeComponent();
			Business = business;
			HostSession = host_session;
			Frame = frame;
			Units = new ObservableCollection<Unit>(Business.UnitsOf(Host));
			Orders = new ObservableCollection<Order>(Business.Orders(Host));
			DataContext = this;
		}

		private void SignOut(object sender, RoutedEventArgs e) {
			HostSession.SignOut();
		}

		private void NewHostingUnit(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AddUnitPage(Business, Frame, Host, Units));
		}

		private void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			if (!e.Handled) {
				e.Handled = true;
				var eventArg = new MouseWheelEventArgs(
					e.MouseDevice, e.Timestamp, e.Delta);
				eventArg.RoutedEvent = UIElement.MouseWheelEvent;
				eventArg.Source = sender;
				var parent = ((Control) sender).Parent as UIElement;
				parent.RaiseEvent(eventArg);
			}
		}

		private void ViewUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			Frame.Navigate(new ViewUnitPage(Business, Frame, unit));
		}

		private void EditUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			Frame.Navigate(new EditUnitPage(Business, Frame, unit, Units));
		}

		private async void DeleteUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			if ((bool) await MaterialDesignThemes.Wpf.DialogHost.Show(unit)) {
				Business.DeleteUnit(unit);
				Units.Remove(unit);
			}
		}

		private void SearchRequests(object sender, RoutedEventArgs e) {
			Frame.Navigate(new SearchRequestsPage(Business, Frame, Host));
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