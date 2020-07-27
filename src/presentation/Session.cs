using System;
using System.Windows.Controls;
using business;
using Lib.Entities;
using Lib.Exceptions;

namespace presentation {
	public class Session<TUser> where TUser : User {
		private IBusiness Business { get; }

		private Frame Frame { get; }

		private Page sign_in_page;
		public Page SignInPage {
			get => sign_in_page;
			set {
				if (sign_in_page == null) {
					sign_in_page = value;
				} else {
					throw new InvalidOperationException("Error: Cannot change the session's sign in page once it has been set.");
				}
			}
		}

		public TUser User {
			get;
			private set;
		}

		public bool IsSignedIn {
			get => User != null;
		}

		public Session(IBusiness business, Frame frame) {
			Business = business;
			Frame = frame;
		}

		// If the email and password are valid, assigns the signed in user to User. Otherwise throws InexistentEmailException or WrongPasswordException
		public void SignIn(TUser user, string password) {
			if (Business.SignIn<TUser>(user, password)) {
				User = user;
			} else {
				throw new WrongPasswordException();
			}
		}

		public void SignOut() {
			User = null;
			Frame.Navigate(SignInPage);
			Frame.ClearHistory();
		}
	}
}