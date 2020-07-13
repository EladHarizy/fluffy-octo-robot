using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public partial class AddUnitPage : Page {
		private IBusiness Business { get; }

		private Frame Frame { get; }

		private Host Host { get; }

		private CheckBoxList<Amenity> Amenities { get; }

		private Validator<TextBox> UnitNameValidator { get; }

		private Validator<ComboBox> UnitTypeValidator { get; }

		private Validator<ComboBox> CityValidator { get; }

		ICollection<Unit> UiUnits { get; }

		public AddUnitPage(IBusiness business, Frame frame, Host host, ICollection<Unit> ui_units) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Host = host;
			form.DataContext = Business;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities);
			amenities_checkboxes.DataContext = Amenities;
			UiUnits = ui_units;

			UnitNameValidator = new Validator<TextBox>(unit_name, unit_name_error);
			UnitNameValidator.AddCheck(control => control.Text == "" ? "Error: Unit name is required." : "");
			UnitNameValidator.AddCheck(control => control.Text.Length > 30 ? "Error: Unit name is too long." : "");

			UnitTypeValidator = new Validator<ComboBox>(unit_type, unit_type_error);
			UnitTypeValidator.AddCheck(control => control.SelectedValue == null ? "Error: Unit type is required." : "");

			CityValidator = new Validator<ComboBox>(city, city_error);
			CityValidator.AddCheck(control => control.SelectedValue == null ? "Error: City is required." : "");
		}
		private void AddUnit(object sender, RoutedEventArgs e) {
			bool valid = true;
			valid = UnitNameValidator.Validate() ? valid : false;
			valid = UnitTypeValidator.Validate() ? valid : false;
			valid = CityValidator.Validate() ? valid : false;

			if (!valid) {
				return;
			}

			Unit unit = new Unit(Host, unit_name.Text, city.SelectedValue as City, Amenities.SelectedItems.ToHashSet(), unit_type.SelectedValue as Unit.Type);
			Business.AddUnit(unit);
			UiUnits.Add(unit);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}