using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class UnitsPage : Page {
		private IBusiness business;

		private Host host { get; }

		public UnitsPage(IBusiness business, Host host) {
			InitializeComponent();
			this.business = business;
			this.host = host;
			host_id.Text = host.ID;
		}
	}
}