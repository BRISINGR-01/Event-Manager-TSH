namespace Shared
{
    public static class Helpers
    {
        private static string imageFolderPath => Path.Combine(Environment.CurrentDirectory, "wwwroot", "images");

        public static string NewId => Guid.NewGuid().ToString();
        public static Guid NewGuid => Guid.NewGuid();

        public static void ReportException(Exception ex)
        {
            return;
            //var smtpClient = new SmtpClient("smtp.gmail.com")
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 25,
            //    Credentials = new System.Net.NetworkCredential(Dev.FromEmailAddress, Dev.EmailPassword),
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    EnableSsl = true
            //}
            //;
            //MailMessage mail = new()
            //{
            //    DeliveryNotificationOptions = DeliveryNotificationOptions.Delay | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess,
            //    Priority = MailPriority.High,
            //    Subject = "TSH Events Error",
            //    Body = "An exception wasn't handled:" +
            //    $"\n{ex.Message}" +
            //    $"\n\n at {ex.StackTrace}",
            //    From = new MailAddress(Dev.FromEmailAddress),
            //};
            //mail.CC.Add(Dev.FromEmailAddress);
            //mail.CC.Add(Dev.ToEmailAddress);
            //mail.To.Add(new MailAddress(Dev.ToEmailAddress));

            //try
            //{
            //    smtpClient.Send(mail);
            //} catch { }

            //var smtpClient = new SmtpClient()
            //{
            //    Host = "mailrelay.fhict.local",
            //    Port = 25,
            //    Credentials = new System.Net.NetworkCredential(Dev.FromEmailAddress, Dev.EmailPassword),
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    EnableSsl = true
            //}
            //;
            //MailMessage mail = new()
            //{
            //    DeliveryNotificationOptions = DeliveryNotificationOptions.Delay | DeliveryNotificationOptions.OnFailure | DeliveryNotificationOptions.OnSuccess,
            //    Priority = MailPriority.High,
            //    Subject = "TSH Events Error",
            //    Body = "An exception wasn't handled:" +
            //    $"\n{ex.Message}" +
            //    $"\n\n at {ex.StackTrace}",
            //    From = new MailAddress(Dev.FromEmailAddress),
            //};
            //mail.CC.Add(Dev.FromEmailAddress);
            //mail.CC.Add(Dev.ToEmailAddress);
            //mail.To.Add(new MailAddress(Dev.ToEmailAddress));

            //smtpClient.Send(mail);
            //try
            //{
            //}
            //catch { }
        }
    }
}
