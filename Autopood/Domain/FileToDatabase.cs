namespace Autopood.Domain
{
    public class FileToDatabase
    {
        public Guid Id { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public Guid? SpaceshipId { get; set; }
        public Guid? PlaneId { get; set; }
    }
}
