namespace lib {
	class Guest {
		private static IDGenerator id_generator = new IDGenerator(8);

		public ID ID {
			get;
			private set;
		}

		public Guest() {
			ID = id_generator.Next();
		}
	}
}