namespace API.Entities
{
    public class UserSave
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public AppUser TargetUser { get; set;}
        public int TargetUserId { get; set; }

    }
}