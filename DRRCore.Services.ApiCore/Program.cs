using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using DRRCore.Application.Interfaces;
using DRRCore.Application.Main;
using DRRCore.Domain.Interfaces;
using DRRCore.Domain.Main;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using DRRCore.Infraestructure.Interfaces.Repository;
using DRRCore.Infraestructure.Repository.MYSQLRepository;
using DRRCore.Infraestructure.Repository.SQLRepository;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using DRRCore.Transversal.Common.JsonReader;
using DRRCore.Transversal.Mapper.Profiles.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Infraestructure.Repository.CoreRepository;
using Azure.Identity;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Main.CoreDomain;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Application.Main.CoreApplication;
using DRRCore.Transversal.Mapper.Profiles.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SftpSettings>(builder.Configuration.GetSection("SftpSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(EmployeeProfile).Assembly);
builder.Services.AddAutoMapper(typeof(ComboProfile).Assembly);

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IJobDepartmentRepository, JobDepartmentRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IUserProcessRepository, UserProcessRepository>();
builder.Services.AddScoped<ICivilStatusRepository, CivilStatusRepository>();
builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IFamilyBondTypeRepository, FamilyBondyTypeRepository>();
builder.Services.AddScoped<IBankAccountTypeRepository, BankAccountTypeRepository>();

builder.Services.AddScoped<ICountryDomain, CountryDomain>();
builder.Services.AddScoped<IDocumentTypeDomain, DocumentTypeDomain>();
builder.Services.AddScoped<IEmployeeDomain, EmployeeDomain>();
builder.Services.AddScoped<IJobDepartmentDomain, JobDepartmentDomain>();
builder.Services.AddScoped<IJobDomain, JobDomain>();
builder.Services.AddScoped<IProcessDomain, ProcessDomain>();
builder.Services.AddScoped<IUserLoginDomain, UserLoginDomain>();
builder.Services.AddScoped<IUserProcessDomain, UserProcessDomain>();
builder.Services.AddScoped<ICivilStatusDomain, CivilStatusDomain>();
builder.Services.AddScoped<IBankAccountTypeDomain, BankAccountTypeDomain>();
builder.Services.AddScoped<IFamilyBondyTypeDomain, FamilyBondyTypeDomain>();
builder.Services.AddScoped<ICurrencyDomain, CurrencyDomain>();

builder.Services.AddScoped<IComboboxApplication, ComboboxApplication>();
builder.Services.AddScoped<IEmployeeApplication, EmployeeAplication>();

builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<IMailFormatter, MailFormatter>();
builder.Services.AddScoped<IFunction, Functions>();
builder.Services.AddScoped<DRRCore.Transversal.Common.Interface.ILogger, LoggerManager>();

/*
 * JWT
 */


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
/*
 * //JWT
 */

var app = builder.Build();

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
