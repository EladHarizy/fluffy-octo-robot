namespace presentation {
	public interface ICondition<T> {
		bool Test(T obj);
	}
}