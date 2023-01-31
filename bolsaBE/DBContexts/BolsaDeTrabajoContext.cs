using bolsaBE.Entities;
using bolsaBE.Entities.Auxiliary.Types.Implementation;
using bolsaBE.Entities.Users.Implementations;
using bolsaBE.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bolsaBE.DBContexts
{
    public class BolsaDeTrabajoContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Degree> Degrees => Set<Degree>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<CivilStatusType> CivilStatusTypes => Set<CivilStatusType>();
        public DbSet<Company> Companies => Set<Company>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<ContactType> ContactTypes => Set<ContactType>();
        public DbSet<DocumentType> DocumentTypes => Set<DocumentType>();
        public DbSet<Search> Searches => Set<Search>();
        public DbSet<Internship> Internships => Set<Internship>();
        public DbSet<Job> Jobs => Set<Job>();
        public DbSet<KnowledgeType> KnowledgeTypes => Set<KnowledgeType>();
        public DbSet<KnowledgeValue> KnowledgeValues => Set<KnowledgeValue>();
        public DbSet<Knowledge> Knowledgments => Set<Knowledge>();
        public DbSet<Postulation> Postulations => Set<Postulation>();
        public DbSet<RelationType> RelationTypes => Set<RelationType>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<OtherData> OtherData => Set<OtherData>();
        public DbSet<UniversityData> UniversityData => Set<UniversityData>();
        public DbSet<Validation> Validations => Set<Validation>();
        public DbSet<WorkdayType> WorkdayTypes => Set<WorkdayType>();
        public DbSet<GenderType> GenderTypes => Set<GenderType>();
        public BolsaDeTrabajoContext(DbContextOptions<BolsaDeTrabajoContext> options) :base(options)
        { }

        public BolsaDeTrabajoContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var knowledgeType = new KnowledgeType[5]
            {
                new KnowledgeType()
                {
                    Name = ".NET",
                    Order = 0,
                },
                new KnowledgeType()
                {
                    Name = "React.js",
                    Order= 1,
                },
                new KnowledgeType()
                {
                    Name = "Vue.js",
                    Order = 2,
                    
                },
                new KnowledgeType()
                {
                    Name = "Ruby",
                    Order = 3,
                },
                new KnowledgeType()
                {
                    Name = "GraphQL",
                    Order = 4,
                }
            };
            modelBuilder.Entity<KnowledgeType>().HasData(knowledgeType);


            var relationType = new RelationType[2]
            {
                new RelationType()
                { Name = "Trabajo en la Empresa que solicita la Búsqueda", Order = 1 },
                new RelationType()
                { Name = "Trabajo para una consultora", Order = 2 },
            };
            modelBuilder.Entity<RelationType>().HasData(relationType);


            var civilStatusType = new CivilStatusType[5]
            {
                new CivilStatusType()
                { Name = "Soltero/a", Order = 1 },
                new CivilStatusType()
                { Name = "Casado/a", Order = 2 },
                new CivilStatusType()
                { Name = "Viudo/a", Order = 3 },
                new CivilStatusType()
                { Name = "Divorciado/a", Order = 4 },
                new CivilStatusType()
                { Name = "Otro", Order = 5 },
            };
            modelBuilder.Entity<CivilStatusType>().HasData(civilStatusType);

            var knowledgeValue = new KnowledgeValue[4]
            {
                new KnowledgeValue()
                { Name = "Experto", Order = 1 },
                new KnowledgeValue()
                { Name = "Alto", Order = 2 },
                new KnowledgeValue()
                { Name = "Medio", Order = 3 },
                new KnowledgeValue()
                { Name = "Bajo", Order = 4 },
            };
            modelBuilder.Entity<KnowledgeValue>().HasData(knowledgeValue);

            var genderTypes = new GenderType[3]
            {
                new GenderType()
                { Name = "Mujer", Order = 1 },
                new GenderType()
                { Name = "Hombre", Order = 2 },
                new GenderType()
                { Name = "Otro", Order = 3 },
            };
            modelBuilder.Entity<GenderType>().HasData(genderTypes);

            var documentTypes = new DocumentType[5]
            {
                new DocumentType()
                { Name = "DNI", Order = 1 },
                new DocumentType()
                { Name = "LC", Order = 2 },
                new DocumentType()
                { Name = "LE", Order = 3 },
                new DocumentType()
                { Name = "CI", Order = 4 },
                new DocumentType()
                { Name = "PASAPORTE", Order = 4 }
            };
            modelBuilder.Entity<DocumentType>().HasData(documentTypes);

            var workDayTypes = new WorkdayType[2]
            {
                new WorkdayType()
                { Name = "Part-Time", Order = 1 },
                new WorkdayType()
                { Name = "Full Time", Order = 2 },
            };
            modelBuilder.Entity<WorkdayType>().HasData(workDayTypes);

            var Degrees = new Degree[5]
            {
                new Degree()
                {
                    DegreeId = Guid.NewGuid(),
                    DegreeTitle = "Ingenieria en Sistemas de la Informacion",
                    Abbreviation = "ISIS",
                    DegreeCategory = DegreeCategory.Grado,
                    TotalSubjects = 50
                },
                new Degree()
                {
                    DegreeId = Guid.NewGuid(),
                    DegreeTitle = "Ingenieria Mecanica",
                    Abbreviation = "IMEC",
                    DegreeCategory = DegreeCategory.Grado,
                    TotalSubjects = 51
                },
                new Degree()
                {
                    DegreeId = Guid.NewGuid(),
                    DegreeTitle = "Ingenieria Quimica",
                    Abbreviation = "IQUI",
                    DegreeCategory = DegreeCategory.Grado,
                    TotalSubjects = 50
                },
                new Degree()
                {
                    DegreeId = Guid.NewGuid(),
                    DegreeTitle = "Ingenieria Civil",
                    Abbreviation = "ICIV",
                    DegreeCategory = DegreeCategory.Grado,
                    TotalSubjects = 49
                },
                new Degree()
                {
                    DegreeId = Guid.NewGuid(),
                    DegreeTitle = "Ingenieria Electrica",
                    Abbreviation = "IELE",
                    DegreeCategory = DegreeCategory.Grado,
                    TotalSubjects = 52
                }
            };

            modelBuilder.Entity<Degree>().HasData(Degrees);

            modelBuilder.Entity<Student>()
                .HasIndex(b => b.DocumentNumber)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(b => b.FileNumber)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(b => b.BusinessName)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(b => b.CuilCuit)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }

    }
}
