using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AdminPage : Page {
		private IBusiness Business { get; }

		private Session<Admin> AdminSession { get; }

		public AdminPage(IBusiness business, Session<Lib.Entities.Admin> admin_session) {
			InitializeComponent();
			Business = business;
			AdminSession = admin_session;
		}
	}
}