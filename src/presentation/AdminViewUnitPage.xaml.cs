using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public class AdminViewUnitPage : Page {
		private IBusiness business;
		private Frame frame;
		private Unit unit;

		public AdminViewUnitPage(IBusiness business, Frame frame, Unit unit) {
			this.business = business;
			this.frame = frame;
			this.unit = unit;
		}
	}
}