using System;
using System.Collections.Generic;
using System.Globalization;
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

		public AddHostPage(IBusiness business, MainWindow main_window, HostSignInPage host_sign_in_page) {
			InitializeComponent();
			Business = business;
			MainWindow = main_window;
			HostSignInPage = host_sign_in_page;
		}

		private void SignUp(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}
			Validator<TextBox> validator = new Validator<TextBox>(
				email,
				email_error,
				new List<Func<TextBox, string>> {
					Control => {
						try {
							Business.AddHost(new Host(first_name.Text, last_name.Text, email.Text, phone.Text, password.Password, BankBranch, account_number.Text));
							return "";
						} catch (EmailExistsException) {
							return "Error: A host already exists with this email. Try signing in instead.";
						}
					}
				}
			);
			validator.Validate();
			HostSignInPage.email.Text = email.Text;
			HostSignInPage.password.Password = password.Password;
			MainWindow.Back();
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
			first_name.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(first_name.Text.ToLower());
			Validator<TextBox> validator = new Validator<TextBox>(
				first_name,
				first_name_error,
				new List<Func<TextBox, string>> {
					control => control.Text == "" ? "Error: First name is required." : "",
					control => Regex.Match(control.Text, @"^[a-z ,.'-]+$", RegexOptions.IgnoreCase).Success ? "" : "Error: Cannot have these symbols in your name."
				}
			);
			return validator.Validate();
		}

		private bool ValidateLastName() {
			last_name.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(last_name.Text.ToLower());
			Validator<TextBox> validator = new Validator<TextBox>(
				last_name,
				last_name_error,
				new List<Func<TextBox, string>> {
					control => control.Text == "" ? "Error: First name is required." : "",
					control => Regex.Match(control.Text, @"^[a-z ,.'-]+$", RegexOptions.IgnoreCase).Success ? "" : "Error: Cannot have these symbols in your name."
				}
			);
			return validator.Validate();
		}

		private bool ValidateEmail() {
			Validator<TextBox> validator = new Validator<TextBox>(
				email,
				email_error,
				new List<Func<TextBox, string>> {
					control => control.Text == "" ? "Error: Email is required." : "",
					control => {
						try {
							email.Text = new Email(email.Text);
							return "";
						} catch (InvalidEmailException error) {
							return error.Message;
						}
					}
				}
			);
			return validator.Validate();
		}

		private bool ValidatePhone() {
			Validator<TextBox> validator = new Validator<TextBox>(
				phone,
				phone_error,
				new List<Func<TextBox, string>> {
					new Func<TextBox, string>(
						control => control.Text == "" ? "Error: Phone number is required." : ""
					),
					new Func<TextBox, string>(
						control => {
							try {
								phone.Text = new Phone(phone.Text);
								return "";
							} catch (InvalidPhoneException error) {
								return error.Message;
							}
						}
					)
				}
			);
			return validator.Validate();
		}

		private bool ValidatePassword() {
			Validator<PasswordBox> validator = new Validator<PasswordBox>(
				password,
				password_error,
				new List<Func<PasswordBox, string>> {
					control => control.Password == "" ? "Error: Password is required." : "",
					control => {
						try {
							control.Password = new Password(control.Password);
							return "";
						} catch (InvalidPasswordException error) {
							return error.Message;
						}
					}
				}
			);
			return validator.Validate();
		}

		private bool ValidateRepeatPassword() {
			Validator<PasswordBox> validator = new Validator<PasswordBox>(
				repeat_password,
				repeat_password_error,
				new List<Func<PasswordBox, string>> {
					control => control.Password != password.Password ? "Error: Passwords do not match." : ""
				}
			);
			return validator.Validate();
		}

		private bool ValidateBankNumber() {
			Validator<TextBox> validator = new Validator<TextBox>(
				bank_number,
				bank_number_error,
				new List<Func<TextBox, string>> {
					control => {
						try {
							control.Text = new ID(control.Text, 2);
							Business.BankBranches().First(branch => branch.BankID == bank_number.Text);
							return "";
						} catch (IncorrectDigitsException) {
							return "Error: Bank number must be at most two digits.";
						} catch (FormatException) {
							return "Error: Could not parse the input as a bank number.";
						} catch (InvalidOperationException) {
							return "Error: No bank with this number was found.";
						}
					}
				}
			);
			return validator.Validate();
		}

		private bool ValidateBranchNumber() {
			Validator<TextBox> validator = new Validator<TextBox>(
				branch_number,
				branch_number_error,
				new List<Func<TextBox, string>> {
					control => {
						try {
							control.Text = new ID(control.Text, 3);
							BankBranch = Business.BankBranches().First(branch => branch.BankID == bank_number.Text && branch.BranchID == control.Text);
							return "";
						} catch (IncorrectDigitsException) {
							return "Error: Branch number must be at most three digits.";
						} catch (FormatException) {
							return "Error: Could not parse the input as a branch number.";
						} catch (InvalidOperationException) {
							return "Error: No branch with this number was found for bank " + bank_number.Text + '.';
						}
					}
				}
			);
			return validator.Validate();
		}

		private bool ValidateAccountNumber() {
			Validator<TextBox> validator = new Validator<TextBox>(
				account_number,
				account_number_error,
				new List<Func<TextBox, string>> {
					control => {
						try {
							control.Text = new ID(control.Text, 6);
							return "";
						} catch (IncorrectDigitsException) {
							return "Error: Account number must be six digits.";
						} catch (FormatException) {
							return "Error: Could not parse the input as an account number.";
						}
					}
				}
			);
			return validator.Validate();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			MainWindow.Back();
		}
	}
}