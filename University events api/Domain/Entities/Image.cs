namespace Domain.Entities
{
    public class Image : IEntity
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
    }
}
