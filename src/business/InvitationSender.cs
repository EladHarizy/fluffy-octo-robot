using System;
using System.Net.Mail;
using System.Text;
using Lib.Config;
using Lib.Entities;

namespace business {
	internal class InvitationSender {
		private Order Order { get; }

		public InvitationSender(Order order) {
			Order = order;
		}

		public void Send() {
			MailMessage message = new MailMessage();
			message.From = new MailAddress(Config.AdminEmail);
			message.To.Add(new MailAddress(Order.GuestRequest.Guest.Email));
			message.Subject = "You have received a new invitation!";
			message.ReplyToList.Add(new MailAddress(Order.Unit.Host.Email));
			BuildMessage(message);
			Config.SmtpClient.Send(message);
		}

		private void BuildMessage(MailMessage message) {
			message.IsBodyHtml = false;
			StringBuilder sb = new StringBuilder();

			sb.AppendLine("Dear " + Order.GuestRequest.Guest.Name + ',');
			sb.AppendLine();

			sb.AppendLine("We have found a potential host for your stay from " + Order.GuestRequest.StartDate + " to " + Order.GuestRequest.EndDate + ". " + Order.Unit.Host.Name + " is inviting you to stay at his place.");
			sb.AppendLine();

			sb.AppendLine("---- Hosting unit ----");
			sb.AppendLine(Order.Unit.Name);
			sb.AppendLine(Order.Unit.Description);
			sb.AppendLine("---- End of hosting unit ----");
			sb.AppendLine();

			sb.AppendLine("The host has left you the following message:");
			sb.AppendLine("---- Host message ----");
			sb.AppendLine(Order.Message);
			sb.AppendLine("---- End of host message ----");

			message.Body = sb.ToString();
		}
	}
}