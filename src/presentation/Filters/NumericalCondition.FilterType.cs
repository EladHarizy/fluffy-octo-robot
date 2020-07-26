using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace presentation {
	public partial class NumericalCondition<TObj, TValue> {
		// FilterTypes are ways to filter some collection by a certain property. For example the filter type "Equals" would filter a collection of objects which have some property of type TValue by including them iff said value is "Equal" to some specific value of type TValue.
		public class FilterType {
			// This is what would be shown in the GUI when the user selects a FilterType
			public string Label { get; }

			// A function which returns true if the condition succeeds
			public Func<TValue, Control, Control, bool> Test { get; }

			public Visibility Control1Visibility { get; }

			public Visibility Control2Visibility { get; }

			public FilterType(string label, Func<TValue, Control, Control, bool> test, Visibility control_2_visibility = Visibility.Collapsed, Visibility control_1_visibility = Visibility.Visible) {
				Label = label;
				Test = test;
				Control2Visibility = control_2_visibility;
				Control1Visibility = control_1_visibility;
			}

			public override string ToString() {
				return Label;
			}
		}
	}
}