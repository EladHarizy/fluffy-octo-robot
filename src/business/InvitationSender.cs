using System;
using System.Net.Mail;
using System.Text;
using Lib.Config;
using Lib.Entities;
using Lib.Exceptions;

namespace business {
	internal class InvitationSender {
		private Order Order { get; }

		public InvitationSender(Order order) {
			Order = order;
		}

		public void Send() {
			try {
				MailMessage message = new MailMessage();
				message.From = new MailAddress(Config.AdminEmail);
				message.To.Add(new MailAddress(Order.GuestRequest.Guest.Email));
				message.Subject = "You have received a new invitation!";
				message.ReplyToList.Add(new MailAddress(Order.Unit.Host.Email));
				BuildMessage(message);
				Config.SmtpClient.Send(message);
			} catch (NullReferenceException ex) {
				throw new EmailNotSentException("Error: Cannot send email to guest because the guest request or unit have been deleted.", ex);
			} catch (Exception ex) when(ex is InvalidOperationException || ex is ObjectDisposedException || ex is SmtpException || ex is SmtpFailedRecipientException || ex is SmtpFailedRecipientsException) {
				throw new EmailNotSentException("Error: Could not send invitation to the guest. Please check your internet connection.", ex);
			}
		}

		private void BuildMessage(MailMessage message) {
			message.IsBodyHtml = true;

			message.Body = string.Format(@"<body><div style=""width:606px;border-style:solid;border-width:1px;border-color:#EEE;border-radius:20px;""><div style=""width:604px;border-style:solid;border-width:1px;border-color:#DDD;border-radius:20px;""><div style=""width:602px;border-style:solid;border-width:1px;border-color:#CCC;border-radius:20px;""><div style=""width:600px;border-style:solid;border-width:1px;border-color:#BBB;border-radius:20px;overflow:hidden;font-family:Arial, Helvetica, sans-serif;""><div style=""background-color:#E91E63;padding:40px;""><div style=""width:166px;margin:0 180px;border-style:solid;border-width:1px;border-color:rgba(128,128,128,0.1);border-radius:50%;""><div style=""width:164px;border-style:solid;border-width:1px;border-color:rgba(128,128,128,0.2);border-radius:50%;""><div style=""width:162px;border-style:solid;border-width:1px;border-color:rgba(128,128,128,0.3);border-radius:50%;""><div style=""width:160px;height:160px;border-style:solid;border-width:1px;border-color:rgba(128,128,128,0.4);border-radius:50%;background-color:white;background-image:url('https://i.imgur.com/d32E6Pb.png');background-position:center;background-repeat:no-repeat;background-size:80%;""></div></div></div></div></div><div style=""padding:20px;""><h1>Dear {0},</h1><p>We have found a potential host for your stay from {1} to {2}. {3} is inviting you to stay at his place. Here is a description of the place.</p><div style=""width:560px;border-style:solid;border-width:1px;border-color:#EEE;border-radius:20px;""><div style=""width:558px;border-style:solid;border-width:1px;border-color:#DDD;border-radius:20px;""><div style=""width:556px;border-style:solid;border-width:1px;border-color:#CCC;border-radius:20px;""><div style=""width:554px;border-style:solid;border-width:1px;border-color:#BBB;border-radius:20px;""><div style=""margin:10px;""><h3>{4}</h3><hr/><p>{5}</p></div></div></div></div></div><div style=""height:20px;""></div><div style=""width:560px;border-style:solid;border-width:1px;border-color:#EEE;border-radius:20px;""><div style=""width:558px;border-style:solid;border-width:1px;border-color:#DDD;border-radius:20px;""><div style=""width:556px;border-style:solid;border-width:1px;border-color:#CCC;border-radius:20px;""><div style=""width:554px;border-style:solid;border-width:1px;border-color:#BBB;border-radius:20px;""><div style=""margin:10px;""><h3>{3} says</h3><hr/><p>{6}</p></div></div></div></div></div><h5>With love,<br/>The fluffy octo-robot</h5></div></div></div></div></div></body>", Order.GuestRequest.Guest.Name, Order.GuestRequest.StartDate, Order.GuestRequest.EndDate, Order.Unit.Host.Name, Order.Unit.Name, Order.Unit.Description, Order.Message);
		}
	}
}