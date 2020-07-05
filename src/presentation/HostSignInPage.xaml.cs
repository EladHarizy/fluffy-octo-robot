using System;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;
using Lib.Exceptions;

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

		public void SignIn() {
			try {
				HostSession.SignIn(email.Text, password.Password);
				MainWindow.LoadPage(new UnitsPage(Business, HostSession.Person));
			} catch (Exception error) when(error is InexistentEmailException || error is InvalidEmailException) {
				email.BorderBrush = System.Windows.Media.Brushes.Red;
				email_error.Text = error.Message;
				email_error.Height = Double.NaN;
			} catch (WrongPasswordException error) {
				password.BorderBrush = System.Windows.Media.Brushes.Red;
				password_error.Text = error.Message;
				password_error.Height = Double.NaN;
			}
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			SignIn();
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			MainWindow.LoadPage(new AddHostPage(Business, MainWindow, this));
		}
	}
}