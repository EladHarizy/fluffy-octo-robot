using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class GuestRequestsPage : Page {
		private IBusiness business;

		public GuestRequestsPage(IBusiness business) {
			InitializeComponent();
			this.business = business;
		}

		private void FormSubmit() {
			// Guest guest = business.Guest();
			// GuestRequest guest_request = new GuestRequest(guest, start_date, end_date, adults, children, region, desired_unit_types, desired_amenities);
		}
	}
}