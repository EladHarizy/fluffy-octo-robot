using System;

namespace presentation {

	public class CheckBoxItem<T> {
		public T Object { get; }

		public bool Selected { get; set; }

		public CheckBoxItem(T obj, bool selected = false) {
			Object = obj;
			Selected = selected;
		}
	}

}