using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Infraestructure.Interfaces.CoreRepository;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.EntityFrameworkCore;

namespace DRRCore.Infraestructure.Repository.CoreRepository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ILogger _logger;
        public CompanyRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<bool> ActiveWebVision(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Companies.FindAsync(id) ?? throw new Exception("No existe la empresa solicitada");
                    obj.OnWeb = true;
                    obj.UpdateDate = DateTime.Now;
                    context.Companies.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> AddAsync(Company obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using (var context = new SqlCoreContext())
                {
                    foreach (var item in Constants.TRADUCTIONS_FORMS)
                    {
                        var exist = obj.Traductions.Where(x => x.Identifier == item).FirstOrDefault();
                        if (exist == null)
                        {
                            obj.Traductions.Add(new Traduction
                            {
                                IdPerson = null,
                                Identifier = item,
                                IdLanguage = 1,
                                LastUpdaterUser=1
                            });
                        }
                    }
                    await context.Companies.AddAsync(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<int> AddCompanyAsync(Company obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using (var context = new SqlCoreContext())
                {
                    foreach (var item in Constants.TRADUCTIONS_FORMS)
                    {
                        var exist = obj.Traductions.Where(x => x.Identifier == item).FirstOrDefault();
                        if (exist == null)
                        {
                            obj.Traductions.Add(new Traduction
                            {
                                IdPerson = null,
                                Identifier = item,
                                IdLanguage = 1,
                                LastUpdaterUser = 1
                            });
                        }
                    
                    }
                    await context.Companies.AddAsync(obj);
                    await context.SaveChangesAsync();
                    return obj.Id;
                }
            }
            catch (Exception ex)
            {               
                throw new Exception(ex.InnerException == null ? ex.Message : ex.InnerException.Message);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Companies.FindAsync(id) ?? throw new Exception("No existe la empresa solicitada");
                    obj.Enable = false;
                    obj.DeleteDate = DateTime.Now;
                    context.Companies.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DesactiveWebVision(int id)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var obj = await context.Companies.FindAsync(id) ?? throw new Exception("No existe la empresa solicitada");
                    obj.OnWeb = false;
                    obj.UpdateDate = DateTime.Now;
                    context.Companies.Update(obj);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public Task<List<Company>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetByIdAsync(int id)
        {
            List<Traduction> traductions = new List<Traduction>();
            try
            {
                using var context = new SqlCoreContext();
                var company= await context.Companies.Where(x => x.Id == id).Include(x => x.IdCountryNavigation).Include(x => x.IdReputationNavigation)
                    .Include(x => x.IdLegalPersonTypeNavigation).Include(x => x.IdPaymentPolicyNavigation).Include(x => x.IdCreditRiskNavigation).FirstOrDefaultAsync() ?? throw new Exception("No existe la empresa solicitada");
                traductions.AddRange(await context.Traductions.Where(x => x.IdCompany == id && x.Identifier.Contains("_E_")).ToListAsync());
                company.Traductions = traductions;
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Company>> GetByNameAsync(string name, string form, int idCountry,bool haveReport, bool similar)
        {
            //Fata el Have Report
            List<Company> companys = new List<Company>();
            try
            {
                using var context = new SqlCoreContext();
                if (form == "C")
                {
                    if (idCountry == 0)
                    {
                        companys = await context.Companies.Include(x=>x.Traductions).Include(x=>x.IdCreditRiskNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.Name.Contains(name) || x.SocialName.Contains(name) && x.HaveReport==haveReport).Take(100).ToListAsync();
                    }
                    else
                    {
                        companys = await context.Companies.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).
                            Include(x => x.IdCountryNavigation).Where(x =>x.IdCountry==idCountry && (x.Name.Contains(name) || x.SocialName.Contains(name)) && x.HaveReport == haveReport).Take(100).ToListAsync();
                    }
                    
                }
                else
                {
                    if (idCountry == 0)
                    {
                        companys = await context.Companies.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.Name.StartsWith(name) || x.SocialName.StartsWith(name) && x.HaveReport == haveReport).Take(100).ToListAsync();
                    }
                    else
                    {
                        companys = await context.Companies.Include(x => x.Traductions).Include(x => x.IdCreditRiskNavigation).
                            Include(x => x.IdCountryNavigation).Where(x => x.IdCountry == idCountry && ( x.Name.StartsWith(name) || x.SocialName.StartsWith(name)) && x.HaveReport == haveReport).Take(100).ToListAsync();
                    }
                  
                }

                return companys; 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public Task<List<Company>> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<Company> GetByOldCode(string oldCode)
        {
            try
            {
                using var context = new SqlCoreContext();
                var company = await context.Companies.Include(x => x.IdCountryNavigation).FirstOrDefaultAsync(x => x.OldCode == oldCode);
                return company;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateAsync(Company obj)
        {
            try
            {
                using (var context = new SqlCoreContext())
                {
                    var existTraduction = obj.Traductions;
                    obj.Traductions=new List<Traduction>();
                    obj.UpdateDate = DateTime.Now;
                    context.Companies.Update(obj);

                    foreach (var item in existTraduction)
                    {
                        var modifierTraduction = await context.Traductions.Where(x => x.IdCompany == obj.Id && x.Identifier == item.Identifier).FirstOrDefaultAsync();
                        if (modifierTraduction != null)
                        {
                            modifierTraduction.ShortValue=item.ShortValue;
                            modifierTraduction.LargeValue=item.LargeValue;  
                            modifierTraduction.LastUpdaterUser=item.LastUpdaterUser;
                            context.Traductions.Update(modifierTraduction);
                        }
                        else
                        {
                            var newTraduction = new Traduction();
                            newTraduction.Id = 0;
                            newTraduction.IdCompany = obj.Id;
                            newTraduction.Identifier = item.Identifier;
                            newTraduction.ShortValue = item.ShortValue;
                            newTraduction.LargeValue = item.LargeValue;
                            newTraduction.LastUpdaterUser = item.LastUpdaterUser;
                            await context.Traductions.AddAsync(newTraduction);
                        }
                    }                    
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}
