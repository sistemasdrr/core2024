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

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SftpSettings>(builder.Configuration.GetSection("SftpSettings"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(DataProfile).Assembly);

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<IMySqlUserRepository, MySqlUserRepository>();
builder.Services.AddScoped<IMySqlWebRepository, MySqlWebRepository>();
builder.Services.AddScoped<IWebQueryRepository, WebQueryRepository>();
builder.Services.AddScoped<IAttachmentsNotSendRepository, AttachmentsNotSendRepository>();
builder.Services.AddScoped<IEmailConfigurationRepository, EmailConfigurationRepository>();
builder.Services.AddScoped<IEmailHistoryRepository, EmailHistoryRepository>();
builder.Services.AddScoped<IApiUserRepository, ApiUserRepository>();

builder.Services.AddScoped<IWebDataDomain, WebDataDomain>();
builder.Services.AddScoped<IAttachmentsNotSendDomain, AttachmentsNotSendDomain>();
builder.Services.AddScoped<IEmailConfigurationDomain, EmailConfigurationDomain>();
builder.Services.AddScoped<IEmailHistoryDomain, EmailHistoryDomain>();
builder.Services.AddScoped<IApiUserDomain, ApiUserDomain>();

builder.Services.AddScoped<IWebDataApplication, WebDataApplication>();
builder.Services.AddScoped<IEmailApplication, EmailApplication>();
builder.Services.AddScoped<IApiApplication, ApiApplication>();
builder.Services.AddScoped<ITokenValidationApplication, TokenValidationApplication>();

builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<DRRCore.Transversal.Common.Interface.ILogger, LoggerManager>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
