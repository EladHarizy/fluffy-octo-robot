using System;
using System.Windows.Controls;
using Lib.DataTypes;
using Lib.Exceptions;

namespace presentation {
	internal class PhoneValidator : RequiredTextValidator {
		public PhoneValidator(TextBox control, TextBlock error_block, params Func<TextBox, string>[] checks) : base(control, error_block) {
			Checks.Add(
				c => {
					try {
						c.Text = new Phone(c.Text);
						return "";
					} catch (InvalidPhoneException ex) {
						return ex.Message;
					}
				}
			);
			foreach (Func<TextBox, string> check in checks) {
				Checks.Add(check);
			}
		}
	}
}