using System;
using System.Windows.Controls;

namespace presentation {
	internal class IntValidator : Validator<TextBox> {
		public IntValidator(TextBox control, TextBlock error_block, params Func<TextBox, string>[] checks) : base(control, error_block, checks) {
			Checks.Add(
				control => {
					try {
						control.Text = string.IsNullOrWhiteSpace(control.Text) ? "" : int.Parse(control.Text).ToString();
						return "";
					} catch (FormatException) {
						return "Error: Input must be a number.";
					} catch (OverflowException) {
						return "Error: Number is too large.";
					}
				}
			);
		}

		public IntValidator(TextBox control, TextBlock error_block, bool required, int? min, int? max, params Func<TextBox, string>[] checks) : this(control, error_block, checks) {
			if (required) {
				Checks.Add(c => c.Text == "" ? "Error: This field is required." : "");
			}
			if (min != null) {
				Checks.Add(c => int.Parse(c.Text) < (int) min ? "Error: Number must be at least " + (int) min + '.' : "");
			}
			if (max != null) {
				Checks.Add(c => int.Parse(c.Text) > (int) max ? "Error: Number must be at most " + (int) max + '.' : "");
			}
		}
	}
}