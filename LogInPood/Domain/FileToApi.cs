namespace Autopood.Domain
{
    public class FileToApi
    {
        public Guid Id { get; set; }
        public string ExistingFilePath { get; set; }
        public Guid? CarId { get; set; }
    }
}
