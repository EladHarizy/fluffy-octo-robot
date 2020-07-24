namespace business {
	public static class BusinessFactory {
		public static IBusiness New() {
			return new Business();
		}
	}
}