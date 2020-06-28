using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private IBusiness Business { get; }

		private string OriginalTitle { get; }

		// Stores the host who is signed in, if any
		private Session<Host> host_session;

		// Stores the guest who is signed in, if any
		private Session<Guest> guest_session;

		public MainWindow() {
			InitializeComponent();
			OriginalTitle = Title;
			Business = new Business();
			host_session = new Session<Host>(Business);
			guest_session = new Session<Guest>(Business);
			LoadPage(new HomePage());
		}

		public void LoadPage(Page page) {
			Page.Content = page;
			SetTitle(page.Title);
		}

		private void SetTitle(string title) {
			Title = OriginalTitle + " | " + title;
		}

		private void GuestRequestsPage(object sender, RoutedEventArgs e) {
			LoadPage(new GuestRequestsPage(Business));
		}

		private void UnitsPage(object sender, RoutedEventArgs e) {
			if (host_session.IsSignedIn) {
				LoadPage(new UnitsPage(Business, host_session.Person));
			} else {
				LoadPage(new HostSignInPage(Business, host_session, this));
			}
		}

		private void AdminPage(object sender, RoutedEventArgs e) {
			LoadPage(new AdminPage());
		}
	}
}