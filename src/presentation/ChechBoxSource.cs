using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace presentation {

	internal class CheckBoxSource<T> : IEnumerable<CheckBoxItem<T>> {
		private IEnumerable<CheckBoxItem<T>> CheckBoxItems { get; }
		public IEnumerable<T> SelectedItems {
			get => CheckBoxItems.Where(item => item.Selected).Select(item => item.Object);
		}

		public IEnumerator<CheckBoxItem<T>> GetEnumerator() {
			return CheckBoxItems.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return CheckBoxItems.GetEnumerator();
		}
	}

}