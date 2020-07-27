using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AdminViewUnitsPage : ValidatedPage {
		public IBusiness Business { get; }

		protected Frame Frame { get; }

		public NumericalCondition<Unit, int> OccupancyCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public NominalCondition<Unit, City> CitiesCondition { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public NominalCondition<Unit, Unit.Type> UnitTypesCondition { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public CollectionComplexCondition<Unit, Amenity> AmenitiesCondition { get; }

		public AdminViewUnitsPage(IBusiness business, Frame frame) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Cities = new CheckBoxList<City>(Business.Cities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);

			OccupancyCondition = new NumericalCondition<Unit, int>(
				occupied_days_filter_toggle,
				occupied_days_filter_type,
				occupied_days_filter_value_1,
				occupied_days_filter_value_2,
				unit => unit.OccupiedDays,
				control => int.Parse((control as TextBox).Text)
			);

			CitiesCondition = new NominalCondition<Unit, City>(
				cities_filter_toggle,
				cities_filter_type,
				Cities,
				unit => unit.City
			);

			UnitTypesCondition = new NominalCondition<Unit, Unit.Type>(
				unit_types_filter_toggle,
				unit_types_filter_type,
				UnitTypes,
				unit => unit.UnitType
			);

			AmenitiesCondition = new CollectionComplexCondition<Unit, Amenity>(
				amenities_filter_toggle,
				amenities_filter_type,
				Amenities,
				unit => unit.Amenities
			);

			Validators = new List<IValidator>() {
				new RequiredComboBoxValidator(occupied_days_filter_type, occupied_days_filter_type_error),

					new IntValidator(occupied_days_filter_value_1, occupied_days_filter_value_1_error, true, null, null),

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

			Filter<Unit> filter = new Filter<Unit>(Business.Units, OccupancyCondition, CitiesCondition, UnitTypesCondition, AmenitiesCondition);
			filtered_units.ItemsSource = filter.ApplyFilter();
		}

		protected virtual void ViewUnit(object sender, RoutedEventArgs e) {
			Unit unit = (sender as Button).CommandParameter as Unit;
			Frame.Navigate(new AdminViewUnitPage(Business, Frame, unit));
		}

		protected void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}