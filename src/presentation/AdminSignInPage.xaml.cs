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

		private EmailValidator EmailValidator { get; }

		private PasswordValidator PasswordValidator { get; }

		public AdminSignInPage(IBusiness business, Session<Admin> admin_session, Frame frame) {
			InitializeComponent();
			Business = business;
			admin_session.SignInPage = this;
			AdminSession = admin_session;
			Frame = frame;

			EmailValidator = new EmailValidator(email, email_error);

			PasswordValidator = new PasswordValidator(password, password_error);

			// Test
			email.Text = "abrahammurciano@gmail.com";
			password.Password = "password";
			// End test
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
				Frame.Navigate(new AdminPage(Business, Frame));
			} catch (WrongPasswordException error) {
				PasswordValidator.SetError(error.Message);
			}
		}

		private void SignIn(object sender, RoutedEventArgs e) {
			SignIn();
		}
	}
}