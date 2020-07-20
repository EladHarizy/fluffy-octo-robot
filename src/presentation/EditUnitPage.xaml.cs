using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class EditUnitPage : ValidatedPage {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		public Unit Unit { get; }

		public CheckBoxList<Amenity> Amenities { get; }

		Collection<Unit> UiUnits { get; }

		public EditUnitPage(IBusiness business, Frame frame, Unit unit, Collection<Unit> ui_units) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Unit = unit;
			UiUnits = ui_units;
			Amenities = new CheckBoxList<Amenity>(Business.Amenities, Unit.Amenities);
			DataContext = this;

			Validators = new List<IValidator>() {
				new Validator<TextBox>(name, name_error,
						control => control.Text == "" ? "Error: Unit name is required." : "",
						control => control.Text.Length > 30 ? "Error: Unit name is too long." : ""
					),

					new Validator<ComboBox>(unit_type, unit_type_error,
						control => control.SelectedValue == null ? "Error: Unit type is required." : ""
					),

					new Validator<ComboBox>(city, city_error,
						control => control.SelectedValue == null ? "Error: City is required." : ""
					)
			};
		}

		private void EditUnit(object sender, RoutedEventArgs e) {
			if (!Validate()) {
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