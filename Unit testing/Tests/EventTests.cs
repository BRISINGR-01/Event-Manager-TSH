using Logic.Models.Events;
using Mocks;
using Shared;
using Shared.Enums;
using Shared.Errors;

namespace Unit_testing.Tests
{
    [TestClass]
    public class EventTests : BaseTester
    {
        [TestMethod]
        public void ValidatePaidException()
        {
            var paidEvent = new PaidEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                DateTime.Now,
                null,
                null,
                -1,
                null,
                false
            );

            Assert.ThrowsException<ClientException>(() => paidEvent.Validate());
        }
        [TestMethod]
        public void ValidatePaidMax()
        {
            var paidEvent = new PaidEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                DateTime.Now,
                null,
                null,
                1,
                -1,
                false
            );

            paidEvent.Validate();

            Assert.IsNull(paidEvent.MaxParticipants);
        }
        [TestMethod]
        public void ValidateTimedEndBeforeStart()
        {
            var timedEvent = new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                DateTime.Now.AddDays(2),
                DateTime.Now.AddDays(1),
                null,
                false
            );

            timedEvent.Validate();

            Assert.IsNull(timedEvent.End);
        }
        [TestMethod]
        public void ValidateTimedEndBeforeNow()
        {
            var timedEvent = new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                DateTime.Now.AddDays(-2),
                DateTime.Now.AddDays(-1),
                null,
                false
            );

            Assert.ThrowsException<ClientException>(() => timedEvent.Validate());
        }
        [TestMethod]
        public void ValidateTimedEndBeforeNowUpdate()
        {
            var timedEvent = new TimedEvent(
                Helpers.NewGuid,
                Helpers.NewGuid,
                "title",
                "desc",
                DateTime.Now.AddDays(-2),
                DateTime.Now.AddDays(-1),
                null,
                false
            );

            Exception? exc = null;

            try
            {
                timedEvent.Validate(true);
            }
            catch (Exception ex)
            {
                exc = ex;
            }

            Assert.IsNull(exc);
        }
        [TestMethod]
        public void IsFullyBooked()
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

            Manager.Event.AlterParticipance(MockData.UserIds[0], eventId, EventParticipanceEnum.Signed);
            var res = Manager.Event.GetBy(eventId);

            Assert.IsTrue(res.IsSuccessful);
            Assert.IsTrue(res.Value?.IsFullyBooked);
        }
        [TestMethod]
        public void Percentage()
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
                10,
                false
            ));
            Manager.Event.AlterParticipance(MockData.UserIds[0], eventId, EventParticipanceEnum.Signed);
            Manager.Event.AlterParticipance(Helpers.NewGuid, eventId, EventParticipanceEnum.Signed);

            var res = Manager.Event.GetBy(eventId);

            Assert.IsTrue(res.IsSuccessful);
            Assert.IsInstanceOfType(res.Value, typeof(PaidEvent));

            PaidEvent paidEvent = (PaidEvent)res.Value;
            paidEvent.SetSigned(Manager.Event.Participance.GetAllSignedForEvent(eventId));

            Assert.AreEqual(paidEvent.Percentage, 20);
        }
    }
}