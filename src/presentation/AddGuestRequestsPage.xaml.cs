using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class AddGuestRequestsPage : Page {
		private MainWindow MainWindow { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		private Validator<DatePicker> StartDateValidator { get; }

		private Validator<DatePicker> EndDateValidator { get; }

		private Validator<TextBox> NumberOfAdultsValidator { get; }

		private Validator<TextBox> NumberOfChildrenValidator { get; }

		private IBusiness Business { get; }

		private Session<Guest> GuestSession { get; }

		private Guest Guest {
			get => GuestSession.Person;
		}

		private int NumberOfAdults;

		private int NumberOfChildren;

		public AddGuestRequestsPage(IBusiness business, Session<Guest> guest_session) {
			InitializeComponent();
			Business = business;
			GuestSession = guest_session;
			DataContext = this;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Cities = new CheckBoxList<City>(Business.Cities);

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

		private void AddGuestRequest(object sender, RoutedEventArgs e) {
			bool valid = true;
			valid = StartDateValidator.Validate() ? valid : false;
			valid = EndDateValidator.Validate() ? valid : false;
			valid = NumberOfAdultsValidator.Validate() ? valid : false;
			valid = NumberOfChildrenValidator.Validate() ? valid : false;

			if (!valid) {
				return;
			}
			IEnumerable<City> selected_cities = Cities.SelectedItems;
			IEnumerable<Unit.Type> selected_types = UnitTypes.SelectedItems;
			Business.AddGuestRequest(new GuestRequest(
				Guest,
				((DateTime) start_date.SelectedDate).ToDate(),
				((DateTime) end_date.SelectedDate).ToDate(),
				NumberOfAdults,
				NumberOfChildren,
				(selected_cities.Count() == 0 ? Business.Cities : selected_cities).ToHashSet(),
				(selected_types.Count() == 0 ? Business.UnitTypes : selected_types).ToHashSet(),
				Amenities.SelectedItems.ToHashSet()
			));
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			GuestSession.SignOut();
		}
	}
}