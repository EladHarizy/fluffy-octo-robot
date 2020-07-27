using System;
using System.Windows.Controls;
using Lib.DataTypes;
using Lib.Exceptions;

namespace presentation {
	internal class PhoneValidator : RequiredTextValidator {
		public PhoneValidator(TextBox control, TextBlock error_block, params Func<TextBox, string>[] checks) : base(control, error_block) {
			Checks.Add(
				control => {
					try {
						control.Text = new Phone(control.Text);
						return "";
					} catch (InvalidPhoneException error) {
						return error.Message;
					}
				}
			);
			foreach (Func<TextBox, string> check in checks) {
				Checks.Add(check);
			}
		}
	}
}