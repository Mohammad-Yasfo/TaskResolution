using AutoMapper;
using TaskResolution.Application.Core.DomainModels;
using TaskResolution.Repositories.Core.Entities;

namespace TaskResolution.Repositories.Core.Profiles
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<SubjectEntity, Subject>();

            CreateMap<Subject, SubjectEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow));
            //.ForMember(dest => dest.StudentSubjects, src => src.Ignore());

            CreateMap<StudentEntity, Student>()
                .ForMember(dest => dest.Subjects, opt => opt.MapFrom(src => src.StudentSubjects.Select(cs => cs.Subject)));

            CreateMap<Student, StudentEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.StudentSubjects, src => src.Ignore());
        }
    }
}