using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	internal class ViewGuestRequestForHostPage : Page {
		private IBusiness Business { get; }
		private Frame Frame { get; }
		private GuestRequest GuestRequest { get; }

		public ViewGuestRequestForHostPage(IBusiness business, Frame frame, GuestRequest guest_request) {
			Business = business;
			Frame = frame;
			GuestRequest = guest_request;
		}
	}
}