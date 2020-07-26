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

		private IValidator EmailValidator { get; }

		private IValidator PasswordValidator { get; }

		public GuestSignInPage(IBusiness business, Session<Guest> guest_session, Frame frame) {
			InitializeComponent();
			Business = business;
			guest_session.SignInPage = this;
			GuestSession = guest_session;
			Frame = frame;

			EmailValidator = new EmailValidator(email, email_error);

			PasswordValidator = new PasswordValidator(password, password_error);
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