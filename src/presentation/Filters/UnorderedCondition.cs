using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace presentation {
	public partial class UnorderedCondition<TObj, TValue> : ICondition<TObj> {
		private ToggleButton Toggle { get; }

		private ComboBox FilterTypeComboBox { get; }

		private Control Control { get; }

		private Func<TObj, TValue> ObjToValue { get; }

		private Func<Control, TValue> ControlToValue { get; }

		public IEnumerable<FilterType> FilterTypes { get; }

		public UnorderedCondition(ToggleButton toggle, ComboBox filter_type_combo_box, Control control, Func<TObj, TValue> obj_to_value, Func<Control, TValue> control_to_value) {
			Toggle = toggle;
			FilterTypeComboBox = filter_type_combo_box;
			Control = control;
			ObjToValue = obj_to_value;
			ControlToValue = control_to_value;

			FilterTypes = new List<FilterType> {
				new FilterType("Equals", (obj_val, control) => obj_val.Equals(ControlToValue(control)))
			};
		}

		public bool Test(TObj obj) {
			return !(Toggle.IsChecked ?? false) || (FilterTypeComboBox.SelectedItem as FilterType).Test(ObjToValue(obj), Control);
		}
	}
}