namespace Lib.Interfaces {
	public interface IIndexed<TKey> : IIndexedReadOnly<TKey> {
		void Key(TKey key);
	}
}