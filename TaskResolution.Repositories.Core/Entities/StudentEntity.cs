using System.ComponentModel.DataAnnotations;
using TaskResolution.Repositories.Core.Entities.Common;

namespace TaskResolution.Repositories.Core.Entities
{
    public class StudentEntity : TransactionalAndRelationEntity
    {
        [Required]
        [StringLength(50)]
        public string Number { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [StringLength(100)]
        public string MidName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public ICollection<StudentSubjectEntity> StudentSubjects { get; set; }
    }
}