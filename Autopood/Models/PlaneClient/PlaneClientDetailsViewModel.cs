namespace Autopood.Models.Plane
{
    public class PlaneClientDetailsViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Register { get; set; }
        public int SerialNumber { get; set; }
        public string Engine { get; set; }
        public string Propeller { get; set; }
        public int TotalTime { get; set; }
        public int Seats { get; set; }
        public bool Inspection { get; set; }
        public List<ImageViewModel> Image { get; set; } = new List<ImageViewModel>();



        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
