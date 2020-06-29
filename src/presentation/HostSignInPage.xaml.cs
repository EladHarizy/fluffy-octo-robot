using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
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
			Host host = null;

			Validator<TextBox> email_validator = new Validator<TextBox>(
				email,
				email_error,
				control => {
					try {
						control.Text = new Email(control.Text);
						return "";
					} catch (InvalidEmailException error) {
						return error.Message;
					}
				},
				control => {
					try {
						host = Business.Host(new Email(control.Text));
						return "";
					} catch (InexistentEmailException error) {
						return error.Message;
					}
				}
			);
			if (!email_validator.Validate()) {
				return;
			}

			Validator<PasswordBox> password_validator = new Validator<PasswordBox>(
				password,
				password_error,
				control => {
					try {
						HostSession.SignIn(host, password.Password);
						return "";
					} catch (InvalidPasswordException error) {
						return error.Message;
					}
				}
			);
			if (!password_validator.Validate()) {
				return;
			}
			MainWindow.LoadPage(new UnitsPage(Business, HostSession.Person));
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			SignIn();
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			MainWindow.LoadPage(new AddHostPage(Business, MainWindow, this));
		}
	}
}