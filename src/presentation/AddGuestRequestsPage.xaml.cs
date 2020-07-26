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
	public partial class AddGuestRequestsPage : ValidatedPage {
		private MainWindow MainWindow { get; }

		private Frame Frame { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		private IBusiness Business { get; }

		private Session<Guest> GuestSession { get; }

		private Guest Guest {
			get => GuestSession.Person;
		}

		public ObservableCollection<GuestRequest> UiGuestRequests { get; }

		private int NumberOfAdults;

		private int NumberOfChildren;

		public AddGuestRequestsPage(IBusiness business, Frame frame, Guest guest, ObservableCollection<GuestRequest> guest_requests, Session<Guest> guest_session) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			GuestSession = guest_session;
			UiGuestRequests = guest_requests;
			DataContext = this;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Cities = new CheckBoxList<City>(Business.Cities);

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

		private void AddGuestRequest(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}

			IEnumerable<City> selected_cities = Cities.SelectedItems;
			IEnumerable<Unit.Type> selected_types = UnitTypes.SelectedItems;
			GuestRequest guest_request = new GuestRequest(
				Guest,
				((DateTime) start_date.SelectedDate).ToDate(),
				((DateTime) end_date.SelectedDate).ToDate(),
				NumberOfAdults,
				NumberOfChildren,
				message.Text,
				(selected_cities.Count() == 0 ? Business.Cities : selected_cities).ToHashSet(),
				(selected_types.Count() == 0 ? Business.UnitTypes : selected_types).ToHashSet(),
				Amenities.SelectedItems.ToHashSet()
			);
			Business.AddGuestRequest(guest_request);
			UiGuestRequests.Add(guest_request);
			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}