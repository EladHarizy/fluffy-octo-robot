using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace presentation {
	public class CollectionComplexCondition<TObj, TValue> : CollectionSimpleCondition<TObj, TValue> {
		public CollectionComplexCondition(ToggleButton toggle, ComboBox filter_type_combo_box, CheckBoxList<TValue> check_box_items, Func<TObj, IEnumerable<TValue>> obj_to_collection) : base(toggle, filter_type_combo_box, check_box_items, obj_to_collection) {
			FilterTypesCollection.Add(new FilterType("Contains only some of", (obj_vals, checkbox_list) => !obj_vals.Except(checkbox_list.SelectedItems).Any()));
		}
	}
}