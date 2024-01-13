using Logic.Interfaces;
using Logic.Utilities;

namespace Logic.Models.Images
{

    public class UserTagImage : IEntity
    {
        public Guid Id => UserId;
        public Guid UserId { get; private set; }
        public Guid BranchId { get; private set; }
        public UserTagImage(Guid userId, Guid branchId)
        {
            UserId = userId;
            BranchId = branchId;
        }
        public string? ImagePath => LocalPath.FindImagePath(Id, BranchId, Shared.Enums.ImageType.Shared);
    }
}
