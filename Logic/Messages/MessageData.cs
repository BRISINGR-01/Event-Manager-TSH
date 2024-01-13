using Shared.Enums;

namespace Logic.Messages
{
    public class MessageData
    {

        public string Title { get; private set; }
        public string Message { get; private set; }
        public List<UserRole> Recipients { get; private set; }
        public Guid BranchId { get; private set; }
        public MessageData(string title, string message, Guid branchId)
        {
            Title = title;
            Message = message;
            Recipients = new();
            BranchId = branchId;
        }

        public void AddRecipient(UserRole recipient)
        {
            Recipients.Add(recipient);
        }
    }
}
