using System;
using System.Windows.Controls;

namespace presentation {
	internal class RequiredTextValidator : RequiredValidator<TextBox> {
		public RequiredTextValidator(TextBox control, TextBlock error_block, params Func<TextBox, string>[] checks) : base(control, error_block, c => !string.IsNullOrWhiteSpace(c.Text), checks) {}
	}
}