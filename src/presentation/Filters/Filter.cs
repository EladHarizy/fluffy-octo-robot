using System.Collections.Generic;
using System.Linq;

namespace presentation {
	// Class to filter a source collection according to a collection of conditions
	internal class Filter<TObj> {
		private IEnumerable<TObj> Source { get; }

		private IEnumerable<ICondition<TObj>> Conditions { get; }

		public Filter(IEnumerable<TObj> source, params ICondition<TObj>[] conditions) {
			Source = source;
			Conditions = conditions;
		}

		public IEnumerable<TObj> ApplyFilter() {
			return Source.Where(obj => Conditions.All(condition => condition.Test(obj)));
		}
	}
}