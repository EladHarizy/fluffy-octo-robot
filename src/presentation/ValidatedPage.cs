using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace presentation {
	public abstract class ValidatedPage : Page {
		protected IEnumerable<IValidator> Validators {
			get;
			set;
		}

		protected bool Validate() {
			bool valid = true;
			foreach (IValidator validator in Validators) {
				if (validator.BaseControl.Visibility == Visibility.Visible && !validator.Validate()) {
					valid = false;
				}
			}
			return valid;
		}
	}
}