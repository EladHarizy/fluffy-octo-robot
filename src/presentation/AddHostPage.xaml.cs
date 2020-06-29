using System.Windows.Controls;
using business;
using Lib.Entities;

namespace presentation {
	public partial class AddHostPage : Page {
		private IBusiness business;

		public AddHostPage(IBusiness business) {
			InitializeComponent();
			this.business = business;
		}

		private void FormSubmit() {

		}
	}
}