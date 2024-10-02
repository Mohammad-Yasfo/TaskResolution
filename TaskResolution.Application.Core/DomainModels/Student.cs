namespace TaskResolution.Application.Core.DomainModels
{
    public class Student
    {
        public Guid ID { get; set; }
        public string Number { get; set; }
        public string FirstName { get; set; }
        public string MidName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}