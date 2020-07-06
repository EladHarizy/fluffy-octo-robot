using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class GuestRequestsPage : Page {
		private MainWindow MainWindow { get; }
		private Validator<DatePicker> StartDateValidator { get; }

		private Validator<DatePicker> EndDateValidator { get; }

		private Validator<TextBox> NumberOfAdultsValidator { get; }

		private Validator<TextBox> NumberOfChildrenValidator { get; }

		private IBusiness Business;

		private Session<Guest> GuestSession { get; }

		private Guest Guest {
			get => GuestSession.Person;
		}

		private int NumberOfAdults;

		private int NumberOfChildren;

		private IEnumerable<City> Region {
			get; // TODO: Make this return an enumerable of the checked cities
		}

		private IEnumerable<Amenity> DesiredAmenities {
			get; // TODO: Make this return an enumerable of the checked cities
		}

		private IEnumerable<Unit.Type> DesiredUnitTypes {
			get; // TODO: Make this return an enumerable of the checked cities
		}

		public GuestRequestsPage(IBusiness business, Session<Guest> guest_session) {
			InitializeComponent();
			Business = business;
			GuestSession = guest_session;

			StartDateValidator = new Validator<DatePicker>(start_date, start_date_error);
			StartDateValidator.AddCheck(
				date_picker => date_picker.SelectedDate == null ? "Error: Start date is required." : ""
			);

			EndDateValidator = new Validator<DatePicker>(end_date, end_date_error);
			EndDateValidator.AddCheck(
				date_picker => date_picker.SelectedDate == null ? "Error: End date is required." : ""
			);

			NumberOfAdultsValidator = new Validator<TextBox>(number_of_adults, number_of_adults_error);
			NumberOfAdultsValidator.AddCheck(
				control => control.Text == "" ? "Error: Number of adults is required." : ""
			);
			NumberOfAdultsValidator.AddCheck(
				control => int.TryParse(control.Text, out NumberOfAdults) ? "" : "Error: Could not interpret the input as a number."
			);
			NumberOfChildrenValidator = new Validator<TextBox>(number_of_children, number_of_children_error);
			NumberOfChildrenValidator.AddCheck(
				control => control.Text == "" ? "Error: Number of children is required." : ""
			);
			NumberOfChildrenValidator.AddCheck(
				control => int.TryParse(control.Text, out NumberOfChildren) ? "" : "Error: Could not interpret the input as a number."
			);
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

		private void EnjoyYourHoliday(object sender, RoutedEventArgs e) {
			if (!StartDateValidator.Validate() || !EndDateValidator.Validate() || !NumberOfAdultsValidator.Validate() || !NumberOfChildrenValidator.Validate()) {
				return;
			}

			try {
				Business.AddGuestRequest(new GuestRequest(
					Guest,
					((DateTime) start_date.SelectedDate).ToDate(),
					((DateTime) end_date.SelectedDate).ToDate(),
					NumberOfAdults,
					NumberOfChildren,
					Region.ToHashSet(),
					DesiredUnitTypes.ToHashSet(),
					DesiredAmenities.ToHashSet()
				));
			}
			// TODO: Catch all the errors that the try might throw instead of catching Exception
			catch (Exception) {
				return;
			}
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			GuestSession.SignOut();
		}
	}
}