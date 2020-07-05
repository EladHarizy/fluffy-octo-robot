using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostPage : Page {
		private IBusiness business;

		private Host host { get; }

		public HostPage(IBusiness business, Host host) {
			InitializeComponent();
			this.business = business;
			this.host = host;
		}
	}
}