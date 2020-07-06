namespace presentation {

    internal class CheckBoxItem<T> {
        string Name {
            get => obj.ToString();
        }
        T obj;
        bool Selected;
    }

}