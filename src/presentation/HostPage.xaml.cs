using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostPage : Page {
		private IBusiness Business;

		private Session<Host> HostSession { get; }

		private Host Host {
			get => HostSession.Person;
		}

		private IEnumerable<Unit> Units { get; }

		public HostPage(IBusiness business, Session<Host> host_session) {
			InitializeComponent();
			Business = business;
			HostSession = host_session;
			Units = Business.UnitsOf(Host);
			units_details_card.DataContext = Units;
			host_details_card.DataContext = Host;
		}

		private void SignOut(object sender, RoutedEventArgs e) {
			HostSession.SignOut();
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