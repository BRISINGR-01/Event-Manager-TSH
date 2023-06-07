using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Event(Guid id, Guid branchId, string title, string description, string? venue)
        {
            Id = id;
            BranchId = branchId;
            Title = title;
            Description = description;
            Venue = venue;
        }
        public string? Thumbnail { get => LocalPath.ToRelative(LocalPath.FindThumbnailEventImagePath(BranchId, Id)); }
        public string? Background { get => LocalPath.ToRelative(LocalPath.FindFullEventImagePath(BranchId, Id)); }

        public void SetIsFullyBooked(bool isFullyBooked) => IsFullyBooked = isFullyBooked;

        public virtual void Validate(bool isUpdate = false) => new NotImplementedException();

        public virtual void SetSigned(Result<int> res)
        {
            if (res.IsSuccessful) Signed = res.Value;
        }
    }
}
