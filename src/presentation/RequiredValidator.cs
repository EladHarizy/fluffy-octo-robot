using System;
using System.Windows.Controls;

namespace presentation {
	internal class RequiredTextValidator : Validator<TextBox> {
		public RequiredTextValidator(TextBox control, TextBlock error_block, params Func<TextBox, string>[] checks) : base(control, error_block, checks) {
			Checks.Add(control => control.Text == "" ? "Error: This field is required." : "");
		}
	}
}