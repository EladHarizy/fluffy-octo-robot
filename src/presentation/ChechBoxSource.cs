using System.Collections.Generic;

namespace presentation {

    internal class CheckBoxSource<T> : IEnumerable<CheckBoxItem<T>> {
        IEnumerable<CheckBoxItem<T>> check_box_items;
        IEnumerable<T> SelectedItems {
            get => check_box_items.Where(item => item.Selected);
        }
    }

}