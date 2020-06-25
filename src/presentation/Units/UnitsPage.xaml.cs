using System.Windows.Controls;
using business;

namespace presentation {
	public partial class UnitsPage : Page {
		private IBusiness business;

		public UnitsPage(IBusiness business) {
			this.business = business;
			InitializeComponent();
		}
	}
}