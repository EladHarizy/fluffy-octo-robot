using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

internal static class HandlePreviewMouseWheel {
	public static void IgnorePreviewMouseWheel(object sender, MouseWheelEventArgs e) {
		if (!e.Handled) {
			e.Handled = true;
			MouseWheelEventArgs eventArg = new MouseWheelEventArgs(
				e.MouseDevice, e.Timestamp, e.Delta);
			eventArg.RoutedEvent = UIElement.MouseWheelEvent;
			eventArg.Source = sender;
			UIElement parent = ((Control) sender).Parent as UIElement;
			parent.RaiseEvent(eventArg);
		}
	}
}