using System.Collections.Generic;
using System.Linq;

namespace presentation {
	// Class to filter a source collection according to a collection of conditions
	public class Filter<TObj> {
		private IEnumerable<ICondition<TObj>> Conditions { get; }

		public Filter(params ICondition<TObj>[] conditions) {
			Conditions = conditions;
		}

		public IEnumerable<TObj> ApplyFilter(IEnumerable<TObj> source) {
			return source.Where(obj => Conditions.All(condition => condition.Test(obj)));
		}
	}
}