using AutoMapper;
using TaskResolution.Application.Core.DomainModels;
using TaskResolution.Application.Core.Dtos;

namespace TaskResolution.Application.Core.Profiles
{
    public class CoreMappingProfile : Profile
    {
        public CoreMappingProfile()
        {
            CreateMap<Subject, SubjectDto>();

            CreateMap<Student, StudentDto>();

            CreateMap<AddStudentDto, Student>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.Subjects, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    if (src.Subjects != null && src.Subjects.Count != 0)
                    {
                        dest.Subjects = [];
                        foreach (var subject in src.Subjects)
                        {
                            dest.Subjects.Add(new Subject
                            {
                                Code = subject
                            });
                        }
                    }
                });
        }
    }
}