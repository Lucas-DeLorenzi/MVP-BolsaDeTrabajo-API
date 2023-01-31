using bolsaBE.DBContexts;
using bolsaBE.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using bolsaBE.Data;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.AddSecurityDefinition("TPIApiBearerAuth", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        Description = "Acá pegar el token generado al loguearse."
    });

    setupAction.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "TPIApiBearerAuth" }
                }, new List<string>() }
    });
});


builder.Services.AddDbContext<BolsaDeTrabajoContext>(
                dbContextOptions => dbContextOptions.UseSqlite(
                        builder.Configuration["ConnectionStrings:BolsaDeTrabajoDBConnectionString"]))
                .AddIdentityCore<User>().AddDefaultTokenProviders()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<BolsaDeTrabajoContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;
    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;

});


builder.Services.AddIdentityCore<Admin>().AddEntityFrameworkStores<BolsaDeTrabajoContext>();
builder.Services.AddIdentityCore<Company>().AddEntityFrameworkStores<BolsaDeTrabajoContext>();
builder.Services.AddIdentityCore<Student>().AddEntityFrameworkStores<BolsaDeTrabajoContext>();

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });


// Injectar en data.DependencyInjection.cs
builder.Services.AddDataRepositories();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BolsaDeTrabajoContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// configuracion CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    );

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//try { 
    await SeedData(); 
//} catch(Exception e) { Console.WriteLine(e); };

app.Run();

async Task SeedData()
{
    var scopeFactory = app!.Services.GetRequiredService<IServiceScopeFactory>();
    using var scope = scopeFactory.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<BolsaDeTrabajoContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    context.Database.EnsureCreated();

    if (userManager.Users.Count() < 4)
    {
        logger.LogInformation("Creando usuario de prueba");

        if (roleManager.Roles.Count() < 3)
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "Administrador"
            });
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "Empresa"
            });
            await roleManager.CreateAsync(new IdentityRole<Guid>
            {
                Name = "Alumno"
            });
        }

        var admin = new Admin("Juan", "Pérez")
        {
            Email = "administrador@email.com",
            UserName = "admin",
        };
        var empresa = new Company("La Segunda")
        {
            Email = "empresa@email.com",
            UserName = "empresa",
        };
        var relations = context.RelationTypes.ToList();
        var empresa2 = new Company("Tregar")
        {
            Email = "empresa2@email.com",
            UserName = "empresa2",
            CuilCuit = "20456001231",
            PhoneNumber = "34164658597",
            Sector = "Lacteos",
            Address = new Address()
            {
                Street = "España",
                StreetNumber = "5864",
                PostalCode = "2000",
                City = "Rosario"
            },
            Contact = new Contact()
            {
                FirstName = "Alvaro",
                LastName = "Gimenez",
                Phone = "3416589745",
                Email = "alvaro@gimenez.com",
                Position = "Encargado",
                RelationType = relations[0],
                RelationTypeId = relations[0].Id,
            }
        };

        var docTypes = context.DocumentTypes.ToList();
        var genderTypes = context.GenderTypes.ToList();
        var civilStatusTypes = context.CivilStatusTypes.ToList();   

        var alumno1 = new Student("José", "Gonzáles")
        {
            Email = "alumno1@email.com",
            UserName = "alumno1",
        };
        var alumno2 = new Student("María", "López")
        {
            Email = "alumno2@email.com",
            UserName = "alumno2",
        };
        var alumno3 = new Student("Juan", "Hidalgo")
        {
            Email = "juan@hidalgo.com",
            UserName = "alumno3",
            FileNumber = "56800",
            DocumentType = docTypes[0],
            DocumentNumber = "35452625",
            GenderType = genderTypes[0],
            CivilStatusType = civilStatusTypes[0],
        };



        await userManager.CreateAsync(admin, "123qwe");
        await userManager.AddToRoleAsync(admin, "Administrador");
        await userManager.CreateAsync(empresa, "123qwe");
        await userManager.AddToRoleAsync(empresa, "Empresa");
        await userManager.CreateAsync(empresa2, "123qwe");
        await userManager.AddToRoleAsync(empresa2, "Empresa");
        await userManager.CreateAsync(alumno1, "123qwe");
        await userManager.AddToRoleAsync(alumno1, "Alumno");
        await userManager.CreateAsync(alumno2, "123qwe");
        await userManager.AddToRoleAsync(alumno2, "Alumno");
        await userManager.CreateAsync(alumno3, "123qwe");
        await userManager.AddToRoleAsync(alumno3, "Alumno");

    }
}

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
