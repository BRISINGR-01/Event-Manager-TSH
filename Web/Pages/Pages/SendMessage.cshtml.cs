using Logic.Configuration;
using Logic.Interfaces;
using Logic.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.Enums;
using Web.Middlewares.Authentication;

namespace Web.Pages.Pages
{
    public class SendMessageModel : PageModelWrapper
    {
        private readonly VapidConfig vapid;
        private readonly EmailConfig emailConfig;
        public SendMessageModel(IOptions<VapidConfig> vapid, IOptions<EmailConfig> emailConfig, IManager manager, IAuthenticationContext ctx) : base(manager, ctx, UserRole.EventOrganizer)
        {
            this.vapid = vapid.Value;
            this.emailConfig = emailConfig.Value;
        }
        [BindProperty]
        public string? Title { get; set; }
        [BindProperty]
        public string? Message { get; set; }
        [BindProperty]
        public bool SendToStudents { get; set; }
        [BindProperty]
        public bool SendToStudentComitees { get; set; }
        [BindProperty]
        public bool SendToEventOrganizers { get; set; }
        [BindProperty]
        public bool SendToAdministrators { get; set; }
        [BindProperty]
        public bool SendEmail { get; set; }
        [BindProperty]
        public bool SendNotification { get; set; }
        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Message))
            {
                HandleError("Message cannot be empty");
                return Page();
            }

            MessageData data = new(Title ?? string.Empty, Message, Ctx.User.BranchId);

            if (SendToStudents) data.AddRecipient(UserRole.Student);
            if (SendToStudentComitees) data.AddRecipient(UserRole.StudentComitee);
            if (SendToEventOrganizers) data.AddRecipient(UserRole.EventOrganizer);
            if (SendToAdministrators) data.AddRecipient(UserRole.Administrator);

            IMessager messager = new Messager();

            if (SendEmail) messager = new EmailDecorator(messager, emailConfig, Manager);
            if (SendNotification) messager = new PushNotificationDecorator(messager, new WebPushHandler(vapid), Manager);

            messager.Send(data);

            return Page();
        }
    }
}
