namespace TaskResolution.Application.Core.Dtos
{
    public class SubjectDto
    {
        // No ID property to avoid exposing it
        public string Name { get; set; }

        public string Code { get; set; }
    }
}