using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace presentation {
	public partial class UnorderedCondition<TObj, TValue> {
		// FilterTypes are ways to filter some collection by a certain property. For example the filter type "Equals" would filter a collection of objects which have some property of type TValue by including them iff said value is "Equal" to some specific value of type TValue.
		public class FilterType {
			// This is what would be shown in the GUI when the user selects a FilterType
			public string Label { get; }

			public Visibility ControlVisibility { get; }

			// A function which returns true if the condition succeeds
			public Func<TValue, Control, bool> Test { get; }

			public FilterType(string label, Func<TValue, Control, bool> test, Visibility control_visibility = Visibility.Visible) {
				Label = label;
				Test = test;
				ControlVisibility = control_visibility;
			}

			public override string ToString() {
				return Label;
			}
		}
	}
}