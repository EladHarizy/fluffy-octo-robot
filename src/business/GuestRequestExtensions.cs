using Lib.Entities;

namespace business {
	public static class GuestRequestExtensions {
		public static int GuestCount(this GuestRequest guest_request) {
			return guest_request.Adults + guest_request.Children;
		}
	}
}