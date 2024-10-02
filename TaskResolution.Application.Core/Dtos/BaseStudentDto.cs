namespace TaskResolution.Application.Core.Dtos
{
    public class BaseStudentDto
    {
        // No ID property to avoid exposing it
        public string Number { get; set; }

        public string FirstName { get; set; }

        public string MidName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        // List of subject codes
        public IList<string> Subjects { get; set; }
    }
}