using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class EditUnitPage : Page {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		public Unit Unit { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		private Validator<TextBox> UnitNameValidator { get; }

		private Validator<ComboBox> UnitTypeValidator { get; }

		private Validator<ComboBox> CityValidator { get; }

		Collection<Unit> UiUnits { get; }

		public EditUnitPage(IBusiness business, Frame frame, Unit unit, Collection<Unit> ui_units) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Unit = unit;
			UiUnits = ui_units;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities, Unit.Amenities);
			DataContext = this;

			UnitNameValidator = new Validator<TextBox>(name, name_error);
			UnitNameValidator.AddCheck(control => control.Text == "" ? "Error: Unit name is required." : "");
			UnitNameValidator.AddCheck(control => control.Text.Length > 30 ? "Error: Unit name is too long." : "");

			UnitTypeValidator = new Validator<ComboBox>(unit_type, unit_type_error);
			UnitTypeValidator.AddCheck(control => control.SelectedValue == null ? "Error: Unit type is required." : "");

			CityValidator = new Validator<ComboBox>(city, city_error);
			CityValidator.AddCheck(control => control.SelectedValue == null ? "Error: City is required." : "");
		}

		private void EditUnit(object sender, RoutedEventArgs e) {
			bool valid = true;
			valid = UnitNameValidator.Validate() ? valid : false;
			valid = UnitTypeValidator.Validate() ? valid : false;
			valid = CityValidator.Validate() ? valid : false;

			if (!valid) {
				return;
			}

			Unit.Amenities = Amenities.SelectedItems;
			Unit.City = city.SelectedItem as City;
			Unit.Name = name.Text;
			Unit.UnitType = unit_type.SelectedItem as Unit.Type;

			Business.EditUnit(Unit);
			UiUnits.Remove(Unit);
			UiUnits.Add(Unit);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}