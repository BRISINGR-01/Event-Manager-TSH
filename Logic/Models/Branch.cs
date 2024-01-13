using Logic.Interfaces;

namespace Logic.Models
{

    public class Branch: IEntity
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Branch()
        {
            Id = Guid.Empty;
            Name = string.Empty;
        }
        public Branch(string name)
        {
            Id = Guid.Empty;
            Name = name;
        }
        public Branch(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
