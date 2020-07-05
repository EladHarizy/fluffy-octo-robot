using business;
using Lib.Entities;
using Lib.Exceptions;

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
		public void SignIn(TPerson person, string password) {
			if (Business.SignIn<TPerson>(person, password)) {
				Person = person;
			} else {
				throw new WrongPasswordException();
			}
		}
	}
}