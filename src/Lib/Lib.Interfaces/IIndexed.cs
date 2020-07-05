using Lib.DataTypes;

namespace Lib.Interfaces {
	public interface IEntity<TIndex> : IEntityReadOnly<TIndex> where TIndex : IAutoIndexable<TIndex> {
		void Key(TIndex key);
	}
}