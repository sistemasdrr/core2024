using DRRCore.Application.Interfaces;
using DRRCore.Application.Main;
using DRRCore.Domain.Interfaces;
using DRRCore.Domain.Main;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using DRRCore.Infraestructure.Interfaces.Repository;
using DRRCore.Infraestructure.Repository.MYSQLRepository;
using DRRCore.Infraestructure.Repository.SQLRepository;
using DRRCore.Transversal.Common.Interface;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.JsonReader;
using DRRCore.Transversal.Mapper.Profiles.Web;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(DataProfile).Assembly);


//Injection Repository
builder.Services.AddScoped<IMySqlUserRepository, MySqlUserRepository>();
builder.Services.AddScoped<IMySqlWebRepository, MySqlWebRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmailUserRepository, EmailUserRepository>();
builder.Services.AddScoped<IWebQueryRepository, WebQueryRepository>();

//Injection Domain
builder.Services.AddScoped<IEmailDomain, EmailDomain>();
builder.Services.AddScoped<IEmailUserDomain, EmailUserDomain>();
builder.Services.AddScoped<IWebDataDomain, WebDataDomain>();
//Injection Application
builder.Services.AddScoped<IEmailUserApplication, EmailUserApplication>();
builder.Services.AddScoped<IWebDataApplication, WebDataApplication>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
//Injection Common
builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<DRRCore.Transversal.Common.Interface.ILogger, LoggerManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
