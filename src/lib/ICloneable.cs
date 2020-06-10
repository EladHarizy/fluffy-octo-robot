using System;

namespace lib {

	// Our professor (Dan Erez) told us the we can implement ICloneable in each class, instead of adding a clone extension to each class
	public interface ICloneable<T> {
		T Clone();
	}
}