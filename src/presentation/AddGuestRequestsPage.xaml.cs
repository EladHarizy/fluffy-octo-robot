using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class AddGuestRequestsPage : ValidatedPage {
		private MainWindow MainWindow { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		private IBusiness Business { get; }

		private Session<Guest> GuestSession { get; }

		private Guest Guest {
			get => GuestSession.User;
		}

		public AddGuestRequestsPage(IBusiness business, Session<Guest> guest_session) {
			InitializeComponent();
			Business = business;
			GuestSession = guest_session;
			DataContext = this;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Cities = new CheckBoxList<City>(Business.Cities);

			Validators = new List<IValidator>() {
				new RequiredDateValidator(start_date, start_date_error),
					new RequiredDateValidator(end_date, end_date_error),
					new IntValidator(number_of_adults, number_of_adults_error, true, 1, null),
					new IntValidator(number_of_adults, number_of_children_error, true, 0, null),
			};

		}

		private void AddGuestRequest(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}

			IEnumerable<City> selected_cities = Cities.SelectedItems;
			IEnumerable<Unit.Type> selected_types = UnitTypes.SelectedItems;
			Business.AddGuestRequest(new GuestRequest(
				Guest,
				((DateTime) start_date.SelectedDate).ToDate(),
				((DateTime) end_date.SelectedDate).ToDate(),
				int.Parse(number_of_adults.Text),
				int.Parse(number_of_children.Text),
				message.Text,
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