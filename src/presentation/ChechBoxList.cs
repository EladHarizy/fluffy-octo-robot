using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace presentation {

	public class CheckBoxList<T> : IEnumerable<CheckBoxItem<T>> {
		public IEnumerable<CheckBoxItem<T>> CheckBoxItems { get; }

		public IEnumerable<T> SelectedItems {
			get => CheckBoxItems.Where(item => item.Selected).Select(item => item.Object);
		}

		public CheckBoxList(IEnumerable<T> source) : this(source, Enumerable.Empty<T>()) {}

		public CheckBoxList(IEnumerable<T> source, IEnumerable<T> selected) {
			CheckBoxItems = new HashSet<CheckBoxItem<T>>(source.Select(item => new CheckBoxItem<T>(item, selected.Contains(item))));
		}

		public void CheckAll(IEnumerable<T> items) {
			foreach (CheckBoxItem<T> item in CheckBoxItems) {
				item.Selected = item.Selected || items.Contains(item.Object);
			}
		}

		public IEnumerator<CheckBoxItem<T>> GetEnumerator() {
			return CheckBoxItems.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return CheckBoxItems.GetEnumerator();
		}
	}

}