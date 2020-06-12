using System;
using System.Runtime.Serialization;

namespace exceptions {
	public class KeyObjectMismatch : Exception {
		public KeyObjectMismatch() : base("Error: Key must match the corresponding object") {}

		public KeyObjectMismatch(string message) : base(message) {}

		public KeyObjectMismatch(string message, Exception inner) : base(message, inner) {}

		protected KeyObjectMismatch(SerializationInfo info, StreamingContext context) : base(info, context) {}
	}
}