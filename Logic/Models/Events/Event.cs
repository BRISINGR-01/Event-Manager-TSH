using Logic.Interfaces;
using Logic.Utilities;
using Shared;
using Shared.Enums;
using Shared.Errors;

namespace Logic.Models.Events
{

    public class Event : IEntity
    {
        public Guid Id { get; private set; }
        public Guid BranchId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string? Venue { get; private set; }
        public bool IsFullyBooked { get; private set; } = false;
        public int? Signed { get; private set; }
        public bool IsSuggestion { get; private set; }

        public static Event New(Guid id, Guid branchId, string title, string description, string? venue, bool isSuggestion) => new(id, branchId, title, description, venue, isSuggestion);
        public static Event New(Guid branchId, string title, string description, string? venue, bool isSuggestion) => new(Helpers.NewGuid, branchId, title, description, venue, isSuggestion);
        public Event(Guid id, Guid branchId, string title, string description, string? venue, bool isSuggestion)
        {
            Id = id;
            BranchId = branchId;
            Title = title;
            Description = description;
            Venue = venue;
            IsSuggestion = isSuggestion;
        }
        public string? Thumbnail => LocalPath.FindImagePath(Id, BranchId, ImageType.Thumbnail);
        public string? Background => LocalPath.FindImagePath(Id, BranchId, ImageType.Background);

        public void SetIsFullyBooked(bool isFullyBooked) => IsFullyBooked = isFullyBooked;

        public virtual void Validate(bool isUpdate = false)
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new ClientException("Title cannot be empty");
            if (string.IsNullOrWhiteSpace(Description)) throw new ClientException("Description cannot be empty");
        }

        public virtual void SetSigned(Result<int> res)
        {
            if (res.IsSuccessful) Signed = res.Value;
        }
    }
}
