using System.ComponentModel.DataAnnotations;
using TaskResolution.Repositories.Core.Entities.Common;

namespace TaskResolution.Repositories.Core.Entities
{
    public class SubjectEntity : TransactionalAndRelationEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; }

        // No direct reference to students
        //public ICollection<StudentSubjectEntity> StudentSubjects { get; set; }
    }
}