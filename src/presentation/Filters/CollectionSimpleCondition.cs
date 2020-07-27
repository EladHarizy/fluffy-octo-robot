using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace presentation {
	// This type of condition checks that some object (such as a GuestRequest) with a collection IEnumerable<TObj> (such as DesiredCities) matches some condition (determined by the selected FilterType). However, it can only filter by "Contains some of". (In this example the host who is searching only would be interested in knowing if the guest request contains at least one of the cities in which he has a hosting unit.)
	// TObj is the type of object being filtered, and TValue is the type that these objects are being filtered by
	public partial class CollectionSimpleCondition<TObj, TValue> : ICondition<TObj> {
		private ToggleButton Toggle { get; }

		private ComboBox FilterTypeComboBox { get; }

		private CheckBoxList<TValue> CheckBoxItems { get; }

		protected ICollection<FilterType> FilterTypesCollection { get; } = new List<FilterType>();
		public IEnumerable<FilterType> FilterTypes {
			get => FilterTypesCollection;
		}

		private Func<TObj, IEnumerable<TValue>> ObjToCollection { get; }

		public CollectionSimpleCondition(ToggleButton toggle, ComboBox filter_type_combo_box, CheckBoxList<TValue> check_box_items, Func<TObj, IEnumerable<TValue>> obj_to_collection) {
			Toggle = toggle;
			FilterTypeComboBox = filter_type_combo_box;
			ObjToCollection = obj_to_collection;
			CheckBoxItems = check_box_items;

			FilterTypesCollection.Add(new FilterType("Contains some of", (obj_vals, checkbox_list) => obj_vals.Intersect(checkbox_list.SelectedItems).Count() != 0));
		}

		public bool Test(TObj obj) {
			return !(Toggle.IsChecked ?? false) || (FilterTypeComboBox.SelectedItem as FilterType).Test(ObjToCollection(obj), CheckBoxItems);
		}
	}
}