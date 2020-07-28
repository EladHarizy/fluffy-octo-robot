using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AdminViewOrdersPage : ValidatedPage {
		public IBusiness Business { get; }

		protected Frame Frame { get; }

		public NumericalCondition<Order, Date> StartDateCondition { get; }

		public NumericalCondition<Order, Date> EndDateCondition { get; }

		public NumericalCondition<Order, int> AdultsCondition { get; }

		public NumericalCondition<Order, int> ChildrenCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public NominalCondition<Order, City> CitiesCondition { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public NominalCondition<Order, Unit.Type> UnitTypesCondition { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public AdminViewOrdersPage(IBusiness business, Frame frame) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Cities = new CheckBoxList<City>(Business.Cities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);

			StartDateCondition = new NumericalCondition<Order, Date>(
				start_date_filter_toggle,
				start_date_filter_type,
				start_date_filter_value_1,
				start_date_filter_value_2,
				order => order.StartDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			EndDateCondition = new NumericalCondition<Order, Date>(
				end_date_filter_toggle,
				end_date_filter_type,
				end_date_filter_value_1,
				end_date_filter_value_2,
				order => order.EndDate,
				control => (Date) (control as DatePicker).SelectedDate
			);

			AdultsCondition = new NumericalCondition<Order, int>(
				adults_filter_toggle,
				adults_filter_type,
				adults_filter_value_1,
				adults_filter_value_2,
				order => order.GuestRequest.Adults,
				control => int.Parse((control as TextBox).Text)
			);

			ChildrenCondition = new NumericalCondition<Order, int>(
				children_filter_toggle,
				children_filter_type,
				children_filter_value_1,
				children_filter_value_2,
				order => order.GuestRequest.Children,
				control => int.Parse((control as TextBox).Text)
			);

			CitiesCondition = new NominalCondition<Order, City>(
				cities_filter_toggle,
				cities_filter_type,
				Cities,
				order => order.Unit.City
			);

			UnitTypesCondition = new NominalCondition<Order, Unit.Type>(
				unit_types_filter_toggle,
				unit_types_filter_type,
				UnitTypes,
				order => order.Unit.UnitType
			);

			Validators = new List<IValidator>() {
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

					new RequiredCheckBoxValidator<Unit.Type>(unit_types_filter_checkboxes, unit_types_filter_checkboxes_error)
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

			Filter<Order> filter = new Filter<Order>(Business.Orders().Where(order => order.GuestRequest.Active), StartDateCondition, EndDateCondition, AdultsCondition, ChildrenCondition, CitiesCondition, UnitTypesCondition);
			filtered_orders.ItemsSource = filter.ApplyFilter();
		}

		protected virtual void ViewOrder(object sender, RoutedEventArgs e) {
			Order order = (sender as Button).CommandParameter as Order;
			Frame.Navigate(new AdminViewOrderPage(Business, Frame, order));
		}

		protected void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}