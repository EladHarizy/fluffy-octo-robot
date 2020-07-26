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
	public partial class ViewUnitsPage : ValidatedPage {
		public IBusiness Business { get; }

		protected Frame Frame { get; }

		public SingleValueCondition<Unit, int> OccupancyCondition { get; }

		public SingleValueCondition<Unit, Date> EndDateCondition { get; }

		public SingleValueCondition<Unit, int> AdultsCondition { get; }

		public SingleValueCondition<Unit, int> ChildrenCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public CollectionSimpleCondition<Unit, City> CitiesCondition { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public CollectionSimpleCondition<Unit, Unit.Type> UnitTypesCondition { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CollectionComplexCondition<Unit, Amenity> AmenitiesCondition { get; }

		public ViewUnitsPage(IBusiness business, Frame frame) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Cities = new CheckBoxList<City>(Business.Cities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);

			OccupancyCondition = new SingleValueCondition<Unit, int>(
				occupied_days_filter_toggle,
				occupied_days_filter_type,
				occupied_days_filter_value_1,
				occupied_days_filter_value_2,
				unit => unit.OccupiedDays,
				control => int.Parse((control as TextBox).Text)
			);

			EndDateCondition = new SingleValueCondition<Unit, Date>(
				end_date_filter_toggle,
				end_date_filter_type,
				end_date_filter_value_1,
				end_date_filter_value_2,
				unit => unit.EndDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			AdultsCondition = new SingleValueCondition<Unit, int>(
				adults_filter_toggle,
				adults_filter_type,
				adults_filter_value_1,
				adults_filter_value_2,
				unit => unit.Adults,
				control => int.Parse((control as TextBox).Text)
			);

			ChildrenCondition = new SingleValueCondition<Unit, int>(
				children_filter_toggle,
				children_filter_type,
				children_filter_value_1,
				children_filter_value_2,
				unit => unit.Children,
				control => int.Parse((control as TextBox).Text)
			);

			CitiesCondition = new CollectionSimpleCondition<Unit, City>(
				cities_filter_toggle,
				cities_filter_type,
				Cities,
				unit => unit.DesiredCities
			);

			UnitTypesCondition = new CollectionSimpleCondition<Unit, Unit.Type>(
				unit_types_filter_toggle,
				unit_types_filter_type,
				UnitTypes,
				unit => unit.DesiredUnitTypes
			);

			AmenitiesCondition = new CollectionComplexCondition<Unit, Amenity>(
				amenities_filter_toggle,
				amenities_filter_type,
				Amenities,
				unit => unit.DesiredAmenities
			);

			Validators = new List<IValidator>() {
				new Validator<ComboBox>(occupied_days_filter_type, occupied_days_filter_type_error, control => control.SelectedItem == null ? "Error: A filter type must be picked" : ""),

					new Validator<DatePicker>(occupied_days_filter_value_1, occupied_days_filter_value_1_error, control => control.SelectedDate == null ? "Error: A number is required here." : ""),

					new Validator<DatePicker>(occupied_days_filter_value_2, occupied_days_filter_value_2_error, control => control.SelectedDate == null ? "Error: A number is required here." : ""),

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

			Filter<Unit> filter = new Filter<Unit>(Business.Units().Where(unit => unit.Active), OccupancyCondition, EndDateCondition, AdultsCondition, ChildrenCondition, CitiesCondition, UnitTypesCondition, AmenitiesCondition);
			filtered_units.ItemsSource = filter.ApplyFilter();
		}

		protected virtual void ViewUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			Frame.Navigate(new ViewUnitPage(Business, Frame, unit));
		}

		protected void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}