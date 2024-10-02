using Microsoft.EntityFrameworkCore;
using TaskResolution.Repositories.Core.Entities;

namespace TaskResolution.Repositories.Core
{
    public class StudentsDbContext : DbContext
    {
        const string ConfigSchema = "cfg";

        public StudentsDbContext(DbContextOptions<StudentsDbContext> options)
            : base(options)
        {
        }

        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<StudentSubjectEntity> StudentSubjects { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Configuring StudentEntity
            builder.Entity<StudentEntity>(entity =>
            {
                entity.ToTable("Students", ConfigSchema);

                entity.HasKey(e => e.ID); // Primary key

                entity.HasIndex(e => e.Number) // Unique index on Number
                    .IsUnique();

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.MidName)
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BirthDate)
                    .IsRequired();
            });

            // Configuring SubjectEntity
            builder.Entity<SubjectEntity>(entity =>
            {
                entity.ToTable("Subjects", ConfigSchema);

                entity.HasKey(e => e.ID); // Primary key

                entity.HasIndex(e => e.Code) // Unique index on Code
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Configuring the many-to-many relationship via StudentSubjectEntity
            builder.Entity<StudentSubjectEntity>(entity =>
            {
                entity.ToTable("StudentSubjects", ConfigSchema);

                entity.HasKey(ss => new { ss.StudentId, ss.SubjectId }); // Composite key

                entity.HasOne(ss => ss.Student)
                      .WithMany(s => s.StudentSubjects)
                      .HasForeignKey(ss => ss.StudentId);

                //entity.HasOne(ss => ss.Subject)
                //      .WithMany(s => s.StudentSubjects)
                //      .HasForeignKey(ss => ss.SubjectId);
            });

            // Seeding data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed subjects
            builder.Entity<SubjectEntity>().HasData(
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Mathematics", Code = "MATH101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Physics", Code = "PHYS101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Chemistry", Code = "CHEM101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Biology", Code = "BIO101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "History", Code = "HIST101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Geography", Code = "GEOG101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Literature", Code = "LIT101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Computer Science", Code = "CS101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Philosophy", Code = "PHIL101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Art", Code = "ART101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Economics", Code = "ECON101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Statistics", Code = "STAT101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Music", Code = "MUSIC101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Physical Education", Code = "PE101" },
                new SubjectEntity { ID = Guid.NewGuid(), Name = "Psychology", Code = "PSYCH101" }
            );

            // Seed students
            builder.Entity<StudentEntity>().HasData(
                new StudentEntity { ID = Guid.NewGuid(), Number = "S001", FirstName = "Mohammad", MidName = "Jafar", LastName = "AlYasfo", BirthDate = new DateTime(2000, 1, 1) },
                new StudentEntity { ID = Guid.NewGuid(), Number = "S002", FirstName = "Daniel", MidName = "Kamau", LastName = "Kimani", BirthDate = new DateTime(2001, 2, 1) },
                new StudentEntity { ID = Guid.NewGuid(), Number = "S003", FirstName = "Yasser", MidName = "Al", LastName = "Jarouf", BirthDate = new DateTime(2002, 3, 1) },
                new StudentEntity { ID = Guid.NewGuid(), Number = "S004", FirstName = "Shijoy", MidName = "A", LastName = "Anandan", BirthDate = new DateTime(2003, 4, 1) }
);
        }
    }
}