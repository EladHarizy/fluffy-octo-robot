namespace Lib.Exceptions {
	public class ObjectNotInDBException : System.Exception {
		public object obj { get; }

		public ObjectNotInDBException() {}

		public ObjectNotInDBException(object obj) {
			this.obj = obj;
		}

		public ObjectNotInDBException(string message) : base(message) {}

		public ObjectNotInDBException(object obj, string message) : base(message) {
			this.obj = obj;
		}

		public ObjectNotInDBException(string message, System.Exception inner) : base(message, inner) {}

		protected ObjectNotInDBException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
	}
}