using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace presentation {
	public partial class AddGuestPage : Page {
		private IBusiness Business { get; }

		private Frame Frame { get; }

		private GuestSignInPage GuestSignInPage { get; }

		private BankBranch BankBranch { get; set; }

		private Validator<TextBox> FirstNameValidator { get; }

		private Validator<TextBox> LastNameValidator { get; }

		private Validator<TextBox> EmailValidator { get; }

		private Validator<TextBox> PhoneValidator { get; }

		private Validator<PasswordBox> PasswordValidator { get; }

		private Validator<PasswordBox> RepeatPasswordValidator { get; }

		public AddGuestPage(IBusiness business, Frame frame, GuestSignInPage Guest_sign_in_page) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			GuestSignInPage = Guest_sign_in_page;

			FirstNameValidator = new Validator<TextBox>(
				first_name,
				first_name_error,
				control => control.Text == "" ? "Error: First name is required." : "",
				control => Regex.Match(control.Text, @"^[a-z ,.'-]+$", RegexOptions.IgnoreCase).Success ? "" : "Error: Cannot have these symbols in your name."
			);

			LastNameValidator = new Validator<TextBox>(
				last_name,
				last_name_error,
				control => control.Text == "" ? "Error: First name is required." : "",
				control => Regex.Match(control.Text, @"^[a-z ,.'-]+$", RegexOptions.IgnoreCase).Success ? "" : "Error: Cannot have these symbols in your name."
			);

			EmailValidator = new Validator<TextBox>(
				email,
				email_error,
				control => control.Text == "" ? "Error: Email is required." : "",
				control => {
					try {
						control.Text = new Email(control.Text);
						return "";
					} catch (InvalidEmailException error) {
						return error.Message;
					}
				}
			);

			PhoneValidator = new Validator<TextBox>(
				phone,
				phone_error,
				new Func<TextBox, string>(
					control => control.Text == "" ? "Error: Phone number is required." : ""
				),
				new Func<TextBox, string>(
					control => {
						try {
							control.Text = new Phone(control.Text);
							return "";
						} catch (InvalidPhoneException error) {
							return error.Message;
						}
					}
				)
			);

			PasswordValidator = new Validator<PasswordBox>(
				password,
				password_error,
				control => control.Password == "" ? "Error: Password is required." : "",
				control => {
					try {
						control.Password = new Password(control.Password);
						return "";
					} catch (InvalidPasswordException error) {
						return error.Message;
					}
				}
			);

			RepeatPasswordValidator = new Validator<PasswordBox>(
				repeat_password,
				repeat_password_error,
				control => control.Password != password.Password ? "Error: Passwords do not match." : ""
			);
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			first_name.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(first_name.Text.ToLower());
			last_name.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(last_name.Text.ToLower());

			bool valid = true;
			valid = FirstNameValidator.Validate() ? valid : false;
			valid = LastNameValidator.Validate() ? valid : false;
			valid = EmailValidator.Validate() ? valid : false;
			valid = PhoneValidator.Validate() ? valid : false;
			valid = PasswordValidator.Validate() ? valid : false;
			valid = RepeatPasswordValidator.Validate() ? valid : false;

			if (!valid) {
				return;
			}

			try {
				Business.AddGuest(new Guest(first_name.Text, last_name.Text, email.Text, phone.Text, password.Password));
				EmailValidator.ResetError();

				GuestSignInPage.email.Text = email.Text;
				GuestSignInPage.password.Password = password.Password;
				Frame.GoBack();
				GuestSignInPage.SignIn();
			} catch (EmailExistsException) {
				EmailValidator.SetError("Error: A Guest already exists with this email. Try signing in instead.");
			}
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}