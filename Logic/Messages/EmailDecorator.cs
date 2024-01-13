using Logic.Configuration;
using Logic.Interfaces;
using Logic.Models;
using Logic.Utilities;
using System.Net;
using System.Net.Mail;

namespace Logic.Messages
{
    public class EmailDecorator : MessagerDecorator, IMessager
    {
        private readonly EmailConfig config;
        private readonly IManager manager;

        public EmailDecorator(IMessager messager, EmailConfig config, IManager manager) : base(messager)
        {
            this.config = config;
            this.manager = manager;
        }
        public override Result Send(MessageData data)
        {
            var getByRoles = manager.User.GetByRoles(data.Recipients, data.BranchId);

            if (getByRoles.IsUnSuccessful) return HandleWrappee(data, getByRoles.Plain);

            var res = Result.Success;

            try
            {
                SendMail(data, getByRoles.Value);
            }
            catch (Exception ex)
            {
                res = new Result(ex);
            }

            return HandleWrappee(data, res);
        }

        private void SendMail(MessageData data, List<User> users)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                //Host = "smtp.gmail.com",
                UseDefaultCredentials = false,
                Port = 587,
                Credentials = new NetworkCredential(config.From, config.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true
            };

            MailMessage mail = new()
            {
                DeliveryNotificationOptions = DeliveryNotificationOptions.Delay | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess,
                Priority = MailPriority.High,
                Subject = data.Title,
                Body = data.Message,
                From = new MailAddress(config.From),
            };

            foreach (var user in users)
            {
                var res = manager.Credentials.GetById(user.Id);
                if (res.IsUnSuccessful) continue;

                mail.To.Add(new MailAddress(res.Value.Email));
                mail.CC.Add(new MailAddress(res.Value.Email));
            }

            mail.CC.Add(config.From);
            smtpClient.Send(mail);
        }
    }
}
