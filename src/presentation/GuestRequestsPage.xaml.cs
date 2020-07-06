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
	public partial class GuestRequestsPage : Page {
		private MainWindow MainWindow { get; }
		private Validator<TextBox> StartDateValidator { get; }

		private Validator<TextBox> EndDateValidator { get; }

		private Validator<TextBox> NumberOfAdultsValidator { get; }

		private Validator<TextBox> NumberOfChildrenValidator { get; }

		private IBusiness business;

		private Guest Guest { get; }

		public GuestRequestsPage(IBusiness business, Guest guest) {
			InitializeComponent();
			this.business = business;
			Guest = guest;
		}

		private void FormSubmit() {
			// Guest guest = business.Guest();
			// GuestRequest guest_request = new GuestRequest(guest, start_date, end_date, adults, children, region, desired_unit_types, desired_amenities);
		}

		private void HandleCheck(object sender, RoutedEventArgs e) {
			text1.Text = "The CheckBox is checked.";
		}

		private void HandleUnchecked(object sender, RoutedEventArgs e) {
			text1.Text = "The CheckBox is unchecked.";
		}

		private void HandleThirdState(object sender, RoutedEventArgs e) {
			text1.Text = "The CheckBox is in the indeterminate state.";
		}

		public void AddGuestRequest(Date start_date) {}

		private void EnjoyYourHoliday(object sender, RoutedEventArgs e) {
			first_name.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(first_name.Text.ToLower());
			last_name.Text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(last_name.Text.ToLower());

			if (!StartDateValidator.Validate() || !EndDateValidator.Validate() || !NumberOfAdultsValidator.Validate() || !NumberOfChildrenValidator.Validate()) {
				return;
			}

			try {
				Business.AddGuestRequest(new Guest(start_date.Text, end_date.Text, number_of_adults.Text, number_of_children.Text, password.Password, BankBranch, account_number.Text));
				EmailValidator.ResetError();

				HostSignInPage.email.Text = email.Text;
				HostSignInPage.password.Password = password.Password;
				MainWindow.Back();
				HostSignInPage.SignIn();
			} catch (EmailExistsException) {
				EmailValidator.SetError("Error: A host already exists with this email. Try signing in instead.");
			}
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			MainWindow.Back();
		}
	}
}