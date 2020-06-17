namespace Lib.Exceptions {
	public class ObjectInDBException : System.Exception {
		public object obj { get; }

		public ObjectInDBException() {}

		public ObjectInDBException(object obj) {
			this.obj = obj;
		}

		public ObjectInDBException(string message) : base(message) {}

		public ObjectInDBException(object obj, string message) : base(message) {
			this.obj = obj;
		}

		public ObjectInDBException(string message, System.Exception inner) : base(message, inner) {}

		protected ObjectInDBException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
	}
}