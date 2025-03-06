namespace Domain.Entities
{
    public class CurrentUser
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }

        public string UserName { get; set; }

    }
}