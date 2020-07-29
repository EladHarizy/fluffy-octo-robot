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
				new RequiredTextValidator(name, name_error,
						control => control.Text.Length > 30 ? "Error: Unit name is too long." : ""
					),

					new RequiredComboBoxValidator(unit_type, unit_type_error),

					new RequiredComboBoxValidator(city, city_error)
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
			int i = UiUnits.IndexOf(Unit);
			UiUnits.RemoveAt(i);
			UiUnits.Insert(i, Unit);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}