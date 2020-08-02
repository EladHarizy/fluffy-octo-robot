using System;
using System.Windows.Controls;
using Lib.DataTypes;
using Lib.Exceptions;

namespace presentation {
	internal class PasswordValidator : RequiredValidator<PasswordBox> {
		public PasswordValidator(PasswordBox control, TextBlock error_block, params Func<PasswordBox, string>[] checks) : base(control, error_block, p => !string.IsNullOrEmpty(p.Password)) {
			Checks.Add(
				c => {
					try {
						c.Password = new Password(c.Password);
						return "";
					} catch (InvalidPasswordException ex) {
						return ex.Message;
					}
				}
			);
			foreach (Func<PasswordBox, string> check in checks) {
				Checks.Add(check);
			}
		}
	}
}