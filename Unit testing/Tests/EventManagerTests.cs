using Logic.Models.Events;
using Mocks;
using Mocks.Repositories;
using Shared;
using Shared.Enums;

namespace Unit_testing.Tests
{
    [TestClass]
    public class EventManagerTests : BaseTester
    {
        [TestMethod]
        public void SetSignedUserToPresent()
        {
            var res = Manager.Event.AlterParticipance(MockData.UserIds[0], MockData.EventIds[0], EventParticipanceEnum.Present);

            Assert.IsTrue(res.IsSuccessful);
        }
        [TestMethod]
        public void SetNotSignedUserToPresent()
        {
            var res = Manager.Event.AlterParticipance(Helpers.NewGuid, MockData.EventIds[0], EventParticipanceEnum.Present);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Exception.Message, "You are not signed up yet");
        }
        [TestMethod]
        public void UnSignUserToEvent()
        {
            Guid eventId = MockRepositories.EventFirst.Id;
            Guid userId = MockRepositories.UserFirst.Id;

            var res = Manager.Event.AlterParticipance(userId, eventId, EventParticipanceEnum.None);

            Assert.IsTrue(res.IsSuccessful);

            var resSigned = Manager.Event.Participance.GetAllSignedForEvent(eventId);

            Assert.IsTrue(resSigned.IsSuccessful);
            Assert.AreEqual(resSigned.Value, 0);
        }
        [TestMethod]
        public void SignUpUserForFullEvent()
        {
            var eventId = Helpers.NewGuid;
            Manager.Event.Create(new PaidEvent(
                eventId,
                MockData.BranchIds[0],
                "title",
                "desc",
                DateTime.Now,
                null,
                null,
                1,
                1,
                false
            ));
            Manager.Event.Participance.Create(new(
                Helpers.NewGuid,
                eventId,
                MockData.UserIds[1],
                EventParticipanceEnum.Signed
            ));

            var res = Manager.Event.AlterParticipance(MockData.UserIds[0], eventId, EventParticipanceEnum.Signed);

            Assert.IsTrue(res.IsUnSuccessful);
            Assert.AreEqual(res.Exception.Message, "Unfortunately the event is fully booked! You can try again later.");
        }
        [TestMethod()]
        public void GetEventsPerMonthTest()
        {
            Manager.Event.Create(new Event(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                null,
                false
            ));
            Manager.Event.Create(new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                new DateTime(2023, 1, 12),
                null,
                null,
                false
            ));
            Manager.Event.Create(new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                new DateTime(2023, 6, 1),
                null,
                null,
                false
            ));
            Manager.Event.Create(new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                new DateTime(2023, 6, 1),
                null,
                null,
                false
            ));
            var res = Manager.Event.GetCountEventsPerMonth();

            Assert.IsTrue(res.IsSuccessful);
            var dict = res.Value;
            Assert.AreEqual(dict["Other"], 1);
            Assert.AreEqual(dict["June"], 2);
        }
        [TestMethod]
        public void GetUserStatistics()
        {
            var id = Helpers.NewGuid;
            Manager.Event.Participance.Create(new(
                Helpers.NewGuid,
                Helpers.NewGuid,
                id,
                EventParticipanceEnum.Present
            ));
            Manager.Event.Participance.Create(new(
                Helpers.NewGuid,
                Helpers.NewGuid,
                id,
                EventParticipanceEnum.Present
            ));
            Manager.Event.Participance.Create(new(
                Helpers.NewGuid,
                Helpers.NewGuid,
                id,
                EventParticipanceEnum.Signed
            ));

            var res = Manager.Event.Participance.GetUserStatistics();

            Assert.IsTrue(res.IsSuccessful);
            Assert.IsNotNull(res.Value);
            Assert.AreEqual(res.Value.Count, 2);
            Assert.AreEqual(res.Value[id].Count, 2);
            Assert.AreEqual(res.Value[id][EventParticipanceEnum.Present], 2);
            Assert.AreEqual(res.Value[id][EventParticipanceEnum.Signed], 1);
        }
    }
}