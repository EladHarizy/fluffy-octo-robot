using System.ComponentModel;
namespace lib {

	public enum Status {
		NotAddressed,
		SentEmail,
		ClosedForCustomerUnresponsive,
		ClosedForCustomerResponsive
	}

	public static class StatusExtensions {
		public static string ToFriendlyString(this Status me) {
			switch (me) {
				case Status.NotAddressed:
					return "Not Addressed";
				case Status.SentEmail:
					return "Send Email";
				case Status.ClosedForCustomerUnresponsive:
					return "Closed for customer unresponsive";
				case Status.ClosedForCustomerResponsive:
					return "Closed for customer responsive";
				default:
					return "Unassigned";
			}
		}

	}
}