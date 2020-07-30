using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class AdminViewGuestRequestsPage : ValidatedPage {
		public IBusiness Business { get; }

		protected Frame Frame { get; }

		protected Filter<GuestRequest> Filter { get; }

		public UnorderedCondition<GuestRequest, ID> IDCondition { get; }

		public NumericalCondition<GuestRequest, Date> StartDateCondition { get; }

		public NumericalCondition<GuestRequest, Date> EndDateCondition { get; }

		public NumericalCondition<GuestRequest, int> AdultsCondition { get; }

		public NumericalCondition<GuestRequest, int> ChildrenCondition { get; }

		public CollectionSimpleCondition<GuestRequest, City> CitiesCondition { get; }

		public CollectionSimpleCondition<GuestRequest, Unit.Type> UnitTypesCondition { get; }

		public CollectionComplexCondition<GuestRequest, Amenity> AmenitiesCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public AdminViewGuestRequestsPage(IBusiness business, Frame frame) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Cities = new CheckBoxList<City>(Business.Cities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);

			IDCondition = new UnorderedCondition<GuestRequest, ID>(
				id_filter_toggle,
				id_filter_type,
				id_filter_value,
				guest_request => guest_request.ID,
				control => (control as TextBox).Text
			);

			StartDateCondition = new NumericalCondition<GuestRequest, Date>(
				start_date_filter_toggle,
				start_date_filter_type,
				start_date_filter_value_1,
				start_date_filter_value_2,
				guest_request => guest_request.StartDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			EndDateCondition = new NumericalCondition<GuestRequest, Date>(
				end_date_filter_toggle,
				end_date_filter_type,
				end_date_filter_value_1,
				end_date_filter_value_2,
				guest_request => guest_request.EndDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			AdultsCondition = new NumericalCondition<GuestRequest, int>(
				adults_filter_toggle,
				adults_filter_type,
				adults_filter_value_1,
				adults_filter_value_2,
				guest_request => guest_request.Adults,
				control => int.Parse((control as TextBox).Text)
			);

			ChildrenCondition = new NumericalCondition<GuestRequest, int>(
				children_filter_toggle,
				children_filter_type,
				children_filter_value_1,
				children_filter_value_2,
				guest_request => guest_request.Children,
				control => int.Parse((control as TextBox).Text)
			);

			CitiesCondition = new CollectionSimpleCondition<GuestRequest, City>(
				cities_filter_toggle,
				cities_filter_type,
				Cities,
				guest_request => guest_request.DesiredCities
			);

			UnitTypesCondition = new CollectionSimpleCondition<GuestRequest, Unit.Type>(
				unit_types_filter_toggle,
				unit_types_filter_type,
				UnitTypes,
				guest_request => guest_request.DesiredUnitTypes
			);

			AmenitiesCondition = new CollectionComplexCondition<GuestRequest, Amenity>(
				amenities_filter_toggle,
				amenities_filter_type,
				Amenities,
				guest_request => guest_request.DesiredAmenities
			);

			Filter = new Filter<GuestRequest>(IDCondition, StartDateCondition, EndDateCondition, AdultsCondition, ChildrenCondition, CitiesCondition, UnitTypesCondition, AmenitiesCondition);

			Validators = new List<IValidator>() {
				new RequiredComboBoxValidator(id_filter_type, id_filter_type_error),

					new IDValidator(id_filter_value, id_filter_value_error, 8),

					new RequiredComboBoxValidator(start_date_filter_type, start_date_filter_type_error),

					new RequiredDateValidator(start_date_filter_value_1, start_date_filter_value_1_error),

					new RequiredDateValidator(start_date_filter_value_2, start_date_filter_value_2_error),

					new RequiredComboBoxValidator(end_date_filter_type, end_date_filter_type_error),

					new RequiredDateValidator(end_date_filter_value_1, end_date_filter_value_1_error),

					new RequiredDateValidator(end_date_filter_value_2, end_date_filter_value_2_error),

					new RequiredComboBoxValidator(adults_filter_type, adults_filter_type_error),

					new IntValidator(adults_filter_value_1, adults_filter_value_1_error, true, null, null),

					new IntValidator(adults_filter_value_2, adults_filter_value_2_error, true, null, null),

					new RequiredComboBoxValidator(children_filter_type, children_filter_type_error),

					new IntValidator(children_filter_value_1, children_filter_value_1_error, true, null, null),

					new IntValidator(children_filter_value_2, children_filter_value_2_error, true, null, null),

					new RequiredComboBoxValidator(cities_filter_type, cities_filter_type_error),

					new RequiredCheckBoxValidator<City>(cities_filter_checkboxes, cities_filter_checkboxes_error),

					new RequiredComboBoxValidator(unit_types_filter_type, unit_types_filter_type_error),

					new RequiredCheckBoxValidator<Unit.Type>(unit_types_filter_checkboxes, unit_types_filter_checkboxes_error),

					new RequiredComboBoxValidator(amenities_filter_type, amenities_filter_type_error)
			};

			Search();
		}

		protected void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		protected void Search(object sender, RoutedEventArgs e) {
			Search();
		}

		protected void Search() {
			if (!Validate()) {
				return;
			}
			filtered_guest_requests.ItemsSource = Filter.ApplyFilter(Business.GuestRequests().Where(guest_request => guest_request.Active));
		}

		protected virtual void ViewGuestRequest(object sender, RoutedEventArgs e) {
			GuestRequest guest_request = (sender as Button).CommandParameter as GuestRequest;
			Frame.Navigate(new AdminViewGuestRequestPage(Business, Frame, guest_request));
		}

		protected void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}