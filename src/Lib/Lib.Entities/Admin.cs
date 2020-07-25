using System.Collections.Generic;
using Lib.DataTypes;
using Lib.Extensions;
using Lib.Interfaces;

namespace Lib.Entities {
	public class Admin : User, ICloneable<Admin> {

		public Admin(string email, Password password) : this(null, email, password.Hash()) {}

		public Admin(ID id, Email email, IEnumerable<byte> password_hash) : base(id, email, password_hash) {}

		public override string ToString() {
			return "Admin " + ID + " - " + Email;
		}

		public Admin Clone() {
			return new Admin(ID, Email, PasswordHash);
		}

		public override bool Equals(object obj) {
			return base.Equals(obj);
		}

		public override int GetHashCode() {
			return base.GetHashCode();
		}
	}
}