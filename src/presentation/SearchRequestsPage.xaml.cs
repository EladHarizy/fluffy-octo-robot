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
	public partial class SearchRequestsPage : ViewGuestRequestsPage {
		public Host Host { get; }

		// The list of units which must be passed on to AddOrderPage, in case a unit has an order confirmed.
		private ObservableCollection<Unit> UiUnits { get; }

		// The list of orders which must be passed on to AddOrderPage, in case an order is added.
		private ObservableCollection<Order> UiOrders { get; }

		public SearchRequestsPage(IBusiness business, Frame frame, Host host, ObservableCollection<Unit> ui_units, ObservableCollection<Order> ui_orders) : base(business, frame) {
			InitializeComponent();
			DataContext = this;
			Host = host;
			UiUnits = ui_units;
			UiOrders = ui_orders;
			Cities.CheckAll(Business.UnitsOf(Host).Select(unit => unit.City).Distinct());
			UnitTypes.CheckAll(Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct());
			Amenities.CheckAll(Business.UnitsOf(Host).Aggregate<Unit, IEnumerable<Amenity>>(new HashSet<Amenity>(), (acc, unit) => acc.Union(unit.Amenities)));
		}

		protected override void ViewGuestRequest(object sender, RoutedEventArgs e) {
			GuestRequest guest_request = (sender as Button).CommandParameter as GuestRequest;
			Frame.Navigate(new AddOrderPage(Business, Frame, guest_request, Business.UnitsOf(Host), UiUnits, UiOrders));
		}
	}
}