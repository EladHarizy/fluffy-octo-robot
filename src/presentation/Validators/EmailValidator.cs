using System;
using System.Windows.Controls;
using Lib.DataTypes;
using Lib.Exceptions;

namespace presentation {
	internal class EmailValidator : RequiredTextValidator {
		public EmailValidator(TextBox control, TextBlock error_block, params Func<TextBox, string>[] checks) : base(control, error_block) {
			Checks.Add(
				c => {
					try {
						c.Text = new Email(c.Text);
						return "";
					} catch (InvalidEmailException ex) {
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