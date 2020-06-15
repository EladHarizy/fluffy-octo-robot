using Lib.DataTypes;

namespace Lib.Interfaces {
	public interface IIndexed<TKey> : IIndexedReadOnly<TKey> where TKey : IAutoIndexable<TKey> {
		void Key(TKey key);
	}
}