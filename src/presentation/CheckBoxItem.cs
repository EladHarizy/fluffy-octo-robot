using System;

namespace presentation {

	internal class CheckBoxItem<T> {
		public T Object { get; }

		public bool Selected { get; set; }

		private Func<T, string> ObjToString { get; }

		public string Label {
			get => ObjToString(Object);
		}

		public CheckBoxItem(T obj) {
			Object = obj;
			ObjToString = obj => obj.ToString();
		}
	}

}