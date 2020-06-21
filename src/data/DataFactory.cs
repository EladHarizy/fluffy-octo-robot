namespace data {
	public static class DataFactory {
		internal static IData Data = new Data();

		public static IData New() {
			return new Data();
		}
	}
}