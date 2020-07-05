namespace Lib.Exceptions {
	[System.Serializable]
	public class WrongPasswordException : System.Exception {
		public WrongPasswordException() : this("Error: Wrong password.") {}

		public WrongPasswordException(string message) : base(message) {}

		public WrongPasswordException(string message, System.Exception inner) : base(message, inner) {}

		protected WrongPasswordException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
	}
}