using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private IBusiness Business { get; }

		private string OriginalTitle { get; }

		// Stores the admin who is signed in, if any
		private Session<Admin> AdminSession { get; }

		// Stores the host who is signed in, if any
		private Session<Host> HostSession { get; }

		// Stores the guest who is signed in, if any
		private Session<Guest> GuestSession { get; }

		public MainWindow() {
			InitializeComponent();
			OriginalTitle = Title;
			Business = BusinessFactory.New();

			AdminSession = new Session<Admin>(Business, AdminPage);
			HostSession = new Session<Host>(Business, HostPage);
			GuestSession = new Session<Guest>(Business, GuestPage);

			AdminPage.Navigate(new AdminSignInPage(Business, AdminSession, AdminPage));
			HostPage.Navigate(new HostSignInPage(Business, HostSession, HostPage));
			GuestPage.Navigate(new GuestSignInPage(Business, GuestSession, GuestPage));
		}
	}
}