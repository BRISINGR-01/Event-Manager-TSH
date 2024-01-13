namespace Shared.Enums
{
    [Flags]
    public enum UserRole
    {
        // the indices represent level of access in binary form
        // Admin > EventOrganizer > StudentComitee > Student > Guest
        // ex: 011 (EO) | 111(SC) = 111
        // 0001 & 0111 = 0001 (A passes)
        // 0011 & 0111 = 0011 (EO passes)
        // 0111 & 0111 = 0111 (SC passes)
        // 1111 & 0111 = 0111 != 1111 (S does not pass) 
        Administrator = 1,
        EventOrganizer = 3,
        StudentComitee = 7,
        Student = 15,
        Guest = 31
    }
}
