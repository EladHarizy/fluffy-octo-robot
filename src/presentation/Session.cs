using business;
using Lib.DataTypes;
using Lib.Entities;

namespace presentation {
	public class Session<TPerson> where TPerson : Person {
		private IBusiness Business { get; }
		public TPerson Person {
			get;
			private set;
		}

		public bool IsSignedIn {
			get => Person != null;
		}

		public Session(IBusiness business) {
			Business = business;
		}

		// If the email and password are valid, assigns the signed in person to Person. Otherwise throws InexistentEmailException or WrongPasswordException
		public void SignIn(Email email, string password) {
			Person = Business.SignIn<TPerson>(email, password);
		}
	}
}