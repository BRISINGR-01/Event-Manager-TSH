using Shared;

namespace Mocks
{
    public class MockData
    {
        public readonly static List<Guid> BranchIds = new() { Helpers.NewGuid, Helpers.NewGuid };
        public readonly static List<Guid> ImageIds = new() { Helpers.NewGuid, Helpers.NewGuid };
        public readonly static List<Guid> UserIds = new() { Helpers.NewGuid, Helpers.NewGuid };
        public readonly static List<Guid> EventIds = new() { Helpers.NewGuid, Helpers.NewGuid };
    }
}
