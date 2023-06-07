using Logic.Interfaces;
using Shared;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class BranchViewModel: IViewModel
    {
        public Guid Id { get; private set; }
        [Required]
        public string Name { get; private set; }
        public BranchViewModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public BranchViewModel(string name)
        {
            Name = name;
            Id = Guid.Empty;
        }
        
        public override string ToString()
        {
            return Name;
        }
    }
}