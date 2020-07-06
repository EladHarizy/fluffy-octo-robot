namespace presentation {

	internal class CheckBoxItem<T> {
		public T Object { get; }

		public bool Selected { get; set; }

		public string Name {
			get => Object.ToString();
		}
	}

}