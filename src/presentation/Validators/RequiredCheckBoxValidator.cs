using System;
using System.Linq;
using System.Windows.Controls;

namespace presentation {
	internal class RequiredCheckBoxValidator<T> : RequiredValidator<ItemsControl> {
		public RequiredCheckBoxValidator(ItemsControl control, TextBlock error_block, params Func<ItemsControl, string>[] checks) : base(control, error_block, c => (c.ItemsSource as CheckBoxList<T>).SelectedItems.Count() > 0, checks) {}
	}
}