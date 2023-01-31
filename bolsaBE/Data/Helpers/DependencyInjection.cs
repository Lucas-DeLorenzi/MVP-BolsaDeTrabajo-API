using bolsaBE.Data.GenericRepository;
using bolsaBE.Data.GenericRepository.bolsaBE.Data.GenericRepository;
using bolsaBE.Data.SpecificRepository.Abstractions;
using bolsaBE.Data.SpecificRepository.Implementations;
using bolsaBE.DBContexts;
using bolsaBE.Entities;
using bolsaBE.Entities.Generics;
using bolsaBE.Services;
using bolsaBE.Services.Abstractions;
using bolsaBE.Services.Implementations;
using bolsaBE.Services.MailServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static bolsaBE.Data.GenericRepository.IGenericRepository;

namespace bolsaBE.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataRepositories(this IServiceCollection services)
        {
            // general
            services.AddScoped<IGenericRepository<IEntity>, GenericRepository<IEntity>>();
            services.AddScoped(typeof(IReadRepository<>), typeof(ReadOnlyRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBolsaDeTrabajoRepository, BolsaDeTrabajoRepository>();
            services.AddScoped<ISystemSupportMail, SystemSupportMail>();

            /// Repositories ///
            // auxTables
            services.AddScoped<ICivilStatusTypeRepository, CivilStatusTypeRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IGenderTypeRepository, GenderTypeRepository>();
            services.AddScoped<IKnowledgeValueRepository, KnowledgeValueRepository>();
            services.AddScoped<IKnowledgeTypeRepository, KnowledgeTypeRepository>();
            services.AddScoped<IWorkdayTypeRepository, WorkdayTypeRepository>();
            services.AddScoped<IRelationTypesRepository, RelationTypesRepository>();

            // entities 
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IStudentsRepository, StudentsRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IValidationRepository, ValidationRepository>();
            services.AddScoped<IDegreesRepository, DegreesRepository>();
            services.AddScoped<ISearchRepository, SearchRepository>(); 
            services.AddScoped<IPostulationRepository, PostulationRepository>();
            services.AddScoped<IKnowledgeRepository, KnowledgeRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();

            /// Services ///
            // auxTables
            services.AddScoped<IFullService, FullService>();

            // entities
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<ICompanyServices, CompanyServices>();
            // no validation services
            services.AddScoped<IDegreesServices, DegreesServices>();
            services.AddScoped<ISearchServices, SearchServices>();
            services.AddScoped<IPostulationServices, PostulationServices>();
            



            return services;
        }
    }
}
