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

		private Filter<GuestRequest> Filter { get; set; }

		public SingleValueCondition<GuestRequest, Date> StartDateCondition { get; }

		public SingleValueCondition<GuestRequest, Date> EndDateCondition { get; }

		public SingleValueCondition<GuestRequest, int> AdultsCondition { get; }

		public SingleValueCondition<GuestRequest, int> ChildrenCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public CollectionSimpleCondition<GuestRequest, City> CitiesCondition { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public SearchRequestsPage(IBusiness business, Frame frame, Host host) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Host = host;
			Cities = new CheckBoxList<City>(Business.Cities, Business.UnitsOf(Host).Select(unit => unit.City).Distinct());
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes, Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct());
			Amenities = new CheckBoxList<Amenity>(Business.Amenities /* TODO: autoselect some amenities based on units he owns , Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct()*/ );

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
				city_filter_toggle,
				cities_filter_type,
				Cities,
				guest_request => guest_request.Region
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
						checkbox_list => (checkbox_list.ItemsSource as CheckBoxList<City>).SelectedItems.Count() <= 0 ? "Error: Must select at least one city." : ""
					)
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

			Filter = new Filter<GuestRequest>(Business.GuestRequests(), StartDateCondition, EndDateCondition, AdultsCondition, ChildrenCondition, CitiesCondition /*, UnitTypeCondition, AmenitiesCondition*/ );
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