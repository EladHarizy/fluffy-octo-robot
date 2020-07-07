using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AddHostingUnitPage : Page {
		private IBusiness Business { get; }
		private Frame Frame { get; }

		private CheckBoxList<Amenity> Amenities { get; }

		public AddHostingUnitPage(IBusiness business, Frame frame) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			form.DataContext = Business;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);
			amenities_checkboxes.DataContext = Amenities;
		}
		private void AddHostingUnit(object sender, RoutedEventArgs e) {

		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}