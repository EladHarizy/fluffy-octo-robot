using Lib.DataTypes;

namespace Lib.Interfaces {
	public interface IIndexed : IIndexedReadOnly<ID> {
		void Key(ID key);
	}
}