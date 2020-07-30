using System.Collections.Generic;
using Lib.DataTypes;
using Lib.Interfaces;

namespace Lib.Entities {
	public class User : IEntity<ID> {
		public User(ID id, Email email, IEnumerable<byte> password_hash) {
			ID = id;
			Email = email;
			PasswordHash = password_hash;
		}

		public virtual ID ID { get; private set; }

		public virtual Email Email { get; set; }

		public virtual IEnumerable<byte> PasswordHash { get; }

		public override bool Equals(object obj) {
			return obj is User user
				&& EqualityComparer<ID>.Default.Equals(ID, user.ID);
		}

		public override int GetHashCode() {
			int hashCode = 833672617;
			hashCode = hashCode * -1521134295 + EqualityComparer<ID>.Default.GetHashCode(ID);
			return hashCode;
		}

		public ID Key() {
			return ID;
		}

		public void Key(ID key) {
			ID = key;
		}
	}
}