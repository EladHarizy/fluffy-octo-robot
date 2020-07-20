using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.Entities;

namespace presentation {
	public partial class SearchRequestsPage : ValidatedPage {
		public IBusiness Business { get; }

		public Frame Frame { get; }

		public Host Host { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		private Filter<GuestRequest> Filter { get; set; }

		public SingleValueCondition<GuestRequest, Date> StartDateCondition { get; }

		public SearchRequestsPage(IBusiness business, Frame frame, Host host) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Host = host;
			Cities = new CheckBoxList<City>(Business.Cities, Business.UnitsOf(Host).Select(unit => unit.City).Distinct());
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes, Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct());
			Amenities = new CheckBoxList<Amenity>(Business.Amenities /* TODO: autoselect some amenities based on units he owns , Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct()*/ );
			filtered_guest_requests.ItemsSource = new List<GuestRequest>();

			StartDateCondition = new SingleValueCondition<GuestRequest, Date>(
				start_date_filter_toggle,
				start_date_filter_type,
				start_date_filter_value_1,
				start_date_filter_value_2,
				guest_request => guest_request.StartDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			Validators = new List<IValidator>() {
				new Validator<ComboBox>(start_date_filter_type, start_date_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<DatePicker>(start_date_filter_value_1, start_date_filter_value_1_error, control => control.SelectedDate == null ? "Error: A date must be picked." : ""),

					new Validator<DatePicker>(start_date_filter_value_2, start_date_filter_value_2_error, control => control.SelectedDate == null ? "Error: A date must be picked." : "")
			};
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void Search(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}

			Filter = new Filter<GuestRequest>(Business.GuestRequests(), StartDateCondition /*, EndDateCondition, AdultsCondition, ChildrenCondition, CityCondition, UnitTypeCondition, AmenitiesCondition*/ );
			filtered_guest_requests.ItemsSource = Filter.ApplyFilter();
		}

		private void ViewGuestRequest(object sender, RoutedEventArgs e) {
			// TODO
		}

		private void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}