using System;

namespace Lib.Exceptions {
	public class InvalidTypeException : System.Exception {
		public InvalidTypeException() {}

		public InvalidTypeException(Type type) : this("Error: A generic function or class received the type " + type.FullName + ", but this type is not supported.") {}

		public InvalidTypeException(string message) : base(message) {}

		public InvalidTypeException(string message, System.Exception inner) : base(message, inner) {}

		protected InvalidTypeException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
	}
}