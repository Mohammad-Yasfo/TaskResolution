namespace TaskResolution.Application.Core.DomainModels
{
    public class Subject
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public ICollection<Student> Students { get; set; }
    }
}