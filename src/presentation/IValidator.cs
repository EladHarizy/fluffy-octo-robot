using System.Windows.Controls;

namespace presentation {
	public interface IValidator {
		bool Validate();

		Control BaseControl { get; }

		TextBlock ErrorBlock { get; }
	}
}