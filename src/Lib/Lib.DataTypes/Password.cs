using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Lib.Exceptions;

namespace Lib.DataTypes {
	public class Password {
		public string Value { get; }

		public Password(string password) {
			if (password.Length < 8) {
				throw new InvalidPasswordException("Error: Password must be at least 8 characters.");
			}
			Value = password;
		}

		public byte[] Hash() {
			using(SHA512 sha = new SHA512Managed()) {
				return sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Value));
			}
		}

		public bool MatchesHash(IEnumerable<byte> hash) {
			IEnumerator enumerator = Hash().GetEnumerator();
			foreach (byte b in hash) {
				enumerator.MoveNext();
				if (b != (byte) enumerator.Current) {
					return false;
				}
			}
			return true;
		}

		public override bool Equals(object obj) {
			return obj is Password password && Value == password.Value;
		}

		public override int GetHashCode() {
			return -1937169414 + EqualityComparer<string>.Default.GetHashCode(Value);
		}

		public override string ToString() {
			return Value;
		}

		public static bool operator ==(Password password1, Password password2) {
			return password1.Equals(password2);
		}

		public static bool operator !=(Password password1, Password password2) {
			return !(password1 == password2);
		}

		public static implicit operator Password(string password) {
			return new Password(password);
		}

		public static implicit operator string(Password password) {
			return password.Value;
		}
	}
}