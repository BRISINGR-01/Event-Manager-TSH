using Logic.Interfaces;
using Logic.Utilities;

namespace Logic.Models.Images
{

    public class EventTagImage : IEntity
    {
        public Guid Id => EventId;
        public Guid EventId { get; private set; }
        public Guid BranchId { get; private set; }

        public EventTagImage(Guid eventId, Guid branchId)
        {
            BranchId = branchId;
            EventId = eventId;
        }
        public string? ImagePath => LocalPath.FindImagePath(Id, BranchId, Shared.Enums.ImageType.Shared);
    }
}
