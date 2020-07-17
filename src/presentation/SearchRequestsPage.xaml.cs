using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using business;
using Lib.Entities;

namespace presentation {
	public partial class SearchRequestsPage : Page {
		public IBusiness Business { get; }

		public Frame Frame { get; }

		public Host Host { get; }

		public CheckBoxList<City> Cities { get; }

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
			Amenities = new CheckBoxList<Amenity>(Business.Amenities /* TODO , Business.UnitsOf(Host).Select(unit => unit.UnitType).Distinct()*/ );
			filtered_guest_requests.ItemsSource = new List<GuestRequest>();
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}

		private void Search(object sender, RoutedEventArgs e) {
			// TODO
		}

		private void ViewGuestRequest(object sender, RoutedEventArgs e) {
			// TODO
		}

		private void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
			HandlePreviewMouseWheel.IgnorePreviewMouseWheel(sender, e);
		}
	}
}