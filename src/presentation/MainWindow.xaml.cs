using System.Windows;
using System.Windows.Controls;
using business;

namespace presentation {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		private IBusiness business = new Business();

		private string OriginalTitle;

		public MainWindow() {
			InitializeComponent();
			OriginalTitle = Title;
			LoadPage(new HomePage());
		}

		private void LoadPage(Page page) {
			Page.Content = page;
			SetTitle(page.Title);
		}

		private void SetTitle(string title) {
			Title = OriginalTitle + " | " + title;
		}

		private void GuestRequestsPage(object sender, RoutedEventArgs e) {
			LoadPage(new GuestRequestsPage(business));
		}

		private void UnitsPage(object sender, RoutedEventArgs e) {
			LoadPage(new UnitsPage(business));
		}

		private void AdminPage(object sender, RoutedEventArgs e) {
			LoadPage(new AdminPage());
		}
	}
}