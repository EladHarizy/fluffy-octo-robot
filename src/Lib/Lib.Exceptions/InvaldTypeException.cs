using System;

namespace Lib.Exceptions {
	public class InvaldTypeException : System.Exception {
		public InvaldTypeException() {}

		public InvaldTypeException(Type type) : this("Error: A generic function or class received the type " + type.FullName + ", but this type is not supported.") {}

		public InvaldTypeException(string message) : base(message) {}

		public InvaldTypeException(string message, System.Exception inner) : base(message, inner) {}

		protected InvaldTypeException(
			System.Runtime.Serialization.SerializationInfo info,
			System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
	}
}