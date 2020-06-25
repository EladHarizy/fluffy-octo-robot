using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace presentation {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
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
			LoadPage(new GuestRequestsPage());
		}

		private void UnitsPage(object sender, RoutedEventArgs e) {
			LoadPage(new UnitsPage());
		}

		private void AdminPage(object sender, RoutedEventArgs e) {
			LoadPage(new AdminPage());
		}

	}
}