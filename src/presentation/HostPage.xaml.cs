using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostPage : Page {
		private IBusiness Business;

		private Session<Host> HostSession { get; }

		public Frame Frame { get; }

		private Host Host {
			get => HostSession.Person;
		}

		private IEnumerable<Unit> Units { get => Business.UnitsOf(Host); }

		public HostPage(IBusiness business, Session<Host> host_session, Frame frame) {
			InitializeComponent();
			Business = business;
			HostSession = host_session;
			Frame = frame;
			units_details_card.DataContext = Units;
			host_details_card.DataContext = Host;
		}

		private void SignOut(object sender, RoutedEventArgs e) {
			HostSession.SignOut();
		}

		private void NewHostingUnit(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AddHostingUnitPage(Business, Frame, Host));
		}

		private void EditUnit(object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			Unit unit = button.CommandParameter as Unit;
			MessageBox.Show("Editing " + unit.ID);
		}

		private void DeleteUnit(object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			Unit unit = button.CommandParameter as Unit;
			MessageBox.Show("Deleting " + unit.ID);
		}
	}
}