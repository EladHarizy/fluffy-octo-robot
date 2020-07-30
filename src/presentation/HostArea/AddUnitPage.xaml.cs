using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AddUnitPage : ValidatedPage {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		private Host Host { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		ICollection<Unit> UiUnits { get; }

		public AddUnitPage(IBusiness business, Frame frame, Host host, ICollection<Unit> ui_units) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Host = host;
			DataContext = this;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);
			UiUnits = ui_units;

			Validators = new List<IValidator>() {
				new RequiredTextValidator(name, name_error,
						control => control.Text.Length > 30 ? "Error: Unit name is too long." : ""
					),

					new RequiredComboBoxValidator(unit_type, unit_type_error),

					new RequiredComboBoxValidator(city, city_error)
			};
		}
		private void AddUnit(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}

			Unit unit = new Unit(Host, name.Text, description.Text, city.SelectedValue as City, new HashSet<Amenity>(Amenities.SelectedItems), unit_type.SelectedValue as Unit.Type);
			Business.AddUnit(unit);
			UiUnits.Add(unit);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}