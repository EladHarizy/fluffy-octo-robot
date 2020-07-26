using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class EditGuestRequestPage : ValidatedPage {
		private MainWindow MainWindow { get; }

		private Frame Frame { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		private IBusiness Business { get; }

		private Guest Guest { get; }

		private int NumberOfAdults;

		private int NumberOfChildren;

		public GuestRequest GuestRequest { get; }

		Collection<GuestRequest> UiGuestRequest { get; }

		public EditGuestRequestPage(IBusiness business, Frame frame, Guest guest) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Guest = guest;
			DataContext = this;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities, GuestRequest.DesiredAmenities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes, GuestRequest.DesiredUnitTypes);
			Cities = new CheckBoxList<City>(Business.Cities, GuestRequest.Region);

			Validators = new List<IValidator>() {
				new Validator<DatePicker>(start_date, start_date_error,
						date_picker => date_picker.SelectedDate == null ? "Error: Start date is required." : ""
					),

					new Validator<DatePicker>(end_date, end_date_error,
						date_picker => date_picker.SelectedDate == null ? "Error: End date is required." : ""
					),

					new Validator<TextBox>(number_of_adults, number_of_adults_error,
						control => control.Text == "" ? "Error: Number of adults is required." : "",
						control => int.TryParse(control.Text, out NumberOfAdults) ? "" : "Error: Could not interpret the input as a number."
					),

					new Validator<TextBox>(number_of_children, number_of_children_error,
						control => control.Text == "" ? "Error: Number of children is required." : "",
						control => int.TryParse(control.Text, out NumberOfChildren) ? "" : "Error: Could not interpret the input as a number."
					)
			};
		}

		private void EditGuestRequest(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}

			IEnumerable<City> selected_cities = Cities.SelectedItems;
			IEnumerable<Unit.Type> selected_types = UnitTypes.SelectedItems;
			GuestRequest.StartDate = ((DateTime) start_date.SelectedDate).ToDate();
			GuestRequest.EndDate = ((DateTime) end_date.SelectedDate).ToDate();
			GuestRequest.Region = (selected_cities.Count() == 0 ? Business.Cities : selected_cities).ToHashSet();
			GuestRequest.DesiredUnitTypes = (selected_types.Count() == 0 ? Business.UnitTypes : selected_types).ToHashSet();
			GuestRequest.DesiredAmenities = Amenities.SelectedItems.ToHashSet();

			Business.EditGuestRequest(GuestRequest);
			UiGuestRequest.Remove(GuestRequest);
			UiGuestRequest.Add(GuestRequest);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}