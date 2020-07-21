using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace presentation {
	public partial class GuestSignInPage : Page {
		private IBusiness Business { get; }

		private Session<Guest> GuestSession { get; }

		private Frame Frame { get; }

		private Validator<TextBox> EmailValidator { get; }

		private Validator<PasswordBox> PasswordValidator { get; }

		public GuestSignInPage(IBusiness business, Session<Guest> guest_session, Frame frame) {
			InitializeComponent();
			Business = business;
			guest_session.SignInPage = this;
			GuestSession = guest_session;
			Frame = frame;

			// Test
			email.Text = "abrahammurciano@gmail.com";
			password.Password = "password";
			// End test

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
			PasswordValidator.AddCheck(
				control => {
					try {
						control.Password = new Password(control.Password);
						return "";
					} catch (InvalidPasswordException) {
						return "Error: Wrong password.";
					}
				}
			);
		}

		public void SignIn() {
			if (!EmailValidator.Validate()) {
				return;
			}

			Guest guest;
			try {
				guest = Business.Guest(new Email(email.Text));
				EmailValidator.ResetError();
			} catch (InexistentEmailException error) {
				EmailValidator.SetError(error.Message);
				return;
			}

			if (!PasswordValidator.Validate()) {
				return;
			}

			try {
				GuestSession.SignIn(guest, password.Password);
				PasswordValidator.ResetError();
				Frame.Navigate(new AddGuestRequestsPage(Business, GuestSession));
			} catch (WrongPasswordException error) {
				PasswordValidator.SetError(error.Message);
			}
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			SignIn();
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			Frame.Navigate(new AddGuestPage(Business, Frame, this));
		}
	}
}