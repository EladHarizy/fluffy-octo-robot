using System;
using System.Windows.Controls;

namespace presentation {
	internal class RequiredValidator<TControl> : Validator<TControl> where TControl : Control {
		public RequiredValidator(TControl control, TextBlock error_block, Func<TControl, bool> has_value, params Func<TControl, string>[] checks) : base(control, error_block) {
			Checks.Add(c => has_value(c) ? "" : "Error: This field is required.");
			foreach (Func<TControl, string> check in checks) {
				Checks.Add(check);
			}
		}
	}
}