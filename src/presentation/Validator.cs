using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace presentation {
	internal class Validator<TControl> where TControl : Control {
		private TControl Control { get; }

		private Brush OriginalBrush { get; }

		private TextBlock ErrorBlock { get; }

		private IEnumerable<Func<TControl, string>> Checks { get; }

		public Validator(TControl control, TextBlock error_block, IEnumerable<Func<TControl, string>> checks) {
			Control = control;
			OriginalBrush = control.BorderBrush;
			ErrorBlock = error_block;
			Checks = checks;
		}

		public bool Validate() {
			foreach (Func<TControl, string> check in Checks) {
				string result = check(Control);
				if (result != "") {
					SetError(result);
					return false;
				}
			}
			ResetError();
			return true;
		}

		private void SetError(string message) {
			Control.BorderBrush = Brushes.Red;
			ErrorBlock.Text = message;
		}

		private void ResetError() {
			Control.BorderBrush = OriginalBrush;
			ErrorBlock.Text = "";
		}
	}
}