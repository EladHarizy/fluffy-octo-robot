using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class ViewUnitPage : Page {
		public IBusiness Business { get; }

		public Frame Frame { get; }

		public Unit Unit { get; }

		public ViewUnitPage(IBusiness business, Frame frame, Unit unit) {
			InitializeComponent();
			Business = business;
			Frame = frame;
			Unit = unit;
			DataContext = this;
		}

		private void Back(object sender, RoutedEventArgs e) {
			Frame.GoBack();
		}
	}
}