using Domain.Managers;
using Logic;
using Logic.Models.Events;
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
            var res = Manager.Event.AlterParticipance(Guid.Parse("97694444-91be-472d-acd8-650139dcf9b8"), Guid.Parse("62d3ff53-ad22-42b5-a56f-2c94fb819996"), EventParticipanceEnum.Present);

            Assert.IsTrue(res.IsSuccessful);
        }
        [TestMethod]
        public void SetNotSignedUserToPresent()
        {
            var res = Manager.Event.AlterParticipance(Guid.Parse("9d1acbca-640b-48cb-9421-adf9a863f9bd"), Guid.Parse("62d3ff53-ad22-42b5-a56f-2c94fb819996"), EventParticipanceEnum.Present);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Error, "You are not signed up yet");
        }
        [TestMethod]
        public void UnSignUserToEvent()
        {
            Guid eventId = MockRepository.EventFirst.Id;
            Guid userId = MockRepository.UserFirst.Id;

            var res = Manager.Event.AlterParticipance(userId, eventId, EventParticipanceEnum.None);

            Assert.IsTrue(res.IsSuccessful);
            Assert.IsTrue(res.Value);

            var resSigned = Manager.Event.Participance.GetAllSignedForEvent(eventId);

            Assert.IsTrue(resSigned.IsSuccessful);
            Assert.AreEqual(resSigned.Value, 0);
        }
        [TestMethod]
        public void SignUpUserForFullEvent()
        {
            var eventId = Helpers.NewGuid;
            var brnachId = Helpers.NewGuid;
            var userId = Guid.Parse("9d1acbca-640b-48cb-9421-adf9a863f9bd");
            Manager.Event.Create(new PaidEvent(
                eventId,
                brnachId,
                "title",
                "desc",
                DateTime.Now,
                null,
                null,
                1,
                1
            ));
            Manager.Event.Participance.Create(new(
                Helpers.NewGuid,
                eventId,
                Helpers.NewGuid,
                EventParticipanceEnum.Signed
            ));

            var res = Manager.Event.AlterParticipance(userId, eventId, EventParticipanceEnum.Signed);

            Assert.IsFalse(res.IsSuccessful);
            Assert.AreEqual(res.Error, "Unfortunately the event is fully booked! You can try again later.");
        }
        [TestMethod()]
        public void GetEventsPerMonthTest()
        {
            Manager.Event.Create(new Event(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                null
            ));
            Manager.Event.Create(new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                new DateTime(2023, 6, 1),
                null,
                null
            ));
            Manager.Event.Create(new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                new DateTime(2023, 6, 1),
                null,
                null
            ));
            var res = Manager.Event.GetCountEventsPerMonth();

            Assert.IsTrue(res.IsSuccessful);
            var dict = res.Value;
            Assert.AreEqual(dict["Other"], 1);
            Assert.AreEqual(dict["January"], 1);
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