using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class EditGuestRequestPage : ValidatedPage {
		private Frame Frame { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		private IBusiness Business { get; }

		public GuestRequest GuestRequest { get; }

		ObservableCollection<GuestRequest> UiGuestRequest { get; }

		public EditGuestRequestPage(IBusiness business, Frame frame, GuestRequest guest_request, ObservableCollection<GuestRequest> ui_guest_request) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			GuestRequest = guest_request;
			UiGuestRequest = ui_guest_request;
			DataContext = this;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities, GuestRequest.DesiredAmenities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes, GuestRequest.DesiredUnitTypes);
			Cities = new CheckBoxList<City>(Business.Cities, GuestRequest.DesiredCities);

			Validators = new List<IValidator>() {
				new RequiredDateValidator(start_date, start_date_error,
						date_picker => date_picker.SelectedDate < Date.Today ? "Error: Date cannot be in the past." : ""
					),

					new RequiredDateValidator(end_date, end_date_error,
						date_picker => date_picker.SelectedDate <= start_date.SelectedDate ? "Error: End date must be after Start date." : ""
					),

					new Validator<TextBox>(number_of_adults, number_of_adults_error,
						control => control.Text == "" ? "Error: Number of adults is required." : "",
						control => int.TryParse(control.Text, out int _) ? "" : "Error: Could not interpret the input as a number."
					),

					new Validator<TextBox>(number_of_children, number_of_children_error,
						control => control.Text == "" ? "Error: Number of children is required." : "",
						control => int.TryParse(control.Text, out int _) ? "" : "Error: Could not interpret the input as a number."
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
			GuestRequest.Adults = int.Parse(number_of_adults.Text);
			GuestRequest.Children = int.Parse(number_of_children.Text);
			GuestRequest.DesiredCities = (selected_cities.Count() == 0 ? Business.Cities : selected_cities).ToHashSet();
			GuestRequest.DesiredUnitTypes = (selected_types.Count() == 0 ? Business.UnitTypes : selected_types).ToHashSet();
			GuestRequest.DesiredAmenities = Amenities.SelectedItems.ToHashSet();

			Business.EditGuestRequest(GuestRequest);
			int i = UiGuestRequest.IndexOf(GuestRequest);
			UiGuestRequest.RemoveAt(i);
			UiGuestRequest.Insert(i, GuestRequest);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}