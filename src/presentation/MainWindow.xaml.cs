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
		private Session<Host> HostSession { get; }

		// Stores the guest who is signed in, if any
		private Session<Guest> GuestSession { get; }

		public MainWindow() {
			InitializeComponent();
			OriginalTitle = Title;
			Business = new Business();
			HostSession = new Session<Host>(Business);
			GuestSession = new Session<Guest>(Business);
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
			if (HostSession.IsSignedIn) {
				LoadPage(new UnitsPage(Business, HostSession.Person));
			} else {
				LoadPage(new HostSignInPage(Business, HostSession, this));
			}
		}

		private void AdminPage(object sender, RoutedEventArgs e) {
			LoadPage(new AdminPage());
		}
	}
}