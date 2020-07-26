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

		private Frame Frame { get; }

		private Validator<TextBox> EmailValidator { get; }

		private Validator<PasswordBox> PasswordValidator { get; }

		public HostSignInPage(IBusiness business, Session<Host> host_session, Frame frame) {
			InitializeComponent();
			Business = business;
			host_session.SignInPage = this;
			HostSession = host_session;
			Frame = frame;

			EmailValidator = new Validator<TextBox>(email, email_error);
			// Check that the email has a valid format
			EmailValidator.AddCheck(
				control => {
					try {
						control.Text = new Email(control.Text);
						return "";
					} catch (InvalidEmailException error) {
						return error.Message;
					}
				}
			);

			PasswordValidator = new Validator<PasswordBox>(password, password_error);
		}

		public void SignIn() {
			if (!EmailValidator.Validate() || !PasswordValidator.Validate()) {
				return;
			}

			try {
				Host host = Business.Host(new Email(email.Text));
				EmailValidator.ResetError();

				HostSession.SignIn(host, password.Password);
				PasswordValidator.ResetError();

				Frame.Navigate(new HostPage(Business, HostSession, Frame));
			} catch (InexistentEmailException error) {
				EmailValidator.SetError(error.Message);
			} catch (WrongPasswordException error) {
				PasswordValidator.SetError(error.Message);
			}
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			SignIn();
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AddHostPage(Business, Frame, this));
		}
	}
}