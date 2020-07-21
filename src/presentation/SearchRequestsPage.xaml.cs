using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

		public SingleValueCondition<GuestRequest, Date> StartDateCondition { get; }

		public SingleValueCondition<GuestRequest, Date> EndDateCondition { get; }

		public SingleValueCondition<GuestRequest, int> AdultsCondition { get; }

		public SingleValueCondition<GuestRequest, int> ChildrenCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public CollectionSimpleCondition<GuestRequest, City> CitiesCondition { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public CollectionSimpleCondition<GuestRequest, Unit.Type> UnitTypesCondition { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CollectionComplexCondition<GuestRequest, Amenity> AmenitiesCondition { get; }

		// The list of orders which must be passed on to AddOrderPage, in case an order is added.
		private ObservableCollection<Order> Orders { get; }

		public SearchRequestsPage(IBusiness business, Frame frame, Host host, ObservableCollection<Order> orders) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Host = host;
			Orders = orders;
			Cities = new CheckBoxList<City>(Business.Cities, Business.UnitsOf(Host).Select(unit => unit.City).Distinct());
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes, Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct());
			Amenities = new CheckBoxList<Amenity>(Business.Amenities, Business.UnitsOf(Host).Aggregate<Unit, IEnumerable<Amenity>>(new HashSet<Amenity>(), (acc, unit) => acc.Union(unit.Amenities)));

			StartDateCondition = new SingleValueCondition<GuestRequest, Date>(
				start_date_filter_toggle,
				start_date_filter_type,
				start_date_filter_value_1,
				start_date_filter_value_2,
				guest_request => guest_request.StartDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			EndDateCondition = new SingleValueCondition<GuestRequest, Date>(
				end_date_filter_toggle,
				end_date_filter_type,
				end_date_filter_value_1,
				end_date_filter_value_2,
				guest_request => guest_request.EndDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			AdultsCondition = new SingleValueCondition<GuestRequest, int>(
				adults_filter_toggle,
				adults_filter_type,
				adults_filter_value_1,
				adults_filter_value_2,
				guest_request => guest_request.Adults,
				control => int.Parse((control as TextBox).Text)
			);

			ChildrenCondition = new SingleValueCondition<GuestRequest, int>(
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

			Validators = new List<IValidator>() {
				new Validator<ComboBox>(start_date_filter_type, start_date_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<DatePicker>(start_date_filter_value_1, start_date_filter_value_1_error, control => control.SelectedDate == null ? "Error: A date must be picked." : ""),

					new Validator<DatePicker>(start_date_filter_value_2, start_date_filter_value_2_error, control => control.SelectedDate == null ? "Error: A date must be picked." : ""),

					new Validator<ComboBox>(end_date_filter_type, end_date_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<DatePicker>(end_date_filter_value_1, end_date_filter_value_1_error, control => control.SelectedDate == null ? "Error: A date must be picked." : ""),

					new Validator<DatePicker>(end_date_filter_value_2, end_date_filter_value_2_error, control => control.SelectedDate == null ? "Error: A date must be picked." : ""),

					new Validator<ComboBox>(adults_filter_type, adults_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<TextBox>(adults_filter_value_1, adults_filter_value_1_error,
						control => control.Text == "" ? "Error: A number is required here." : "",
						control => {
							try {
								control.Text = int.Parse(control.Text).ToString();
								return "";
							} catch (FormatException) {
								return "Error: Input must be a number.";
							}
						}
					),

					new Validator<TextBox>(adults_filter_value_2, adults_filter_value_2_error,
						control => control.Text == "" ? "Error: A number is required here." : "",
						control => {
							try {
								control.Text = int.Parse(control.Text).ToString();
								return "";
							} catch (FormatException) {
								return "Error: Input must be a number.";
							}
						}
					),

					new Validator<ComboBox>(children_filter_type, children_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<TextBox>(children_filter_value_1, children_filter_value_1_error,
						control => control.Text == "" ? "Error: A number is required here." : "",
						control => {
							try {
								control.Text = int.Parse(control.Text).ToString();
								return "";
							} catch (FormatException) {
								return "Error: Input must be a number.";
							}
						}
					),

					new Validator<TextBox>(children_filter_value_2, children_filter_value_2_error,
						control => control.Text == "" ? "Error: A number is required here." : "",
						control => {
							try {
								control.Text = int.Parse(control.Text).ToString();
								return "";
							} catch (FormatException) {
								return "Error: Input must be a number.";
							}
						}
					),

					new Validator<ComboBox>(cities_filter_type, cities_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<ItemsControl>(cities_filter_checkboxes, cities_filter_checkboxes_error,
						checkbox_list => (checkbox_list.ItemsSource as CheckBoxList<City>).SelectedItems.Count() <= 0 ? "Error: Must select at least one city to use this filter." : ""
					),

					new Validator<ComboBox>(unit_types_filter_type, unit_types_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<ItemsControl>(unit_types_filter_checkboxes, unit_types_filter_checkboxes_error,
						checkbox_list => (checkbox_list.ItemsSource as CheckBoxList<Unit.Type>).SelectedItems.Count() <= 0 ? "Error: Must select at least one unit type to use this filter." : ""
					),

					new Validator<ComboBox>(amenities_filter_type, amenities_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : "")
			};

			Search();
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void Search(object sender, RoutedEventArgs e) {
			Search();
		}

		private void Search() {
			if (!Validate()) {
				return;
			}

			Filter<GuestRequest> filter = new Filter<GuestRequest>(Business.GuestRequests().Where(guest_request => guest_request.Active), StartDateCondition, EndDateCondition, AdultsCondition, ChildrenCondition, CitiesCondition, UnitTypesCondition, AmenitiesCondition);
			filtered_guest_requests.ItemsSource = filter.ApplyFilter();
		}

		private void ViewGuestRequest(object sender, RoutedEventArgs e) {
			GuestRequest guest_request = (sender as Button).CommandParameter as GuestRequest;
			Frame.Navigate(new AddOrderPage(Business, Frame, guest_request, Business.UnitsOf(Host), Orders));
		}

		private void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}