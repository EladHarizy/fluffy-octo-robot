using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.DataTypes;
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
		private void AddUnit(object sender, RoutedEventArgs e) {
			if (!Validate()) {
				return;
			}

			Unit unit = new Unit(Host, name.Text, city.SelectedValue as City, new HashSet<Amenity>(Amenities.SelectedItems), unit_type.SelectedValue as Unit.Type);
			Business.AddUnit(unit);
			UiUnits.Add(unit);

			Frame.GoBack();
		}

		private void Cancel(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}