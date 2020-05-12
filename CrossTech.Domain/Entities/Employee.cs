namespace CrossTech.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PositionId { get; set; }
        public UserProfile Profile { get; set; }
        public Position Position { get; set; }
    }
}
