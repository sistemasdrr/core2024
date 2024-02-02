using DRRCore.Application.Interfaces.MigrationApplication;
using DRRCore.Application.Main.MigrationApplication;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Domain.Main.CoreDomain;
using DRRCore.Domain.Main.MysqlDomain;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Infraestructure.Interfaces.MySqlRepository;
using DRRCore.Infraestructure.Repository.CoreRepository;
using DRRCore.Infraestructure.Repository.MYSQLRepository;
using DRRCore.Transversal.Common.Interface;
using DRRCore.Transversal.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMEmpresaRepository, MEmpresaRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyBackgroundRepository, CompanyBackgroundRepository>();
builder.Services.AddScoped<ICompanyBranchRepository, CompanyBranchRepository>();
builder.Services.AddScoped<ICompanyFinancialInformationRepository, CompanyFinancialInformationRepository>();
builder.Services.AddScoped<ICompanyCreditOpinionRepository, CompanyCreditOpinionRepository>();
builder.Services.AddScoped<ICompanyGeneralInformationRepository, CompanyGeneralInformationRepository>();
builder.Services.AddScoped<IFinancialBalanceRepository, FinancialBalanceRepository>();
builder.Services.AddScoped<ICompanySBSRepository, CompanySBSRepository>();
builder.Services.AddScoped<IBankDebtRepository, BankDebtRepository>();
builder.Services.AddScoped<IComercialLatePaymentRepository, ComercialLatePaymentRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>();
builder.Services.AddScoped<IImportsAndExportsRepository, ImportsAndExportsRepository>();
builder.Services.AddScoped<IFinancialSalesHistoryRepository, FinancialSalesHistoryRepository>();

builder.Services.AddScoped<IMEmpresaDomain, MEmpresaDomain>();
builder.Services.AddScoped<ICompanyDomain, CompanyDomain>();
builder.Services.AddScoped<ICompanyBackgroundDomain, CompanyBackgroundDomain>();
builder.Services.AddScoped<ICompanyBranchDomain, CompanyBranchDomain>();
builder.Services.AddScoped<ICompanyFinancialInformationDomain, CompanyFinancialInformationDomain>();
builder.Services.AddScoped<ICompanyCreditOpinionDomain, CompanyCreditOpinionDomain>();
builder.Services.AddScoped<ICompanyGeneralInformationDomain, CompanyGeneralInformationDomain>();
builder.Services.AddScoped<IFinancialBalanceDomain, FinancialBalanceDomain>();
builder.Services.AddScoped<ICompanySBSDomain, CompanySBSDomain>();
builder.Services.AddScoped<IBankDebtDomain, BankDebtDomain>();
builder.Services.AddScoped<IComercialLatePaymentDomain, ComercialLatePaymentDomain>();
builder.Services.AddScoped<IProviderDomain, ProviderDomain>();
builder.Services.AddScoped<IImportsAndExportsDomain, ImportsAndExportsDomain>();
builder.Services.AddScoped<IFinancialSalesHistoryDomain, FinancialSalesHistoryDomain>();

builder.Services.AddScoped<IMigraUser, MigraUser>();

builder.Services.AddScoped<IMailSender, MailSender>();
builder.Services.AddScoped<IFileManager, FileManager>();
builder.Services.AddScoped<IMailFormatter, MailFormatter>();

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
