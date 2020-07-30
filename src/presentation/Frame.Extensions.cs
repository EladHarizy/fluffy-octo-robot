using System.Windows.Controls;

namespace presentation {
	public static class FrameExtensions {
		public static void ClearHistory(this Frame frame) {
			while (frame.CanGoBack) {
				frame.RemoveBackEntry();
			}
		}
	}
}