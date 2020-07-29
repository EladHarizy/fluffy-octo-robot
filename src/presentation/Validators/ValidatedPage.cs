using System.Collections.Generic;
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
				if (validator.BaseControl.IsVisible && !validator.Validate()) {
					valid = false;
				}
			}
			return valid;
		}
	}
}