namespace Domain.Entities
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; }
    }
}
