using Shared.Enums;

namespace Unit_testing
{
    [TestClass]
    public class BaseTester
    {
        protected Manager Manager = new();
        [TestInitialize]
        public void Initialize()
        {
            Manager.Reset();
        }
        protected void SetUserRole(UserRole role) => Manager.SetUserRole(role);
    }
}
