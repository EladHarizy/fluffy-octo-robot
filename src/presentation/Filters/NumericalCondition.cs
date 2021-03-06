using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Lib.Entities;

namespace presentation {
	// TObj is the type of object being filtered, and TValue is the type that these objects are being filtered by
	public partial class NumericalCondition<TObj, TValue> : ICondition<TObj> where TValue : IComparable<TValue> {
		private ToggleButton Toggle { get; }

		private ComboBox FilterTypeComboBox { get; }

		private Control Control1 { get; }

		private Control Control2 { get; }

		private Func<TObj, TValue> ObjToValue { get; }

		private Func<Control, TValue> ControlToValue { get; }

		public IEnumerable<FilterType> FilterTypes { get; }

		public NumericalCondition(ToggleButton toggle, ComboBox filter_type_combo_box, Control control_1, Control control_2, Func<TObj, TValue> obj_to_value, Func<Control, TValue> control_to_value) {
			Toggle = toggle;
			FilterTypeComboBox = filter_type_combo_box;
			Control1 = control_1;
			Control2 = control_2;
			ObjToValue = obj_to_value;
			ControlToValue = control_to_value;

			FilterTypes = new List<FilterType> {
				new FilterType("Equals", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) == 0),
				new FilterType("Less than", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) < 0),
				new FilterType("Less than or equal", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) <= 0),
				new FilterType("More than", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) > 0),
				new FilterType("More than or equal", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) >= 0),
				new FilterType("Between (exclusive)", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) > 0 && obj_val != null && obj_val.CompareTo(ControlToValue(c2)) < 0, Visibility.Visible),
				new FilterType("Between (inclusive)", (obj_val, c1, c2) => obj_val != null && obj_val.CompareTo(ControlToValue(c1)) >= 0 && obj_val != null && obj_val.CompareTo(ControlToValue(c2)) <= 0, Visibility.Visible)
			};
		}

		public bool Test(TObj obj) {
			return !(Toggle.IsChecked ?? false) || (FilterTypeComboBox.SelectedItem as FilterType).Test(ObjToValue(obj), Control1, Control2);
		}
	}
}