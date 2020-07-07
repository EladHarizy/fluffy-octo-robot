using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace presentation {

	internal class CheckBoxList<T> : IEnumerable<CheckBoxItem<T>> {
		public IEnumerable<CheckBoxItem<T>> CheckBoxItems { get; }

		public IEnumerable<T> SelectedItems {
			get => CheckBoxItems.Where(item => item.Selected).Select(item => item.Object);
		}

		public CheckBoxList(IEnumerable<T> source) {
			CheckBoxItems = new HashSet<CheckBoxItem<T>>(source.Select(item => new CheckBoxItem<T>(item)));
		}

		public IEnumerator<CheckBoxItem<T>> GetEnumerator() {
			return CheckBoxItems.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return CheckBoxItems.GetEnumerator();
		}
	}

}