using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using TaskResolution.Application.Core.Contracts;
using TaskResolution.Application.Core.Profiles;
using TaskResolution.Application.Core.Services;
using TaskResolution.Loggers;
using TaskResolution.Repositories.Core.Repositories;

namespace TaskResolution.Configuration
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(Component.For<Loggers.ILogger>().ImplementedBy<Log4NetLogger>().LifestyleTransient());
            container.Register(Component.For<IStudentsService>().ImplementedBy<StudentsService>().LifestyleTransient());
            container.Register(Component.For<IStudentsRepository>().ImplementedBy<StudentsRepository>().LifestyleTransient());
            container.Register(Component.For<ISubjectsService>().ImplementedBy<SubjectsService>().LifestyleTransient());
            container.Register(Component.For<ISubjectsRepository>().ImplementedBy<SubjectsRepository>().LifestyleTransient());

            // Register AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CoreMappingProfile>();
                cfg.AddProfile<CoreMappingProfile>();
            });

            IMapper mapper = config.CreateMapper();
            container.Register(Component.For<IMapper>().Instance(mapper).LifestyleSingleton());
        }
    }
}