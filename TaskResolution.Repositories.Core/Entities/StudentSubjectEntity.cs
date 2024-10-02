using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskResolution.Repositories.Core.Entities.Common;

namespace TaskResolution.Repositories.Core.Entities
{
    public class StudentSubjectEntity : RelationEntity
    {
        [Required]
        [ForeignKey("Student")]
        public Guid StudentId { get; set; }
        public StudentEntity Student { get; set; }

        [Required]
        [ForeignKey("Subject")]
        public Guid SubjectId { get; set; }
        public SubjectEntity Subject { get; set; }
    }
}