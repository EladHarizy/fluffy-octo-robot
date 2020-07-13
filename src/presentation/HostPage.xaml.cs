using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostPage : Page {
		private IBusiness Business { get; }

		private Session<Host> HostSession { get; }

		public Frame Frame { get; }

		private Host Host {
			get => HostSession.Person;
		}

		private ObservableCollection<Unit> Units { get; }

		public HostPage(IBusiness business, Session<Host> host_session, Frame frame) {
			InitializeComponent();
			Business = business;
			HostSession = host_session;
			Frame = frame;
			Units = new ObservableCollection<Unit>(Business.UnitsOf(Host));
			units_details_card.DataContext = Units;
			host_details_card.DataContext = Host;
		}

		private void SignOut(object sender, RoutedEventArgs e) {
			HostSession.SignOut();
		}

		private void NewHostingUnit(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AddUnitPage(Business, Frame, Host, Units));
		}

		private void EditUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			Frame.Navigate(new EditUnitPage(Business, Frame, unit));
		}

		private async void DeleteUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			if ((bool) await MaterialDesignThemes.Wpf.DialogHost.Show(unit)) {
				Business.DeleteUnit(unit);
				Units.Remove(unit);
			}
		}
	}
}