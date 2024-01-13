using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class EventSuggestionViewModel
    {
        [Required, DataType(DataType.Text)]
        public string Title { get; set; }
        [Required, DataType(DataType.Text)]
        public string Description { get; set; }
        public string? Venue { get; set; }
        public EventSuggestionViewModel(string title, string description, string venue)
        {
            Title = title;
            Description = description;
            Venue = venue;
        }
        public EventSuggestionViewModel()
        {
            Title = string.Empty;
            Description = string.Empty;
            Venue = null;
        }
    }
}
