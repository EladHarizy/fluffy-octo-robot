using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class AdminViewOrdersPage : ValidatedPage {
		public IBusiness Business { get; }

		protected Frame Frame { get; }

		protected Filter<Order> Filter { get; }

		public UnorderedCondition<Order, ID> IDCondition { get; }

		public UnorderedCondition<Order, ID> GuestRequestIDCondition { get; }

		public UnorderedCondition<Order, Email> GuestEmailCondition { get; }

		public UnorderedCondition<Order, ID> UnitIDCondition { get; }

		public UnorderedCondition<Order, Email> HostEmailCondition { get; }

		public NumericalCondition<Order, Date> StartDateCondition { get; }

		public NumericalCondition<Order, Date> EndDateCondition { get; }

		public NumericalCondition<Order, int> AdultsCondition { get; }

		public NumericalCondition<Order, int> ChildrenCondition { get; }

		public NominalCondition<Order, City> CitiesCondition { get; }

		public NominalCondition<Order, Unit.Type> UnitTypesCondition { get; }

		public CheckBoxList<City> Cities { get; }

		public CheckBoxList<Unit.Type> UnitTypes { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public AdminViewOrdersPage(IBusiness business, Frame frame) {
			InitializeComponent();
			DataContext = this;
			Business = business;
			Frame = frame;
			Cities = new CheckBoxList<City>(Business.Cities);
			UnitTypes = new CheckBoxList<Unit.Type>(Business.UnitTypes);
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);

			IDCondition = new UnorderedCondition<Order, ID>(
				id_filter_toggle,
				id_filter_type,
				id_filter_value,
				order => order.ID,
				control => (control as TextBox).Text
			);

			GuestRequestIDCondition = new UnorderedCondition<Order, ID>(
				guest_request_id_filter_toggle,
				guest_request_id_filter_type,
				guest_request_id_filter_value,
				order => order.GuestRequest.ID,
				control => (control as TextBox).Text
			);

			GuestEmailCondition = new UnorderedCondition<Order, Email>(
				guest_email_filter_toggle,
				guest_email_filter_type,
				guest_email_filter_value,
				order => order.GuestRequest.Guest.Email,
				control => (control as TextBox).Text
			);

			UnitIDCondition = new UnorderedCondition<Order, ID>(
				unit_id_filter_toggle,
				unit_id_filter_type,
				unit_id_filter_value,
				order => order.Unit.ID,
				control => (control as TextBox).Text
			);

			HostEmailCondition = new UnorderedCondition<Order, Email>(
				host_email_filter_toggle,
				host_email_filter_type,
				host_email_filter_value,
				order => order.Unit.Host.Email,
				control => (control as TextBox).Text
			);

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

			Filter = new Filter<Order>(IDCondition, GuestRequestIDCondition, GuestEmailCondition, UnitIDCondition, HostEmailCondition, StartDateCondition, EndDateCondition, AdultsCondition, ChildrenCondition, CitiesCondition, UnitTypesCondition);

			Validators = new List<IValidator>() {
				new RequiredComboBoxValidator(id_filter_type, id_filter_type_error),

					new IDValidator(id_filter_value, id_filter_value_error, 8),

					new RequiredComboBoxValidator(guest_request_id_filter_type, guest_request_id_filter_type_error),

					new IDValidator(guest_request_id_filter_value, guest_request_id_filter_value_error, 8),

					new RequiredComboBoxValidator(guest_email_filter_type, guest_email_filter_type_error),

					new EmailValidator(guest_email_filter_value, guest_email_filter_value_error),

					new RequiredComboBoxValidator(unit_id_filter_type, unit_id_filter_type_error),

					new IDValidator(unit_id_filter_value, unit_id_filter_value_error, 8),

					new RequiredComboBoxValidator(host_email_filter_type, host_email_filter_type_error),

					new EmailValidator(host_email_filter_value, host_email_filter_value_error),

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
			filtered_orders.ItemsSource = Filter.ApplyFilter(Business.Orders);
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