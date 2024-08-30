namespace Autopood.Models.Plane
{
    public class ImageViewModel
    {
        public Guid ImageId { get; set; }
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
        public string Image { get; set; }
        public Guid? PlaneId { get; set; }
    }
}