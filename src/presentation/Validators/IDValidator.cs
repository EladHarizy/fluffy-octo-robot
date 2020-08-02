using System;
using System.Windows.Controls;
using Lib.DataTypes;
using Lib.Exceptions;

namespace presentation {
	internal class IDValidator : RequiredTextValidator {
		private int Digits { get; }
		public IDValidator(TextBox control, TextBlock error_block, int digits, params Func<TextBox, string>[] checks) : base(control, error_block) {
			Digits = digits;
			Checks.Add(
				c => {
					try {
						c.Text = new ID(c.Text, Digits);
						return "";
					} catch (FormatException) {
						return "Error: ID must contain only digits.";
					} catch (Exception ex) when(ex is IncorrectDigitsException || ex is OverflowException) {
						return "Error: ID must have precisely " + Digits + " digits.";
					}
				}
			);
			foreach (Func<TextBox, string> check in checks) {
				Checks.Add(check);
			}
		}
	}
}