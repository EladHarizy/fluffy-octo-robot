using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class SearchRequestsPage : Page {
		public IBusiness Business { get; }

		private Frame Frame { get; }

		public Host Host { get; }

		public SearchRequestsPage(IBusiness business, Frame frame, Host host) {
			Business = business;
			Frame = frame;
			Host = host;
		}
	}
}