using Domain.Managers;
using Logic;
using Logic.Models.Events;
using Shared;
using Shared.Enums;
using Shared.Errors;
using System.Data.SqlTypes;

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
                null
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
                -1
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
                null
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
                null
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
                null
            );

            Exception? exc = null;

            try
            {
                timedEvent.Validate(true);
            } catch (Exception ex)
            {
                exc = ex;
            }

            Assert.IsNull(exc);
        }
        [TestMethod]
        public void IsFullyBooked()
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

            Manager.Event.AlterParticipance(userId, eventId, EventParticipanceEnum.Signed);
            var res = Manager.Event.GetBy(eventId);

            Assert.IsTrue(res.IsSuccessful);
            Assert.IsTrue(res.Value?.IsFullyBooked);
        }
        [TestMethod]
        public void Percentage()
        {
            var eventId = Helpers.NewGuid;
            var brnachId = Helpers.NewGuid;
            var userId = MockRepository.UserFirst.Id;
            Manager.Event.Create(new PaidEvent(
                eventId,
                brnachId,
                "title",
                "desc",
                DateTime.Now,
                null,
                null,
                1,
                10
            ));
            Manager.Event.AlterParticipance(userId, eventId, EventParticipanceEnum.Signed);
            Manager.Event.AlterParticipance(Helpers.NewGuid, eventId, EventParticipanceEnum.Signed);

            var res = Manager.Event.GetBy(eventId);

            Assert.IsTrue(res.IsSuccessful);
            Assert.IsInstanceOfType(res.Value, typeof(PaidEvent));

            PaidEvent paidEvent = (PaidEvent)res.Value!;
            paidEvent.SetSigned(Manager.Event.Participance.GetAllSignedForEvent(eventId));

            Assert.AreEqual(paidEvent.Percentage, 20);
        }
    }
}