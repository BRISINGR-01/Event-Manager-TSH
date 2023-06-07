using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class EventViewModel: IViewModel
    {
        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        [DataType(DataType.Text)]
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Upload), FromForm(Name = "Image")]
        public IFormFile? Image { get ; set; }
        [DataType(DataType.DateTime)]
        public DateTime? Start { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? End { get; set; }

        [DataType(DataType.Text | DataType.Url)]
        public string? Venue { get; set; }
        [Range(0, 32767)]
        public double? Price { get; set; }
        [Range(0, 32767)]
        public int? MaxParticipants { get; set; }

        public EventViewModel(Guid id, Guid branchId, string title, string description, IFormFile? image, DateTime start, DateTime? end, string? venue, double? price, int? maxParticipants)
        {
            Id = id;
            BranchId = branchId;
            Title = title;
            Description = description;
            Image = image;
            Start = start;
            End = end;
            Venue = venue;
            Price = price;
            MaxParticipants = maxParticipants;
        }
        public EventViewModel()
        {
            Id = Guid.Empty;
            BranchId = Guid.Empty;
            Title = string.Empty;
            Description = string.Empty;
            Image = default;
            Start = DateTime.MinValue;
            End = DateTime.MinValue;
            Venue = string.Empty;

        }
    }
}
