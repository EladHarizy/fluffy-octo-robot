using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using business;
using Lib.DataTypes;
using Lib.Entities;
using Lib.Exceptions;

namespace presentation {
	public partial class AddHostPage : Page {
		private IBusiness Business { get; }

		private MainWindow MainWindow { get; }

		private HostSignInPage HostSignInPage { get; }

		private BankBranch BankBranch { get; set; }

		private Brush InitialBorderBrush { get; }

		public AddHostPage(IBusiness business, MainWindow main_window, HostSignInPage host_sign_in_page) {
			InitializeComponent();
			Business = business;
			MainWindow = main_window;
			HostSignInPage = host_sign_in_page;
			InitialBorderBrush = first_name.BorderBrush;
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}
			try {
				Business.AddHost(new Host(first_name.Text, last_name.Text, email.Text, phone.Text, password.Password, BankBranch, account_number.Text));
			} catch (EmailExistsException) {
				SetError(email, email_error, "Error: A host already exists with this email. Try signing in instead.");
			}
			HostSignInPage.email.Text = email.Text;
			HostSignInPage.password.Password = password.Password;
			HostSignInPage.SignIn();
		}

		private bool Validate() {
			bool valid = true;
			valid = ValidateFirstName() ? valid : false;
			valid = ValidateLastName() ? valid : false;
			valid = ValidateEmail() ? valid : false;
			valid = ValidatePhone() ? valid : false;
			valid = ValidatePassword() ? valid : false;
			valid = ValidateRepeatPassword() ? valid : false;
			valid = ValidateBankNumber() ? valid : false;
			valid = ValidateBranchNumber() ? valid : false;
			valid = ValidateAccountNumber() ? valid : false;
			return valid;
		}

		private bool ValidateFirstName() {
			if (first_name.Text == "") {
				SetError(first_name, first_name_error, "Error: First name is required.");
			} else if (char.IsLower(first_name.Text[0])) {
				SetError(first_name, first_name_error, "Error: First name must start with an uppercase letter.");
			} else if (!Regex.Match(first_name.Text, @"^[a-z ,.'-]+$", RegexOptions.IgnoreCase).Success) {
				SetError(first_name, first_name_error, "Error: Cannot have these symbols in your name.");
			} else {
				ResetError(first_name, first_name_error);
				return true;
			}
			return false;
		}

		private bool ValidateLastName() {
			if (last_name.Text == "") {
				SetError(last_name, last_name_error, "Error: Last name is required.");
			} else if (char.IsLower(last_name.Text[0])) {
				SetError(last_name, last_name_error, "Error: Last name must start with an uppercase letter.");
			} else if (!Regex.Match(last_name.Text, @"^[a-z ,.'-]+$", RegexOptions.IgnoreCase).Success) {
				SetError(last_name, last_name_error, "Error: Cannot have these symbols in your name.");
			} else {
				ResetError(last_name, last_name_error);
				return true;
			}
			return false;
		}

		private bool ValidateEmail() {
			if (email.Text == "") {
				SetError(email, email_error, "Error: Email is required.");
			} else {
				try {
					email.Text = new Email(email.Text);
					ResetError(email, email_error);
					return true;
				} catch (InvalidEmailException error) {
					SetError(email, email_error, error.Message);
				}
			}
			return false;
		}

		private bool ValidatePhone() {
			if (phone.Text == "") {
				SetError(phone, phone_error, "Error: Phone is required.");
			} else {
				try {
					phone.Text = new Phone(phone.Text);
					ResetError(phone, phone_error);
					return true;
				} catch (InvalidPhoneException error) {
					SetError(phone, phone_error, error.Message);
				}
			}
			return false;
		}

		private bool ValidatePassword() {
			if (password.Password == "") {
				SetError(password, password_error, "Error: Password is required.");
			} else {
				try {
					password.Password = new Password(password.Password);
					ResetError(password, password_error);
					return true;
				} catch (InvalidPasswordException error) {
					SetError(password, password_error, error.Message);
				}
			}
			return false;
		}

		private bool ValidateRepeatPassword() {
			if (repeat_password.Password != password.Password) {
				SetError(repeat_password, repeat_password_error, "Error: Passwords do not match.");
			} else {
				ResetError(repeat_password, repeat_password_error);
				return true;
			}
			return false;
		}

		private bool ValidateBankNumber() {
			try {
				bank_number.Text = new ID(bank_number.Text, 2);
				Business.BankBranches().First(branch => branch.BankID == bank_number.Text);
				ResetError(bank_number, bank_number_error);
				return true;
			} catch (IncorrectDigitsException) {
				SetError(bank_number, bank_number_error, "Error: Bank number must be at most two digits.");
			} catch (FormatException) {
				SetError(bank_number, bank_number_error, "Error: Could not parse the input as a bank number.");
			} catch (InvalidOperationException) {
				SetError(bank_number, bank_number_error, "Error: No bank with this number was found.");
			}
			return false;
		}

		private bool ValidateBranchNumber() {
			try {
				branch_number.Text = new ID(branch_number.Text, 3);
				BankBranch = Business.BankBranches().First(branch => branch.BankID == bank_number.Text && branch.BranchID == branch_number.Text);
				ResetError(branch_number, branch_number_error);
				return true;
			} catch (IncorrectDigitsException) {
				SetError(branch_number, branch_number_error, "Error: Branch number must be at most three digits.");
			} catch (FormatException) {
				SetError(branch_number, branch_number_error, "Error: Could not parse the input as a branch number.");
			} catch (InvalidOperationException) {
				SetError(branch_number, branch_number_error, "Error: No branch with this number was found for bank " + bank_number.Text + '.');
			}
			return false;
		}

		private bool ValidateAccountNumber() {
			try {
				account_number.Text = new ID(account_number.Text, 6);
				ResetError(account_number, account_number_error);
				return true;
			} catch (IncorrectDigitsException) {
				SetError(account_number, account_number_error, "Error: Account number must be six digits.");
			} catch (FormatException) {
				SetError(account_number, account_number_error, "Error: Could not parse the input as an account number.");
			}
			return false;
		}

		private void SetError(Control control, TextBlock text_block, string message) {
			control.BorderBrush = Brushes.Red;
			text_block.Text = message;
		}

		private void ResetError(Control control, TextBlock text_block) {
			control.BorderBrush = InitialBorderBrush;
			text_block.Text = "";
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			MainWindow.LoadPage(HostSignInPage);
		}
	}
}