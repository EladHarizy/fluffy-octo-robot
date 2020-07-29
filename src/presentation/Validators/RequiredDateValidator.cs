using System;
using System.Windows.Controls;

namespace presentation {
	internal class RequiredDateValidator : RequiredValidator<DatePicker> {
		public RequiredDateValidator(DatePicker control, TextBlock error_block, params Func<DatePicker, string>[] checks) : base(control, error_block, c => c.SelectedDate.HasValue, checks) {}
	}
}