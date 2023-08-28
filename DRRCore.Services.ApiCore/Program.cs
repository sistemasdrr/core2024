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
builder.Services.AddScoped<IApiApplication, ApiApplication>();
builder.Services.AddScoped<ITokenValidationApplication, TokenValidationApplication>();

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

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
