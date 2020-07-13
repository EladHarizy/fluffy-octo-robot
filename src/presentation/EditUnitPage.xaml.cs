using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class EditUnitPage : Page {
		public IBusiness Business { get; }

		public Frame Frame { get; }

		public Unit Unit { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		public EditUnitPage(IBusiness business, Frame frame, Unit unit) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Unit = unit;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities, Unit.Amenities);
			DataContext = this;
		}

		private void EditUnit(object sender, RoutedEventArgs e) {}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}