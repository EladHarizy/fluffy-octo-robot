using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace presentation {
	public partial class AdminSignInPage : Page {
		private IBusiness Business { get; }

		private Session<Admin> AdminSession { get; }

		private Frame Frame { get; }

		private Validator<TextBox> EmailValidator { get; }

		private Validator<PasswordBox> PasswordValidator { get; }

		public AdminSignInPage(IBusiness business, Session<Admin> admin_session, Frame frame) {
			InitializeComponent();
			Business = business;
			admin_session.SignInPage = this;
			AdminSession = admin_session;
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

			Admin admin;
			try {
				admin = Business.Admin(new Email(email.Text));
				EmailValidator.ResetError();
			} catch (InexistentEmailException error) {
				EmailValidator.SetError(error.Message);
				return;
			}

			if (!PasswordValidator.Validate()) {
				return;
			}

			try {
				AdminSession.SignIn(admin, password.Password);
				PasswordValidator.ResetError();
				Frame.Navigate(new AdminPage(Business, AdminSession));
			} catch (WrongPasswordException error) {
				PasswordValidator.SetError(error.Message);
			}
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			SignIn();
		}
	}
}