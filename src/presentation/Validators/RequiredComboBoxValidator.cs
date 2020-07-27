using System;
using System.Windows.Controls;

namespace presentation {
	internal class RequiredComboBoxValidator : RequiredValidator<ComboBox> {
		public RequiredComboBoxValidator(ComboBox control, TextBlock error_block, params Func<ComboBox, string>[] checks) : base(control, error_block, c => c.SelectedItem != null, checks) {}
	}
}