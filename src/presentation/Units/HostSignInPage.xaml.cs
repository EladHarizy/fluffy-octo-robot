using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostSignInPage : Page {
		private IBusiness Business { get; }

		private Session<Host> HostSession { get; }

		private MainWindow MainWindow { get; }

		public HostSignInPage(IBusiness business, Session<Host> host_session, MainWindow main_window) {
			InitializeComponent();
			Business = business;
			HostSession = host_session;
			MainWindow = main_window;
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			HostSession.SignIn(email.Text, password.Password);
			// TODO
		}

		private void SignUp(object sender, RoutedEventArgs e) {}
	}
}