using System;
using System.Collections.Generic;
using System.Windows;

namespace presentation {
	public partial class CollectionSimpleCondition<TObj, TValue> {
		// FilterTypes are ways to filter some collection by a certain property. For example the filter type "Contains some of" would filter a collection of objects which have some collection of type IEnumerable<TValue> by including them iff said collection contains some of the elements in some specific collection of type IEnumerable<TValue>.
		public class FilterType {
			// This is what would be shown in the GUI when the user selects a FilterType
			public string Label { get; }

			// A function which returns true if the condition succeeds
			public Func<IEnumerable<TValue>, CheckBoxList<TValue>, bool> Test { get; }

			public Visibility CheckBoxListVisibility { get; }

			public FilterType(string label, Func<IEnumerable<TValue>, CheckBoxList<TValue>, bool> test, Visibility checkbox_list_visibility = Visibility.Visible) {
				Label = label;
				Test = test;
				CheckBoxListVisibility = checkbox_list_visibility;
			}

			public override string ToString() {
				return Label;
			}
		}
	}
}