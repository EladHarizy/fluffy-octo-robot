namespace Lib.Exceptions {
	[System.Serializable]
	public class InvalidPasswordException : System.Exception {
		public InvalidPasswordException() : this("Error: Invalid password.") {}

		public InvalidPasswordException(string message) : base(message) {}

		public InvalidPasswordException(string message, System.Exception inner) : base(message, inner) {}

		protected InvalidPasswordException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
	}
}