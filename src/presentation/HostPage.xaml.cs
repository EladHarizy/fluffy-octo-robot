using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class HostPage : Page {
		private IBusiness Business;

		private Host Host { get; }

		private IEnumerable<Unit> Units { get; }

		public HostPage(IBusiness business, Host host) {
			Business = business;
			Host = host;
			Units = Business.UnitsOf(Host);
			InitializeComponent();
			DataContext = Units;
			host_name_textblock.Text = Host.Name;
		}

		private void SignOut(object sender, RoutedEventArgs e) {}
	}
}