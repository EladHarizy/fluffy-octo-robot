using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace presentation {
	// TObj is the type of object being filtered, and TValue is the type that these objects are being filtered by
	public partial class NominalCondition<TObj, TValue> : ICondition<TObj> {
		private ToggleButton Toggle { get; }

		private ComboBox FilterTypeComboBox { get; }

		private CheckBoxList<TValue> CheckBoxes { get; }

		private Func<TObj, TValue> ObjToValue { get; }

		public IEnumerable<FilterType> FilterTypes { get; }

		public NominalCondition(ToggleButton toggle, ComboBox filter_type_combo_box, CheckBoxList<TValue> checkboxes, Func<TObj, TValue> obj_to_value) {
			Toggle = toggle;
			FilterTypeComboBox = filter_type_combo_box;
			CheckBoxes = checkboxes;
			ObjToValue = obj_to_value;

			FilterTypes = new List<FilterType> {
				new FilterType("Is one of", (obj_val, checkbox_items) => checkbox_items.SelectedItems.Contains(obj_val))
			};
		}

		public bool Test(TObj obj) {
			return !(Toggle.IsChecked ?? false) || (FilterTypeComboBox.SelectedItem as FilterType).Test(ObjToValue(obj), CheckBoxes);
		}
	}
}