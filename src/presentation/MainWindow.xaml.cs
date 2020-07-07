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

		// Stores the host who is signed in, if any
		private Session<Host> HostSession { get; }

		// Stores the guest who is signed in, if any
		private Session<Guest> GuestSession { get; }

		public MainWindow() {
			InitializeComponent();
			OriginalTitle = Title;
			Business = new Business();

			HostSession = new Session<Host>(Business, HostPage);
			GuestSession = new Session<Guest>(Business, GuestPage);

			Page HostSignInPage = new HostSignInPage(Business, HostSession, HostPage);
			Page GuestSignInPage = new GuestSignInPage(Business, GuestSession, GuestPage);
			GuestPage.Navigate(GuestSignInPage);
			HostPage.Navigate(HostSignInPage);

			AdminPage.Navigate(new AdminPage());
		}
	}
}