namespace Shared.Errors
{
    public class AccessDeniedException : ClientException
    {
        public AccessDeniedException(Guid? id = null) : base("Access Denied to " + (id == null ? "an unauthenticated user" : id.ToString()) + " at " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString()) { }
    }
}
