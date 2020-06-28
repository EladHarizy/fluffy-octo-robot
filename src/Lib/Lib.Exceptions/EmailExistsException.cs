using Lib.DataTypes;

public class EmailExistsException : System.Exception {
	public Email Email { get; }

	public EmailExistsException() {}

	public EmailExistsException(Email email) : this("Error: A person already exists with the email '" + email + "'.") {
		Email = email;
	}

	public EmailExistsException(string message) : base(message) {}

	public EmailExistsException(string message, System.Exception inner) : base(message, inner) {}

	protected EmailExistsException(
		System.Runtime.Serialization.SerializationInfo info,
		System.Runtime.Serialization.StreamingContext context) : base(info, context) {}
}