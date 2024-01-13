namespace Shared.Errors
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Not found") { }
    }
}
